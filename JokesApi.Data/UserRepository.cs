namespace JokesApi.Data
{
    public class UserRepository
    {
        private readonly string _connection;

        public UserRepository(string connection)
        {
            _connection = connection;
        }

        public void AddUser(User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            using FavoritesDataContext context = new FavoritesDataContext(_connection);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User Login(string email, string password)
        {
            User user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool verifyPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!verifyPassword)
            {
                return null;
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            using FavoritesDataContext context = new FavoritesDataContext(_connection);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool EmailExists(string email)
        {
            using FavoritesDataContext context = new FavoritesDataContext(_connection);
            return context.Users.Any(u => u.Email == email);
        }
    }
}
