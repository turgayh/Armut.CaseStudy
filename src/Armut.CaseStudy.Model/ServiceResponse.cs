namespace Armut.CaseStudy.Model
{
    public class ServiceResponse<T>
    {
        public T Content { get; set; }
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }

    }
}