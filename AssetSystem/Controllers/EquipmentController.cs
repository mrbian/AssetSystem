using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Adaptor;
using AssetSystem.Controllers.Enum;
using AssetSystem.Library;
using AssetSystem.Models;
using AssetSystem.Views;

namespace AssetSystem.Controllers
{
    class EquipmentController:BaseController
    {
        public EquipmentController() : base()
        {
            EquipmentViews = EquipmentViews ?? new EquipmentViews();
            EquipmentAdaptor = EquipmentAdaptor ?? new EquipmentAdaptor();
        }

        public EquipmentViews EquipmentViews;
        public EquipmentAdaptor EquipmentAdaptor;

        /// <summary>
        /// 处理用户的对设备的操作并分发
        /// </summary>
        public void EquipmentCtrl()
        {
            int op = EquipmentViews.EquipmentCtrl();
            switch (op)
            {
                case (int)EnumEquipmentOpOptions.Add: //添加一个设备
                    AddEquipment();
                    break;
                case (int)EnumEquipmentOpOptions.Delete: //删除某一设备
                    DeleteEquipment();
                    break;
                case (int)EnumEquipmentOpOptions.Update: //修改某一设备
                    UpdateEquipment();
                    break;
                case (int)EnumEquipmentOpOptions.PrintAll: //打印所有设备数据
                    PrintAllEquipment();
                    break;
                case (int)EnumEquipmentOpOptions.Exit:  //退出，函数返回
                    return;
                default:
                    EquipmentViews.OperatorFail(); //打印操作失败
                    break;
            }
            EquipmentCtrl(); //持续调用自身
        }

        /// <summary>
        /// 添加一个设备
        /// </summary>
        protected void AddEquipment()
        {
            CtrlCtx
                .GetEquipmentTypeController()
                .PrintAllEquipmentType(); //调用设备种类Controller的公共方法打印所有的设备种类

            Dictionary<int, string> dictionary = EquipmentViews.AddEquipement(); //调用view里面的添加设备方法得到用户输入
            string title = dictionary[0];  //设备标题
            double worth = Convert.ToDouble(dictionary[1]); //设备价格
            DateTime dateTime = Util.StringToDateTime(dictionary[2]); //设备购买日期
            string remark = dictionary[3]; //备注
            int equipmentTypeId = Convert.ToInt32(dictionary[4]); //设备所属小类的Id
            Admin admin = CtrlCtx.GetAdminController().GetCurrentAdmin();
            int result = EquipmentAdaptor
                .AddEquipment(title, worth, dateTime, remark, equipmentTypeId,
                admin.Id); //调用数据库方法添加设备
            if (result == 0) //因为设备种类Id不合法而失败
            {
                EquipmentViews.OperatorFail(); //打印失败
            }else if (result == 1)  //操作成功
            {
                EquipmentViews.OperatorSuccess(); //打印成功
            }
            else
            {
                throw new MyException("不应有其他返回值",3);
            }
        }

        /// <summary>
        /// 删除一个设备
        /// </summary>
        protected void DeleteEquipment()
        {
            PrintAllEquipment(); //打印所有的设备列表
            int Id = EquipmentViews.DeleteEquipment();
            int result = EquipmentAdaptor.DeleteEquipment(Id);
            if (result == 0)
            {
                EquipmentViews.OperatorFail();
            }else if (result == 1)
            {
                EquipmentViews.OperatorSuccess();
            }
            else
            {
                throw new MyException("不应有其他的返回结果",0);
            }
        }

        /// <summary>
        /// 开始修改一个设备
        /// </summary>
        protected void UpdateEquipment()
        {
            PrintAllEquipment();
            int Id = EquipmentViews.UpdateEquipment(); //调用视图获取用户要修改的设备的Id
            Equipment equipment = EquipmentAdaptor.GetEquipmentById(Id); //通过Id得到设备
            equipment = EquipmentViews.UpdateEquipementById(equipment); //调用视图获取用户的修改结果
            int result = EquipmentAdaptor.UpdateEquipment(equipment); //更新设备
            EquipmentViews.OperatorSuccess(); //显示操作成功
        }
        

        /// <summary>
        /// 打印所有设备
        /// </summary>
        protected void PrintAllEquipment()
        {
            List<Equipment> equipments = EquipmentAdaptor.GetAllEquipments(); //得到所有的设备数据
            EquipmentViews.PrintAllEquipments(equipments);
        }
    }
}
