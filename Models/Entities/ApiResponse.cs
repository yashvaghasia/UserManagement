
namespace UserManagement.Models.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        internal static object? ErrorResponse(string v)
        {
            throw new NotImplementedException();
        }

        internal static object? SuccessResponse(User user)
        {
            throw new NotImplementedException();
        }
    }
}
