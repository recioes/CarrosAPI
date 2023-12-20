using CarrosAPI.Core.Interfaces.Repositories;
using System.IO;

namespace CarrosAPI.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _csvFilePath;

        public UserRepository(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public async Task <(string Username, string HashedPassword)> GetUserAsync(string username)
        {
            using (var reader = new StreamReader(_csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var linha = await reader.ReadLineAsync();
                    var valores = linha.Split(',');

                    if (valores[0] == username)
                    {
                        return (valores[0], valores[1]);
                    }
                }
            }
            return (null, null);
        }
    }
}

