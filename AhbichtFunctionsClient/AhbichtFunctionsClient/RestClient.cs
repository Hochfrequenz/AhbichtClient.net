using System.Net;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;
using AhbichtFunctionsClient.Model;
using EDILibrary;

namespace AhbichtFunctionsClient;

/// <summary>
/// a client for the ahbicht-functions REST API
/// </summary>
public class AhbichtFunctionsRestClient : IPackageKeyToConditionResolver, IConditionKeyToTextResolver, ICategorizedKeyExtractor
{
    private readonly IAhbichtAuthenticator _authenticator;
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    /// <summary>
    /// Provide the constructor with a http client factory.
    /// It will create a client from said factory and use the <paramref name="httpClientName"/> for that.
    /// </summary>
    /// <param name="httpClientFactory">factory to create the http client from</param>
    /// <param name="authenticator">something that tells you whether and how you need to authenticate yourself at ahbichtfunctions</param>
    /// <param name="httpClientName">name used to create the client</param>
    public AhbichtFunctionsRestClient(IHttpClientFactory httpClientFactory, IAhbichtAuthenticator authenticator, string httpClientName = "AhbichtFunctions")
    {
        _httpClient = httpClientFactory.CreateClient(httpClientName);
        if (_httpClient.BaseAddress == null)
        {
            throw new ArgumentNullException(nameof(httpClientFactory), $"The http client factory must provide a base address for the client with name '{httpClientName}'");
        }

        _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
    }

    /// <summary>
    /// Make sure that the client is authenticated, if necessary
    /// </summary>
    private async Task EnsureAuthentication()
    {
        if (_authenticator.UseAuthentication())
        {
            var token = await _authenticator.Authenticate(_httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    /// <summary>
    /// tests if ahbicht backend is available
    /// </summary>
    /// <remarks>
    /// Note that this does _not_ check if you're authenticated.
    /// The method will probably throw an <see cref="HttpRequestException"/> if the host cannot be found.
    /// </remarks>
    /// <returns>
    /// Returns true iff the ahbicht backend/ahbicht function is available under the configured base address.
    /// </returns>
    public async Task<bool> IsAvailable()
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress!);
        // note that this is available without any authentication
        // see e.g. https://ahbicht.azurewebsites.net/ or https://localhost:7071
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        var content = await response.Content.ReadAsStringAsync();
        return content.Contains("Your Functions 3.0 app is up and running");
    }

    public async Task<PackageKeyConditionExpressionMapping> ResolvePackage(string packageKey, EdifactFormat format, EdifactFormatVersion formatVersion)
    {
        if (packageKey is null)
        {
            throw new ArgumentNullException(nameof(packageKey));
        }

        if (!packageKey.EndsWith("P"))
        {
            throw new ArgumentException($"The package key '{packageKey}' has to end with 'P'");
        }

        var uriBuilder = new UriBuilder(_httpClient!.BaseAddress!)
        {
            Path = $"/api/ResolvePackageConditionExpression/{formatVersion}/{format}/{packageKey}"
        };

        var convertUrl = uriBuilder.Uri.AbsoluteUri;
        await EnsureAuthentication();
        var httpResponse = await _httpClient.GetAsync(convertUrl);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.Unauthorized when !_authenticator.UseAuthentication():
                    throw new AuthenticationException($"Did you correctly set up the {nameof(IAhbichtAuthenticator)}?");
                default:
                    throw new HttpRequestException($"Could not map package key '{packageKey}'; Status code: {httpResponse.StatusCode} / {responseContent}");
            }
        }
        var responseBody = JsonSerializer.Deserialize<PackageKeyConditionExpressionMapping>(responseContent!, _jsonSerializerOptions);
        if (responseBody?.PackageKey is null && responseBody?.PackageExpression is null)
        {
            // the bad thing is: the backend returns a success status code with a error body
            var errorBody = JsonSerializer.Deserialize<ErrorResponse>(responseContent!, _jsonSerializerOptions);
            throw new PackageNotResolvableException(packageKey, $"The package key '{packageKey}' could not be found: {errorBody.ErrorMessage}");
        }
        return responseBody!;
    }
    
    public async Task<ConditionKeyConditionTextMapping> ResolveCondition(string conditionKey, EdifactFormat format, EdifactFormatVersion formatVersion)
    {
        if (conditionKey is null)
        {
            throw new ArgumentNullException(nameof(conditionKey));
        }

        if (conditionKey.Contains("[")||conditionKey.Contains("]"))
        {
            throw new ArgumentException($"The condition key '{conditionKey}' must not contain square brackets");
        }

        var uriBuilder = new UriBuilder(_httpClient!.BaseAddress!)
        {
            Path = $"/api/ResolveConditionText/{formatVersion}/{format}/{conditionKey}"
        };

        var convertUrl = uriBuilder.Uri.AbsoluteUri;
        await EnsureAuthentication();
        var httpResponse = await _httpClient.GetAsync(convertUrl);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.Unauthorized when !_authenticator.UseAuthentication():
                    throw new AuthenticationException($"Did you correctly set up the {nameof(IAhbichtAuthenticator)}?");
                default:
                    throw new HttpRequestException($"Could not map condition key '{conditionKey}'; Status code: {httpResponse.StatusCode} / {responseContent}");
            }
        }
        var responseBody = JsonSerializer.Deserialize<ConditionKeyConditionTextMapping>(responseContent!, _jsonSerializerOptions);
        if (responseBody?.ConditionKey is null && responseBody?.ConditionText is null)
        {
            // the bad thing is: the backend returns a success status code with a error body
            var errorBody = JsonSerializer.Deserialize<ErrorResponse>(responseContent!, _jsonSerializerOptions);
            throw new ConditionNotResolvableException(conditionKey, $"The condition key '{conditionKey}' could not be found: {errorBody.ErrorMessage}");
        }
        return responseBody!;
    }

    public async Task<CategorizedKeyExtract> ExtractKeys(string expression)
    {
        if (expression is null||string.IsNullOrWhiteSpace(expression))
        {
            throw new ArgumentNullException(nameof(expression));
        }

        var uriBuilder = new UriBuilder(_httpClient!.BaseAddress!)
        {
            Path = "/api/CategorizedKeyExtract",
            Query = $"expression={expression}"
        };

        var convertUrl = uriBuilder.Uri.AbsoluteUri;
        await EnsureAuthentication();
        var httpResponse = await _httpClient.GetAsync(convertUrl);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.Unauthorized when !_authenticator.UseAuthentication():
                    throw new AuthenticationException($"Did you correctly set up the {nameof(IAhbichtAuthenticator)}?");
                default:
                    throw new HttpRequestException($"Could not parse expression '{expression}'; Status code: {httpResponse.StatusCode} / {responseContent}");
            }
        }
        var responseBody = JsonSerializer.Deserialize<CategorizedKeyExtract>(responseContent!, _jsonSerializerOptions);
        if (responseBody?.RequirementConstraintKeys is null && responseBody?.FormatConstraintKeys is null)
        {
            // the bad thing is: the backend returns a success status code with a error body
            var errorBody = JsonSerializer.Deserialize<ErrorResponse>(responseContent!, _jsonSerializerOptions);
            throw new CategorizedKeyExtractError(expression, $"The condition key '{expression}' could not be found: {errorBody.ErrorMessage}");
        }
        return responseBody!;
    }
}
