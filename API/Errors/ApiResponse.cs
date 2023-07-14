namespace API.Errors
{
    // Generates an api error response based on the incoming status code and possibly a message if provided
    public class ApiResponse
    {
        // Constructor uses status code and message if provided to create an api error response using helper method
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        // Helper method that generates a default message if no message was provided
        private static string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Something seems to be wrong with your request",
                401 => "Not authorized to view this resource",
                404 => "Resource not found",
                500 => "Something went wrong on our end",
                _ => "Error code not found",
            };
        }
    }
}