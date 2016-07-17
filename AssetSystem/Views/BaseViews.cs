using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Views
{
    class BaseViews
    {
        public BaseViews()
        {
            
        }

        /// <summary>
        /// 显示管理系统的标题
        /// </summary>
        public void ShowTitle()
        {
            Console.WriteLine("++++++资产管理系统++++++");
        }

        /// <summary>
        /// 显示登录信息错误
        /// </summary>
        public void ShowLoginError()
        {
            Console.WriteLine("账户或者密码错误");
        }

        /// <summary>
        /// 显示用户选择错误
        /// </summary>
        public void ShowChooseError()
        {
            Console.WriteLine("非法输入");
        }
    }
}
