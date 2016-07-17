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

        /// <summary>
        /// 对用户的选择进行判断并分发到指定Controller
        /// </summary>
        public void Choose()
        {
            int op = AdminViews.ShowChoose();
            switch (op)
            {
                #region 调用设备种类Controller对设备种类进行管理
                case (int)ChooseOptions.EquipmentTypeCtrl:
                    CtrlCtx.GetEquipmentTypeController().EquipmentTypeCtrl();
                    break;
                #endregion
                #region 调用设备Controller对设备进行管理
                case (int)ChooseOptions.EquipmentCtrl:
                    break;
                #endregion
                #region 调用用户Controller对用户进行管理
                case (int)ChooseOptions.UserCtrl:
                    break;
                #endregion
                #region 修改Admin的账户密码
                case (int)ChooseOptions.ChangePassword:
                    break;
                #endregion
                //都不满足则让用户重新选取
                default:
                    AdminViews.ShowChooseError();
                    break;
            }
            Choose(); //执行完毕后重新调用执行
        }

    }
}
