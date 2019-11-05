using Dapper.FastCrud;
using Models;
using MUP.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MUP.API.Services
{
    public class UserService : BaseService
    {
        public Task<IEnumerable<User>> GetUser(string username,string password)
        {
            return this.Connection.FindAsync<User>(s => s.Where($"Username = @username AND Password = @password").WithParameters(new { username, password }));
        }
        public Task<int> UpdateUser(User user)
        {
            return this.Connection.BulkUpdateAsync(user);
        }
    }
}