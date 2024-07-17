namespace AhbichtFunctionsClient.Model;

public class ContentEvaluationResult
{
    [System.Text.Json.Serialization.JsonPropertyName("format_constraints")]
    public Dictionary<string, EvaluatedFormatConstraint> FormatConstraints { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("hints")]
    public Dictionary<string, string> Hints { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("packages")]
    public Dictionary<string, string> Packages { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("requirement_constraints")]
    public Dictionary<string, ConditionFulfilledValue> RequirementConstraints { get; set; }
}
