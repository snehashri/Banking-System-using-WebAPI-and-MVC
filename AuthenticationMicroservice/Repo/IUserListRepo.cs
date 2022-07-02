using AuthenticationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Repo
{
    public interface IUserListRepo
    {
        public User getUserById(int id);
    }
}
