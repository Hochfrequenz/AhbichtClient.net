namespace AhbichtFunctionsClient.Model;

public class ContentEvaluationResult
{
    public Dictionary<string, EvaluatedFormatConstraint> FormatConstraints { get; set; }
    public Dictionary<string, string> Hints { get; set; }
    public string Id { get; set; }
    public Dictionary<string, string> Packages { get; set; }
    public Dictionary<string, string> RequirementConstraints { get; set; }
}
