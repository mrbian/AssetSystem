using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Models;

namespace AssetSystem.Views
{
    class UserViews : BaseViews
    {
        public UserViews() : base()
        {
            
        }

        public int UserCtrl()
        {
            Clear();
            Console.WriteLine("+++++++用户信息管理++++++++");
            Console.WriteLine("请输入数字选择你要进行的操作：");
            Console.WriteLine("1、打印所有用户信息");
            Console.WriteLine("2、返回上一层");
            int op = Convert.ToInt32(Console.ReadLine());
            return op;
        }

        public void PrintAllUser(List<User> users)
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("用户Id  |  逻辑Id  |   姓名  |   职位  |   备注");
            foreach (var user in users)
            {
                Console.WriteLine(user.Id + "   |   " +
                    user.LogicId + "   |   " +
                    user.Name + "   |   " +
                    user.Position + "   |   " +
                    user.Remark);
            }
            Pause();
        }
    }
}
