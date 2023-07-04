using Microsoft.EntityFrameworkCore;
using technical_challenge.Exceptions;
using technical_challenge.Model;
using technical_challenge.Model.DTO;
using technical_challenge.Repository.Interface;

namespace technical_challenge.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext.UserDatabaseContext _databaseContext;

        public UserRepository(DatabaseContext.UserDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _databaseContext.Set<User>().FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                throw new RepositoryException("User does not exist!");
            }

            return user;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _databaseContext.Set<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new RepositoryException("User does not exist!");
            }

            return user;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _databaseContext.Set<User>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new RepositoryException("User does not exist!");
            }

            return user;
        }

        public async Task<User> AddUser(RegisterCredentials user)
        {
            if (user is null)
            {
                throw new RepositoryException("Invalid user");
            }

            // Check if a user with the same username or email already exists
            bool userExists = await _databaseContext.Set<User>()
                .AnyAsync(u => u.Username == user.Username || u.Email == user.Email);

            if (userExists)
            {
                throw new RepositoryException("Username or email is already taken");
            }

            if (user.Password != user.ConfirmPassword)
            {
                throw new RepositoryException("Passwords do not match");
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            var addedUser = await _databaseContext.Set<User>().AddAsync(new User
            {
                Email = user.Email,
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt)
            });

            if (_databaseContext.Entry(addedUser.Entity).State != EntityState.Added)
            {
                throw new RepositoryException("User was not saved successfully!");
            }

            await _databaseContext.SaveChangesAsync();

            return addedUser.Entity;
        }

    }
} 
