namespace MrDAL.Core.Utils;

internal enum ErrorType
{
    UniqueKeyViolation,
    ForeignKeyConflict,
    OtherSqlException,
    OtherException
}