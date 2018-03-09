namespace Sendwithus
{
    /// <summary>
    /// sendwithus SnippetDefinition class
    /// </summary>
    public class SnippetDefinition
    {
        public string name { get; set; }
        public string body { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SnippetDefinition(string name, string body)
        {
            this.name = name;
            this.body = body;
        }
    }
}
