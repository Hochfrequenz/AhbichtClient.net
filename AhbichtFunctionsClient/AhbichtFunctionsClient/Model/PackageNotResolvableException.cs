namespace AhbichtFunctionsClient.Model;

/// <summary>
/// raised when a package is unresolvable
/// </summary>
public class PackageNotResolvableException: ArgumentException
{
    public string PackageKey {  get; private set; }

    public PackageNotResolvableException(string packageKey, string? message = null) : base(message)
    {
        PackageKey = packageKey;
    }
}

/// <summary>
/// raised when a condition is unresolvable
/// </summary>
public class ConditionNotResolvableException: ArgumentException
{
    public string ConditionKey {  get; private set; }

    public ConditionNotResolvableException(string conditionKey, string? message = null) : base(message)
    {
        ConditionKey = conditionKey;
    }
}

/// <summary>
/// raised when a categorized key extract cannot be created (most likely because of malformed input)
/// </summary>
public class CategorizedKeyExtractError: ArgumentException
{
    public string Expression {  get; private set; }

    public CategorizedKeyExtractError(string expression, string? message = null) : base(message)
    {
        Expression = expression;
    }
}
