namespace AhbichtFunctionsClient.Model;

public class FormatConstraintEvaluationResult
{
    [System.Text.Json.Serialization.JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("format_constraints_fulfilled")]
    public bool FormatConstraintsFulfilled { get; set; }
}
