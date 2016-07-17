using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Adaptor;
using AssetSystem.Controllers.Enum;
using AssetSystem.Views;

namespace AssetSystem.Controllers
{
    class EquipmentTypeController : BaseController
    {
        public EquipmentTypeController():base()
        {
            EquipmentTypeViews = EquipmentTypeViews ?? new EquipmentTypeViews();
            EquipmentTypeAdaptor = EquipmentTypeAdaptor ?? new EquipmentTypeAdaptor();
        }

        public EquipmentTypeViews EquipmentTypeViews;
        public EquipmentTypeAdaptor EquipmentTypeAdaptor;

        public void EquipmentTypeCtrl()
        {
            int op = EquipmentTypeViews.EquipmentTypeCtrl();
            switch (op)
            {
                case (int)EnumEquipmentTypeOperatorOptions.Add:
                    AddEquipmentType(); //添加一个种类
                    break;

                case (int)EnumEquipmentTypeOperatorOptions.Delete:
                    DeleteEquipmentType();
                    break;

                case (int)EnumEquipmentTypeOperatorOptions.PrintAll:
                    PrintAllEquipmentType(); //打印所有种类
                    break;

                case (int)EnumEquipmentTypeOperatorOptions.Exit:
                    //todo 清空这一层占用的内存，节省内存
                    return; //退出执行上一层

                default:
                    break;
            }
            EquipmentTypeCtrl(); //重新展示
        }

        /// <summary>
        /// 添加设备类别
        /// </summary>
        protected void AddEquipmentType()
        {
            PrintAllEquipmentType();  //打印出所有的设备种类
            Dictionary<int,String> dictionary = EquipmentTypeViews.AddEquipmentType(); //获取用户输入结果
            int result;
            if (! dictionary.ContainsKey(2)) //如果添加的是大类
            {
                result = EquipmentTypeAdaptor.AddEquipmentType(
                    dictionary[0],
                    Convert.ToInt32(dictionary[1]),
                    null);
            } 
            else // 如果添加的是小类
            {
                result = EquipmentTypeAdaptor.AddEquipmentType(
                    dictionary[0],
                    Convert.ToInt32(dictionary[1]),
                    Convert.ToInt32(dictionary[2]));
            }
            if (result == 0)
            {
                EquipmentTypeViews.OperatorFail();
            }else if (result == 1)
            {
                EquipmentTypeViews.OperatorSuccess();
            }
        }

        /// <summary>
        /// 删除设备种类
        /// </summary>
        protected void DeleteEquipmentType()
        {
            PrintAllEquipmentType();
            int Id = EquipmentTypeViews.DeleteEquipmentType();
            int result = EquipmentTypeAdaptor.DeleteEquipmentType(Id);
            if (result == 0)
            {
                EquipmentTypeViews.ShowEquipmentTypeHasEquipmentError(); //指定Id的设备大类下有小类，删除失败提示
            }
            else if (result == -1)
            {
                EquipmentTypeViews.ShowIdNullError(); //指定Id的设备类别不存在，删除失败提示
            }
            else if (result == 1)
            {
                EquipmentTypeViews.OperatorSuccess(); //操作成功
            }
            else if (result == -2) //指定Id的设备小类下面有设备，删除失败提示
            {
                EquipmentTypeViews.ShowSmallEquipmentTypeHasEquipmentError();
            }
            else
            {
                throw new MyException("返回值不应该有其他，请修改",5);
            }
        }

        protected void PrintAllEquipmentType()
        {
            EquipmentTypeViews
                .PrintAllEquipmentType(EquipmentTypeAdaptor.GetAllEquipmentTypes());//打印所有的设备种类
        }
    }
}
