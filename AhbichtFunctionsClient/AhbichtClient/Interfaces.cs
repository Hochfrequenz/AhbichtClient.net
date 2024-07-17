using AhbichtClient.Model;
using EDILibrary;

namespace AhbichtClient;

/// <summary>
/// Interface of all the things that can evaluate expressions
/// </summary>
/// <remarks>This will be useful if you want to mock the client elsewhere</remarks>
public interface IContentEvaluator
{
    /// <summary>
    /// given an ahb condition expression and information about which conditions is fulfilled, evaluate the expression as whole
    /// </summary>
    public Task<AhbExpressionEvaluationResult> Evaluate(string ahbExpression, ContentEvaluationResult contentEvaluationResult);
}

/// <summary>
/// Interface of all the things that can extract conditions keys from a given expression
/// </summary>
/// <remarks>This will be useful if you want to mock the client elsewhere</remarks>
public interface ICategorizedKeyExtractor
{
    /// <summary>
    /// given an ahb condition expression and information about which conditions is fulfilled, evaluate the expression as whole
    /// </summary>
    public Task<CategorizedKeyExtract> ExtractKeys(string expression);
}

/// <summary>
/// Interface of all the things that can map a condition key to a human-readable text
/// </summary>
/// <remarks>This will be useful if you want to mock the client elsewhere</remarks>
public interface IConditionKeyToTextResolver
{
    /// <summary>
    /// given a condition key, returns the text that is associated with it
    /// </summary>
    /// <raises><see cref="ConditionNotResolvableException"/> if the condition key is not resolvable</raises>
    public Task<ConditionKeyConditionTextMapping> ResolveCondition(string conditionKey, EdifactFormat format, EdifactFormatVersion formatVersion);
}

/// <summary>
/// Interface of all the things that can map a package key to a condition string
/// </summary>
/// <remarks>This will be useful if you want to mock the client elsewhere</remarks>
public interface IPackageKeyToConditionResolver
{
    /// <summary>
    /// given a package key, returns the condition that is associated with it
    /// </summary>
    /// <raises><see cref="PackageNotResolvableException"/> if the package key is not resolvable</raises>
    public Task<PackageKeyConditionExpressionMapping> ResolvePackage(string packageKey, EdifactFormat format, EdifactFormatVersion formatVersion);
}


/// <summary>
/// Can provide information on whether you need to authenticate against transformer.bee and how
/// </summary>
public interface IAhbichtAuthenticator
{
    /// <summary>
    /// returns true iff the client should use authentication
    /// </summary>
    /// <returns></returns>
    public bool UseAuthentication();

    /// <summary>
    /// provides the token to authenticate against transformer.bee (if and only if <see cref="UseAuthentication"/> is true)
    /// </summary>
    /// <returns></returns>
    public Task<string> Authenticate(HttpClient client);
}
