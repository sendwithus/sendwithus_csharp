﻿using System;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus LogEvent class
    /// </summary>
    public class LogEvent
    {
        public string Object { get; set; } // capitalized because "object" is a C# datatype (lower case in API docs)
        public Int64 created { get; set; }
        public string status { get; set; }
        public string status_message { get; set; }

        /// <summary>
        /// Default constructor for a log event
        /// </summary>
        public LogEvent()
        {
            Object = String.Empty;
            created = 0;
            status = String.Empty;
            status_message = String.Empty;
        }
    }
}
