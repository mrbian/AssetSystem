using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Views
{
    class EquipmentTypeViews : BaseViews
    {
        public EquipmentTypeViews() :base()
        {
            
        }

        /// <summary>
        /// 打印设备种类管理备选操作
        /// </summary>
        /// <returns>用户选择的操作符int</returns>
        public int EquipmentTypeCtrl()
        {
            Console.WriteLine("------设备种类管理------");
            Console.WriteLine("请输入数字执行您想要进行的操作：");
            Console.WriteLine("1、增加设备种类");
            Console.WriteLine("2、删除设备种类");
            Console.WriteLine("3、查看设备种类列表");
            int op = Convert.ToInt32(Console.ReadLine());
            return op;
        }
    }
}
