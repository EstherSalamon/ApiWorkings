using JokesApi.Data;
using JokesApi.Data.Migrations;
using JokesApi.Web.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JokesApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly string _connection;

        public JokesController(IConfiguration config)
        {
            _connection = config.GetConnectionString("ConStr");
        }

        [HttpGet("search")]
        public List<Joke> GetAll(int amount, string type)
        {
            

            HttpClient client = new HttpClient();
            List<Joke> toReturn = new List<Joke>();

            if(type == "random")
            {
                List<Joke> result = client.GetFromJsonAsync<List<Joke>>($"https://official-joke-api.appspot.com/jokes/random/{amount}", new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }).Result;

                foreach(var r in result)
                {
                    if(toReturn.All(j => j.Id != r.Id))
                    {
                        toReturn.Add(r);
                    }
                }
            }
            else
            {
                string url = $"https://official-joke-api.appspot.com/jokes/{type}";
                url += "/random";
                for(int i = 0; i < amount; i++)
                {
                    List<Joke> oneAtATime = client.GetFromJsonAsync<List<Joke>>(url, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).Result; //Because the API used only allows searching specific amounts when searching random, not by type

                    if(toReturn.All(j => j.Id != oneAtATime[0].Id))
                    {
                        toReturn.Add(oneAtATime[0]);
                    }
                }

            }

            return toReturn;
        }

        [HttpGet("jokebyid")]
        public Joke GetById(int id)
        {
            HttpClient client = new HttpClient();
            Joke joke = client.GetFromJsonAsync<Joke>($"https://official-joke-api.appspot.com/jokes/{id}", new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }).Result;
            return joke;
        }

        [HttpPost("addtofave")]
        [Authorize]
        public void AddToFaves(Joke joke)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return;
            }

            UserRepository uRepo = new UserRepository(_connection);
            User user = uRepo.GetByEmail(User.Identity.Name);

            FaveJoke fave = new FaveJoke
            {
                JokeId = joke.Id,
                Type = joke.Type,
                SetUp = joke.Setup,
                PunchLine = joke.Punchline,
                UserId = user.Id,
                Note = ""
            };

            JokeRepository jRepo = new JokeRepository(_connection);
            jRepo.AddJoke(fave);
        }

        [HttpPost("remove")]
        [Authorize]
        public void Remove(IntVM vm)
        {
            Console.WriteLine(vm.Int);
            JokeRepository repo = new JokeRepository(_connection);
            repo.RemoveFromFaves(vm.Int);
        }

        [HttpGet("getall")]
        [Authorize]
        public List<FaveJoke> FaveJokes()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return null;
            }

            UserRepository repo = new UserRepository(_connection);
            User user = repo.GetByEmail(User.Identity.Name);

            JokeRepository jRepo = new JokeRepository(_connection);
            return jRepo.GetByUserId(user.Id);
        }

        [HttpPost("update")]
        [Authorize]
        public void EditNote(UpdateVM vm)
        {
            JokeRepository jRepo = new JokeRepository(_connection);
            jRepo.EditNote(vm.Note, vm.JokeId);
        }
    }
}
