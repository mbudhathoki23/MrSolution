using System;

namespace MrDAL.Lib.Dapper.Contrib;

/// <summary>
///     Specifies that this field is a primary key in the database
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class KeyAttribute : Attribute
{
}