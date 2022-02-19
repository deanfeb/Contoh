namespace Contoh.Microservice.Employee.Models
{
    public class ResponseBase<T>
    {
        public SuccessType Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }

    public enum SuccessType
    {
        Failed = 0,
        Success = 1
    }
}
