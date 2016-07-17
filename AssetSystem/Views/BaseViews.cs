using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Views
{
    class BaseViews
    {
        //todo 对用户的输入合法进行严格验证
        public BaseViews()
        {
            
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        public void OperatorSuccess()
        {
            Console.WriteLine("操作成功");
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        public void OperatorFail()
        {
            Console.WriteLine("操作失败");
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
