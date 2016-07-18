using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Controllers.Enum;
using AssetSystem.Library;
using AssetSystem.Models;

namespace AssetSystem.Views
{
    class EquipmentViews : BaseViews
    {
        public EquipmentViews() : base()
        {
            
        }

        /// <summary>
        /// 打印并返回用户输入的值
        /// </summary>
        /// <returns>整型的用户输入的操作符</returns>
        public int EquipmentCtrl()
        {
            Console.Clear();
            Console.WriteLine("++++++设备信息管理++++++");
            Console.WriteLine("请输入数字选择你要进行的操作：");
            Console.WriteLine("1、添加设备");
            Console.WriteLine("2、删除设备");
            Console.WriteLine("3、修改设备");
            Console.WriteLine("4、打印所有设备");
            Console.WriteLine("5、退出到上一层");
            int op = Convert.ToInt32(Console.ReadLine());
            return op;
        }

        /// <summary>
        /// 打印添加设备所必须的提示并返回用户输入的数据
        /// </summary>
        /// <returns>
        /// 字典：
        /// 0 : 设备名称
        /// 1 : 设备价格
        /// 2 : 设备购买日期
        /// 3 : 设备备注
        /// 4 : 设备所属小类的Id
        /// </returns>
        public Dictionary<int, String> AddEquipement()
        {
            Dictionary<int,String> dictionary = new Dictionary<int, String>();

            Console.WriteLine("请输入要添加的设备的名称：");
            string title = Console.ReadLine();   //用户输入的设备名称
            dictionary.Add(0,title);

            Console.WriteLine("请输入要添加设备的价格：");
            string input = Console.ReadLine();  //用户输入的价格
            double worth = Convert.ToDouble(input);   //验证输入的价格合法
            dictionary.Add(1,input);

            Console.WriteLine("请输入设备的购买日期（格式范例：2016.7.10）");
            string dt = Console.ReadLine(); //获取用户输入日期
            DateTime dateTime = Util.StringToDateTime(dt);
            dictionary.Add(2,dt);

            Console.WriteLine("请输入对设备的备注");
            input = Console.ReadLine();  //获取用户输入的备注
            string remark = input;
            dictionary.Add(3,remark);

            Console.WriteLine("请输入设备所属的设备种类");
            input = Console.ReadLine();  //获取用户输入的设备种类的Id
            int Id = Convert.ToInt32(input); //验证输入的Id为整型合法
            dictionary.Add(4,input);
            return dictionary;
        }

        /// <summary>
        /// 打印删除一个设备，获得用户输入的要删除的设备的Id
        /// </summary>
        /// <returns>要删除的设备的Id</returns>
        public int DeleteEquipment()
        {
            Console.WriteLine("请输入要删除的设备的Id:");
            int Id = Convert.ToInt32(Console.ReadLine());
            return Id;
        }

        /// <summary>
        /// 打印修改一个设备，获得用户输入的想要修改的设备的Id
        /// </summary>
        /// <returns>要修改的设备的Id</returns>
        public int UpdateEquipment()
        {
            Console.WriteLine("请输入你想要修改的设备的Id:");
            int Id = Convert.ToInt32(Console.ReadLine());
            return Id;
        }

        /// <summary>
        /// 打印修改一个设备的各项数据
        /// </summary>
        /// <param name="equipment">要修改的equipment实例</param>
        /// <returns>
        /// 修改后的Equipment实例
        /// </returns>
        public Equipment UpdateEquipementById(Equipment equipment)
        {
            Console.WriteLine("请输入设备的标题(留空则不修改)：" + equipment.Title );
            string title = Console.ReadLine();
            equipment.Title = title == "" ? equipment.Title : title;

            Console.WriteLine("请输入设备的价格（留空则不修改）" + equipment.Worth);
            string worth = Console.ReadLine();
            equipment.Worth = worth == "" ? equipment.Worth : Convert.ToDouble(worth);
            
            Console.WriteLine("请输入设备的购买日期（格式为2016.7.18，留空不做修改）" + equipment.PurchasingDate);
            string dateTime = Console.ReadLine();
            equipment.PurchasingDate = dateTime == "" ? equipment.PurchasingDate : Util.StringToDateTime(dateTime);

            Console.WriteLine("请输入设备的状态（留空不做修改）" + equipment.State);
            string state = Console.ReadLine();
            equipment.State = state == "" ? equipment.State : Convert.ToInt32(state);

            Console.WriteLine("请输入对设备的备注(留空不做修改)" + equipment.Remark);
            string remark = Console.ReadLine();
            equipment.Remark = remark == "" ? equipment.Remark : remark;

            return equipment;
        }

        /// <summary>
        /// 格式化打印数据库中所有设备
        /// </summary>
        /// <param name="equipments">List型设备数据</param>
        public void PrintAllEquipments(List<Equipment> equipments)
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("设备Id  |  设备名称  |   设备逻辑Id  |   设备价格  |   设备购买日期   |  设备状态  |  备注  | 设备所属设备小类的Id");
            foreach (var equipment in equipments)
            {
                Console.WriteLine(equipment.Id + "   |   " + 
                    equipment.Title + "    |    " + 
                    equipment.LogicId + "    |      " + 
                    equipment.Worth + "   |    " +
                    equipment.PurchasingDate + "   |   " + 
                    (equipment.State == 1 ? "正常" : equipment.State == 0 ? "报废" :
                    equipment.State == 2 ? "维修中" : "其他") + "   |   " + 
                    equipment.Remark + "    |    " + 
                    equipment.EquipmentType.Id);
            }
            Pause();
        }
    }
}
