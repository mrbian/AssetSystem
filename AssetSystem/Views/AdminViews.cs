using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Views
{
    class AdminViews : BaseViews
    {
        public AdminViews() :base()
        {
            
        }

        /// <summary>
        /// 进行验证信息的显示和获取
        /// </summary>
        /// <returns></returns>
        public List<String> ShowAuth()
        {
            ShowTitle();
            Console.WriteLine("请输入管理员账户名:");
            String account = Console.ReadLine();   //账户
            Console.WriteLine("请输入管理员密码");
            String password = Console.ReadLine();   //密码
            List<String> adminInfo = new List<string>();  //验证信息存储的List
            adminInfo.Add(account);
            adminInfo.Add(password);
            return adminInfo;
        }

        /// <summary>
        /// 显示管理员的操作选项并且返回管理员输入
        /// </summary>
        /// <returns>用户输入整型Type</returns>
        public int ShowChoose()
        {
            Console.WriteLine("请输入数字选择操作：");
            Console.WriteLine("1、资产类型管理");
            Console.WriteLine("2、资产管理");
            Console.WriteLine("3、人员管理");
            Console.WriteLine("4、修改密码");
            String option = Console.ReadLine();
            int op = Convert.ToInt32(option);
            return op;
        }

        /// <summary>
        /// 打印设备管理操作
        /// </summary>
        /// <returns>用户选择的操作符int</returns>
        public int EquipmentCtrl()
        {
            Console.WriteLine("------设备管理------");
            Console.WriteLine("请输入数字执行您想要进行的操作：");
            Console.WriteLine("1、增加设备");
            Console.WriteLine("2、删除设备");
            Console.WriteLine("3、修改设备信息");
            Console.WriteLine("4、查看设备列表");
            int op = Convert.ToInt32(Console.ReadLine());
            return op;
        }

        /// <summary>
        /// 打印用户管理操作
        /// </summary>
        /// <returns>用户选择的int0操作符</returns>
        public int UserCtrl()
        {
            Console.WriteLine("------用户管理------");
            Console.WriteLine("请输入数字执行您想要进行的操作：");
            Console.WriteLine("1、增加用户");
            Console.WriteLine("2、删除用户");
            Console.WriteLine("3、修改用户信息");
            int op = Convert.ToInt32(Console.ReadLine());
            return op;
        }

        /// <summary>
        /// 打印修改密码
        /// </summary>
        public void ChangePassword()
        {
            Console.WriteLine("------修改密码------");
        }
    }
    
}
