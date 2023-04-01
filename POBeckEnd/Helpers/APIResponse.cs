namespace POBeckEnd.Helpers
{
    public class APIResponse
    {
        public APIResponse(int code, string status, string message, object? data)
        {
            this.code = code;
            this.status = status;
            this.message = message;
            this.data = data;
        }

        public int code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public object? data { get; set; }
    }
}
