namespace MrDAL.Models.Common;

public enum ResultType
{
    NoError,
    ReferenceConflicted,
    EntityNotExists,
    UniqueValueViolation,
    InternalError,
    FeatureNotImplemented,
    DataAccessError,
    ValidationError,
    InvalidOperation,
    ExternalProviderError,
    InternetConnection
}