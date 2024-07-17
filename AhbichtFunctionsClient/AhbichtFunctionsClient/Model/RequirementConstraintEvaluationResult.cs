namespace AhbichtFunctionsClient.Model;

public class RequirementConstraintEvaluationResult
{
    public string FormatConstraintsExpression { get; set; }
    public string Hints { get; set; }
    public bool RequirementConstraintsFulfilled { get; set; }
    public bool RequirementIsConditional { get; set; }
}
