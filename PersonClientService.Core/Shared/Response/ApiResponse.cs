namespace PersonClientService.Core.Shared.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public ApiResponse()
        {
        }
        public ApiResponse(T data)
        {
            Success = true;
            Message = null;
            Data = data;
        }
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }
        public ApiResponse(bool success, string? message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

}
