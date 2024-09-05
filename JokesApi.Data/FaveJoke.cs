using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesApi.Data
{
    public class FaveJoke
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string SetUp { get; set; }
        public string PunchLine { get; set; }
        public int JokeId { get; set; }
        public string Note { get; set; }
    }
}
