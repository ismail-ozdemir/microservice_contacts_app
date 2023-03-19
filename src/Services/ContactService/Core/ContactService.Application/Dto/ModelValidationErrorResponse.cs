namespace ContactService.Application.Dto
{
    public class ModelValidationErrorResponse
    {
        public ModelValidationErrorResponse(List<FieldError> validationErrors, string message)
        {
            ValidationErrors = validationErrors.ToArray();
            Message = message;
        }

        public FieldError[] ValidationErrors { get; set; }
        public string Message { get; set; }
  
    }
    public class FieldError
    {
        public string Field { get; set; }
        public object Value { get; set; }
        public string Error { get; set; }
    }
}
