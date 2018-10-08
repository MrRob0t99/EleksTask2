
namespace EleksTask
{
    public class Error
    {
        public int ErrorCode { get; }
        public string ErrorDescription { get; set; }

        public Error()
        {
        }

        public Error(string description)
        {
            ErrorDescription = description;
            ErrorCode = 500;
        }

        public Error(int errorCode, string description)
        {
            ErrorDescription = description;
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"Status code: {ErrorCode}, \nDescription:{ErrorDescription}\n";
        }
    }
}