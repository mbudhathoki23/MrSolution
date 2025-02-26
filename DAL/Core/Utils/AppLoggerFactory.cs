namespace MrDAL.Core.Utils;

//   public abstract class AppLoggerFactory
//{
//	protected static IAppLogger _defaultEngine;

//	public static IAppLogger DefaultEngine
//	{
//		get => _defaultEngine ?? new AppNLogger();
//		set => _defaultEngine = value;
//	}

//	public static IAppLogger GetEngine(LoggerEngine engine)
//	{
//		switch (engine)
//		{
//			case LoggerEngine.NLog:
//				return new AppNLogger();
//               default:
//				throw new ArgumentOutOfRangeException(nameof(engine), engine, null);
//		}
//	}
//}