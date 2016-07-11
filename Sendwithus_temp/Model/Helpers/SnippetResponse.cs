using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus SnippetResponse class
    /// </summary>
    public class SnippetResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public Snippet snippet { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SnippetResponse()
        {
            success = false;
            status = String.Empty;
            snippet = new Snippet();
        }
    }
}
