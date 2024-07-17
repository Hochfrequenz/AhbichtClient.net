using AhbichtClient;
using AhbichtClient.Model;
using EDILibrary;

namespace AhbichtClientQuickStartApp;

public class MyHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        return new HttpClient
        {
            BaseAddress = new Uri("http://ahbicht.azurewebsites.net")
        };
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var client = new AhbichtRestClient(new MyHttpClientFactory(), new NoAuthenticator());
        var packageMapping = await client.ResolvePackage("10P", EdifactFormat.UTILMD, EdifactFormatVersion.FV2404);
        Console.WriteLine($"The package '10P' is equivalent to {packageMapping.PackageExpression}");
        var conditionMapping = await client.ResolveCondition("244", EdifactFormat.UTILMD, EdifactFormatVersion.FV2404);
        Console.WriteLine($"where '[244]' refers to '{conditionMapping.ConditionText}'");

        const string expression = "Muss ([1] O [2])[951]";
        var categorizedKeyExtract = await client.ExtractKeys(expression);
        Console.WriteLine($"To evaluate the expression '{expression}' you need to provide values for the following keys: " +
                          string.Join(", ", categorizedKeyExtract.RequirementConstraintKeys) + " and " + string.Join(", ", categorizedKeyExtract.FormatConstraintKeys));

        var myResults = new ContentEvaluationResult
        {
            Hints = new Dictionary<string, string?>(),
            FormatConstraints = new Dictionary<string, EvaluatedFormatConstraint>
            {
                {
                    "951", new EvaluatedFormatConstraint
                    {
                        FormatConstraintFulfilled = true
                    }
                }
            },
            RequirementConstraints = new Dictionary<string, ConditionFulfilledValue>()
            {
                { "1", ConditionFulfilledValue.Fulfilled },
                { "2", ConditionFulfilledValue.Unfulfilled }
            }
        };
        var evaluationResult = await client.Evaluate(expression, myResults);
        Console.WriteLine($"The expression '{expression}' is evaluated to: {evaluationResult.RequirementConstraintEvaluationResult.RequirementConstraintsFulfilled}");
    }
    // this prints:
    // The package '10P' is equivalent to [20] ? [244]
    // where '[244]' refers to 'Wenn SG10 CCI+6++ZA9 (Aggreg. verantw. ÜNB) in dieser SG8 vorhanden'
    // To evaluate the expression 'Muss ([1] O [2])[951]' you need to provide values for the following keys: 1, 2 and 951
    // The expression 'Muss ([1] O [2])[951]' is evaluated to: True
}
