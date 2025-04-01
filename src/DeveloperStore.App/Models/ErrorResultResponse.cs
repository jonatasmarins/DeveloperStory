namespace DeveloperStore.App.Models
{
    public abstract class ErrorResultResponse
    {
        public string Type { get; protected set; } = string.Empty;

        public string Error { get; protected set; } = string.Empty;

        public string Detail { get; protected set; } = string.Empty;
    }

    public class ResourceNotFoundResultResponse : ErrorResultResponse
    {
        public ResourceNotFoundResultResponse(string error = "Not Found", string detail = "")
        {
            Type = "ResourceNotFound";
            Error = error;
            Detail = detail;
        }
    }

    public class AuthenticationErrorResultResponse : ErrorResultResponse
    {
        public AuthenticationErrorResultResponse(string error = "Invalid authentication token", string detail = "")
        {
            Type = "AuthenticationError";
            Error = error;
            Detail = detail;
        }
    }

    public class ValidationErrorResultResponse : ErrorResultResponse
    {
        public ValidationErrorResultResponse(string error = "Invalid Input Data", string detail = "")
        {
            Type = "ValidationError";
            Error = error;
            Detail = detail;
        }
    }
}
