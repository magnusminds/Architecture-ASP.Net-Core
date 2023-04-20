using Newtonsoft.Json;


namespace Architecture.Dto.APIResponse
{
    public class ArchitectureAPIResponse
    {
        public string Version
        {
            get;
            set;
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StatusCode
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public string MessageCode
        {
            get;
            set;
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsError
        {
            get;
            set;
        }

        public object ResponseException
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }

        [JsonConstructor]
        public ArchitectureAPIResponse(string message, string messageCode, object result = null, int statusCode = 200, string apiVersion = "1.0.0.0")
        {
            StatusCode = statusCode;
            Message = message;
            MessageCode = messageCode;
            Result = result;
            Version = apiVersion;
        }

        public ArchitectureAPIResponse(object result, int statusCode = 200)
        {
            StatusCode = statusCode;
            Result = result;
        }

        public ArchitectureAPIResponse(int statusCode, object apiError)
        {
            StatusCode = statusCode;
            ResponseException = apiError;
            IsError = true;
        }

        public ArchitectureAPIResponse()
        {
        }
    }
}
