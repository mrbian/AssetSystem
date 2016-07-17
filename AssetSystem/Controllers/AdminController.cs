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
    class AdminController : BaseController
    {
        public AdminController() : base()
        {
            AdminViews = AdminViews ?? new AdminViews();
            AdminAdaptor = AdminAdaptor ?? new AdminAdaptor();
        }

        public AdminAdaptor AdminAdaptor;
        public AdminViews AdminViews;

        //get Auth
        public void Auth()
        {
            List<String> adminInfo = AdminViews.ShowAuth(); //显示验证信息并且得到用户输入
            var admin = AdminAdaptor.AdminLogin(adminInfo[0],adminInfo[1]);
            if (admin == null)
            {
                AdminViews.ShowLoginError();
                Auth();
            }
            Choose();
        }

        public void Choose()
        {
            int op = AdminViews.ShowChoose();
            switch (op)
            {
                case (int)ChooseOptions.EquipmentTypeCtrl:
                    CtrlCtx.GetEquipmentTypeController().EquipmentTypeCtrl();
                    break;

                case (int)ChooseOptions.EquipmentCtrl:
                    EquipmentCtrl();
                    break;

                case (int)ChooseOptions.UserCtrl:
                    UserCtrl();
                    break;

                case (int)ChooseOptions.ChangePassword:
                    ChangePassword();
                    break;

                default:
                    AdminViews.ShowChooseError();
                    Choose();
                    break;
            }
        }


        

        public void EquipmentCtrl()
        {
            int op = AdminViews.EquipmentCtrl();
        }

        public void UserCtrl()
        {
            int op = AdminViews.UserCtrl();
        }

        public void ChangePassword()
        {
            AdminViews.ChangePassword();
        }
    }
}
