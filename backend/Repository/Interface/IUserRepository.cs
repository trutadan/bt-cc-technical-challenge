using technical_challenge.Model;
using technical_challenge.Model.DTO;

namespace technical_challenge.Repository.Interface
{
    public interface IUserRepository
    {
        /// <summary>
        /// This method get user by its id
        /// </summary>
        /// <param name="id">user id guid</param>
        /// <returns>found user else throw repository exception</returns>
        public Task<User> GetUserById(int id);

        /// <summary>
        /// This method get user data by its username
        /// </summary>
        /// <param name="username">user name string</param>
        /// <returns>User with associated data</returns>
        public Task<User> GetUserByUsername(string username);

        /// <summary>
        /// This method add a new user in database
        /// </summary>
        /// <param name="user">register credentials</param>
        /// <returns>added user in database</returns>
        public Task<User> AddUser(RegisterCredentials user);
    }
}
