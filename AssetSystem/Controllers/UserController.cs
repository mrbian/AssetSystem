using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Adaptor;
using AssetSystem.Controllers.Enum;
using AssetSystem.Models;
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

        public void UserCtrl()
        {
            int op = UserViews.UserCtrl();
            switch (op)
            {
                case (int)EnumUserOpOptions.PrintAll: //打印所有用户信息
                    PrintAllUser();
                    break;
                case (int)EnumUserOpOptions.Exit: //退出到上一层
                    return;
                default:
                    break;
            }
            UserCtrl();
        }

        /// <summary>
        /// 打印所有用户
        /// </summary>
        public void PrintAllUser()
        {
            List<User> users = UserAdaptor.GetAllUser(); //得到所有用户
            UserViews.PrintAllUser(users); //打印所有用户
        }
    }
}
