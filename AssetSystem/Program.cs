using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Controllers;

namespace AssetSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 设置控制台颜色
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Cyan;
            #endregion

            #region 初始化登录

            ControllerCtx ctrlCtx = new ControllerCtx();
            ctrlCtx.GetAdminController().Auth();

            #endregion
        }
    }
}
