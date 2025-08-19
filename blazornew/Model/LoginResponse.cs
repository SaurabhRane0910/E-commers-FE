namespace blazornew.Model
{
    public class LoginMessage
    {
        public string Text { get; set; }
    }

    public class LoginResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }  // changed from string
        public LoginData Data { get; set; }
    }
}
