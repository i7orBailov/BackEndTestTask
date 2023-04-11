namespace BackEndTestTask.Models
{
    public class ResponseMessageBase
    {
        public ResponseMessageBase(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public ResponseMessageBase(bool isSuccessful, ExceptionTemplate exception) 
            : this(isSuccessful)
        {
            ExceptionData = exception;
        }

        public bool IsSuccessful { get; set; }
        public ExceptionTemplate ExceptionData { get; set; }
    }
}