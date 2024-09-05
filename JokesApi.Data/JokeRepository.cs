using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace JokesApi.Data
{
    public class JokeRepository
    {
        private readonly string _connection;

        public JokeRepository(string connection)
        {
            _connection = connection;
        }

        public List<FaveJoke> GetByUserId(int id)
        {
            using FavoritesDataContext context = new FavoritesDataContext(_connection);
            return context.FaveJokes.Where(j => j.UserId == id).ToList();
        }

        public void AddJoke(FaveJoke joke)
        {
            using FavoritesDataContext context = new FavoritesDataContext(_connection);
            context.FaveJokes.Add(joke);
            context.SaveChanges();
        }

        public void RemoveFromFaves(int id)
        {
            using FavoritesDataContext context = new FavoritesDataContext(_connection);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM FaveJokes WHERE JokeId = {id}");
        }

        public void EditNote(string note, int id)
        {
            using FavoritesDataContext context = new FavoritesDataContext(_connection);
            context.Database.ExecuteSqlInterpolated($"UPDATE FaveJokes SET Note = {note} WHERE jokeId = {id}");
        }
    }
}
