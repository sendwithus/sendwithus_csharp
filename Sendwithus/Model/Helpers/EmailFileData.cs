using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// A class to represent inline data for an email
    /// </summary>
    public class EmailFileData
    {
        public string id { get; set; }
        public string data { get; set; }

        public EmailFileData()
        {
            id = String.Empty;
            data = String.Empty;
        }

        /// <summary>
        /// Constructor for the file data
        /// </summary>
        /// <param name="id">The id of the file data</param>
        /// <param name="data">The file data, as a base 64 encoded string</param>
        public EmailFileData(string id, string data)
        {
            this.id = id;
            this.data = data;
        }
    }
}
