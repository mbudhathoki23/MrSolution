using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MrDAL.Core.Utils;

public class Debouncer : IDisposable
{
    private readonly Action _action;
    private readonly object _mutex = new();
    private readonly HashSet<ManualResetEvent> _resets = new();
    private readonly TimeSpan _ts;

    public Debouncer(TimeSpan timespan, Action action)
    {
        _ts = timespan;
        _action = action;
    }

    public void Dispose()
    {
        lock (_mutex)
        {
            while (_resets.Count > 0)
            {
                var reset = _resets.First();
                _resets.Remove(reset);
                reset.Set();
            }
        }
    }

    public void Invoke()
    {
        var thisReset = new ManualResetEvent(false);

        lock (_mutex)
        {
            while (_resets.Count > 0)
            {
                var otherReset = _resets.First();
                _resets.Remove(otherReset);
                otherReset.Set();
            }

            _resets.Add(thisReset);
        }

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                if (!thisReset.WaitOne(_ts)) _action();
            }
            finally
            {
                lock (_mutex)
                {
                    using (thisReset)
                    {
                        _resets.Remove(thisReset);
                    }
                }
            }
        });
    }
}