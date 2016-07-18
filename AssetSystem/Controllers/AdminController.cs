using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        private static Admin _admin;

        public void Login(Admin admin)
        {
            _admin = admin;
        }

        public Admin GetCurrentAdmin()
        {
            return _admin;
        }

        public AdminController() : base()
        {
            AdminViews = AdminViews ?? new AdminViews();
            AdminAdaptor = AdminAdaptor ?? new AdminAdaptor();
        }

        public AdminAdaptor AdminAdaptor;
        public AdminViews AdminViews;

        //todo : 管理员存储放在这里还是崩溃
        
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
            Login(admin);  //管理登录，上下文保存当前登录的管理员的信息
            Choose();
        }

        /// <summary>
        /// 对用户的选择进行判断并分发到指定Controller
        /// </summary>
        public void Choose()
        {
            int op = AdminViews.ShowChoose(GetCurrentAdmin());
            switch (op)
            {
                #region 调用设备种类Controller对设备种类进行管理
                case (int)ChooseOptions.EquipmentTypeCtrl:
                    CtrlCtx.GetEquipmentTypeController().EquipmentTypeCtrl();
                    break;
                #endregion
                #region 调用设备Controller对设备进行管理
                case (int)ChooseOptions.EquipmentCtrl:
                    CtrlCtx.GetEquipmentController().EquipmentCtrl();
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
