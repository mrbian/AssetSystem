using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Adaptor;
using AssetSystem.Views;

namespace AssetSystem.Controllers
{
    class UserController : BaseController
    {
        public UserController() : base()
        {
            UserViews = UserViews ?? (new UserViews());
            UserAdaptor = UserAdaptor ?? (new UserAdaptor());
        }

        public UserViews UserViews;
        public UserAdaptor UserAdaptor;
    }
}
