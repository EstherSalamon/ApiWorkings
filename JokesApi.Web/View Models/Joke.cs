using System.Collections;

namespace JokesApi.Web.View_Models
{
    public class Joke
    {
        public string Type { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }
        public int Id { get; set; }
    }
}
