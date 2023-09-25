
using System.Text;
using CarrosAPI.Core.Interfaces.Repositories;
using CarrosAPI.Core.Interfaces.Services;
using CarrosAPI.Core.Models.Auth;
using System.Security.Cryptography;


namespace CarrosAPI.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AuthenticateAsync(UserInfo userInfo)
        {
            var (storedUsername, storedHashedPassword) = await _userRepository.GetUserAsync(userInfo.Username);
            var incomingHashedPassword = HashPassword(userInfo.Password);

            if (storedUsername != null && storedHashedPassword.ToUpper() == incomingHashedPassword.ToUpper())
            {
                return true;
            }
            return false;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }
    }

}

