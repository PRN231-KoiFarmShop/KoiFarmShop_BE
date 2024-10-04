namespace ks.application.Models.Response
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; } = default;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }
        public static BaseResponse<string> NoContent()
        {
            return new()
            {
                Message = string.Empty,
                IsSuccess = true,
                Code = 204
            };
        }
        public static BaseResponse<T> Success(T Data)
            => new()
            {
                IsSuccess = true,
                Message = string.Empty,
                Data = Data,
                Code = 200
            };
        public static BaseResponse<T> Error(string message)
            => new()
            {
                Message = message,
                Code = 500,
                IsSuccess = false
            };




    }
}
