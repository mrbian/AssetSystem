using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Models;

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
            Clear(); //清空控制台
            Console.WriteLine("------设备种类管理------");
            Console.WriteLine("请输入数字执行您想要进行的操作：");
            Console.WriteLine("1、增加设备种类");
            Console.WriteLine("2、删除设备种类");
            Console.WriteLine("3、查看设备种类列表");
            Console.WriteLine("4、返回上一层");
            int op = Convert.ToInt32(Console.ReadLine());
            return op;
        }

        /// <summary>
        /// 打印所有的设备种类
        /// </summary>
        /// <param name="equipmentTypes">设备种类的List数据集</param>
        public void PrintAllEquipmentType(List<EquipmentType> equipmentTypes)
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("类别Id   |  类别名称 |  类别类型  |  小类所属大类Id");
            foreach (EquipmentType equipmentType in equipmentTypes)
            {
                Console.WriteLine(equipmentType.Id + " | " + 
                    equipmentType.Title + "  |  " + 
                    equipmentType.Type.ToString() + " |  " +
                    (equipmentType.BigEquipmentType == null ? "NULL" : equipmentType.BigEquipmentType.Id.ToString()  ));
            }
            Pause();
        }

        /// <summary>
        /// 添加设备种类
        /// </summary>
        /// <returns>
        /// 字典数组
        /// 0 : 名称
        /// 1 ： 类型
        /// 2 ： 小类所属大类的Id
        /// </returns>
        public Dictionary<int,String> AddEquipmentType()
        {
            Console.WriteLine("请输入要添加类别的名称：");
            String title = Console.ReadLine();
            Console.WriteLine("请输入要添加类别的类型：（0代表大类，1代表小类）");
            int type = Convert.ToInt32(Console.ReadLine());
            Dictionary<int,String> dictionary = new Dictionary<int, String>();
            if (type == 1)
            {
                Console.WriteLine("请输入小类所属大类的Id");
                int bigEquipmentTypeId = Convert.ToInt32(Console.ReadLine());
                dictionary.Add(2,bigEquipmentTypeId.ToString());
            }
            dictionary.Add(0,title);
            dictionary.Add(1,type.ToString());
            return dictionary;
        }

        /// <summary>
        /// 读取用户删除一个设备种类的Id
        /// </summary>
        /// <returns>用户输入的Id</returns>
        public int DeleteEquipmentType()
        {
            Console.WriteLine("请输入要删除类别的Id:");
            int Id = Convert.ToInt32(Console.ReadLine());
            return Id;
        }

        /// <summary>
        /// 指定Id的设备种类不存在错误打印
        /// </summary>
        public void ShowIdNullError()
        {
            Console.WriteLine("指定Id的设备种类不存在");
            Pause();
        }

        /// <summary>
        /// 删除设备大类下有设备小类的错误提示
        /// </summary>
        public void ShowEquipmentTypeHasEquipmentError()
        {
            Console.WriteLine("指定设备大类下有设备小类，请先删除对应设备小类");
            Pause();
        }

        /// <summary>
        /// 删除设备小类下设备时的错误提示
        /// </summary>
        public void ShowSmallEquipmentTypeHasEquipmentError()
        {
            Console.WriteLine("指定设备小类下有设备，请先删除对应设备");
            Pause();
        }
    }
}
