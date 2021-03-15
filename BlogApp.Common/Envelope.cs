using System;

namespace BlogApp.Common
{
    public class Envelope
    {
        public object Result { get; }
        public string ErrorMessage { get; }
        public DateTime Timestamp { get; }

        public Envelope(object result, string errorMessage)
        {
            Result = result;
            ErrorMessage = errorMessage;
            Timestamp = DateTime.UtcNow;
        }

        public static Envelope Ok(object result = null)
        {
            return new Envelope(result, null);
        }
        
        public static Envelope Error(string errorMessage)
        {
            return new Envelope(null, errorMessage);
        }
    }
}