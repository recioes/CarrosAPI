using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrosAPI.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task <(string Username, string HashedPassword)> GetUserAsync(string username);
    }
}
