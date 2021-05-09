using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Entities
{
    public sealed class UserEntity
    {
        public UserEntity(int userid,
                                  string userName)
        {
            UserId = userid;
            UserName = userName;
        }
        public int UserId { get; }
        public string UserName { get; }
    }
}
