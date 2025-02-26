using System;

namespace MrDAL.Lib.Dapper.Contrib;

/// <summary>
///     Specifies that this is a computed column.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ComputedAttribute : Attribute
{
}