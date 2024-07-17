namespace AhbichtFunctionsClient.Model;

public class RequirementConstraintEvaluationResult
{
    [System.Text.Json.Serialization.JsonPropertyName("format_constraints_expression")]
    public string FormatConstraintsExpression { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("hints")]
    public string Hints { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("requirement_constraints_fulfilled")]
    public bool RequirementConstraintsFulfilled { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("requirement_is_conditional")]
    public bool RequirementIsConditional { get; set; }
}
