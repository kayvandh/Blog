using Infrastructure;

namespace Blog.Services
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(T data)
        {
            Data = data;
            HasError = false;
        }

        public ServiceResponse(string errorMessage)
        {
            Data = default(T);
            HasError = true;
            ErrorMessage = errorMessage;
        }
        public ServiceResponse(Exception ex)
        {
            Data = default(T);
            HasError = true;
            ErrorMessages = ex.GetInnerExceptions().Select(e => e.Message).ToList();
            ErrorMessage = $"{ex.Message}: {string.Join(", ", ErrorMessages)}";
        }

        public T Data { get; set; }
        public bool HasError { get; set; }
        public bool HasResult => Data != null;
        public string ErrorMessage { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
