using System.Collections.Generic;
using System.Linq;
using AssetSystem.Models;

namespace AssetSystem.Adaptor
{
    public class UserAdaptor:BaseAdaptor
    {
        public UserAdaptor() : base()
        {
            
        }

        public List<User> GetAllUser()
        {
            return DbCtx.Users.ToList();
        }
    }
}