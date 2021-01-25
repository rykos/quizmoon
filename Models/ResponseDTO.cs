namespace quizmoon.Models
{
    public struct ResponseDTO
    {
        public ResponseDTO(string type, string message)
        {
            Type = type;
            Message = message;
        }

        public static ResponseDTO Success(string msg)
        {
            return new ResponseDTO("Success", msg);
        }

        public static ResponseDTO Error(string msg)
        {
            return new ResponseDTO("Error", msg);
        }

        public string Type { get; set; }
        public string Message { get; set; }
    }
}