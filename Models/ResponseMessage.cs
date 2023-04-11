namespace BackEndTestTask.Models
{
    public class ResponseMessage<T> : ResponseMessageBase
    {
        public ResponseMessage(bool isSuccessful, T data) : base(isSuccessful)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
