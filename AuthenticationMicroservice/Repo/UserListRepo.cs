using AuthenticationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Repo
{
    public class UserListRepo : IUserListRepo
    {
        private readonly UserContext context;
        public UserListRepo(UserContext _context)
        {
            context = _context;
        }
        public User getUserById(int id)
        {
            return context.Users.Find(id);
            
        }

        internal object getUserById()
        {
            throw new NotImplementedException();
        }
    }
}
