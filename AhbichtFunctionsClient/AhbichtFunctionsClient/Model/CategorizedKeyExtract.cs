namespace AhbichtFunctionsClient.Model;

public class CategorizedKeyExtract
{
    [System.Text.Json.Serialization.JsonPropertyName("format_constraint_keys")]
    public List<string> FormatConstraintKeys { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("hint_keys")]
    public List<string> HintKeys { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("package_keys")]
    public List<string> PackageKeys { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("requirement_constraint_keys")]
    public List<string> RequirementConstraintKeys { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("time_condition_keys")]
    public List<string> TimeConditionKeys { get; set; }
}
