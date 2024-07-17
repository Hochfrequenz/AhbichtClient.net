namespace AhbichtFunctionsClient.Model;

internal class ErrorResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("error")]
    public string ErrorMessage { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public int Code { get; set; }
}
