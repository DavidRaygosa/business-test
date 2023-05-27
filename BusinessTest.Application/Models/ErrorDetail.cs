namespace BusinessTest.Application.Models
{
    using System.Text.Json;

    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Stack { get; set; }
        public string? Origin { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class ResponseDetail<T>
    {
        public T? Result { get; set; }
        public ErrorDetail? Error { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}