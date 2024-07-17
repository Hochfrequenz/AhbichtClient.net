namespace AhbichtFunctionsClient.Model;

public class EvaluatedFormatConstraint
{
    [System.Text.Json.Serialization.JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("format_constraint_fulfilled")]
    public bool FormatConstraintFulfilled { get; set; }
}
