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
                case (int)EnumEquipmentOpOptions.Find: //调用按条件查找
                    FindEquipment();
                    break;
                case (int)EnumEquipmentOpOptions.Borrow: //借用某一设备
                    BorrowEquipment();
                    break;
                case (int)EnumEquipmentOpOptions.Return: //归还某一设备
                    ReturnEquipment();
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

        /// <summary>
        /// 打印出所给出的数据而不是数据库中的全部
        /// </summary>
        /// <param name="equipments">要打印的数据集</param>
        protected void PrintSelectEquipment(List<Equipment> equipments)
        {
            EquipmentViews.PrintAllEquipments(equipments);
        }

        /// <summary>
        /// 按条件查找所有设备开始
        /// </summary>
        protected void FindEquipment()
        {
            int op = EquipmentViews.FindEquipment();
            FindEquipmentByCondition(op);
        }

        /// <summary>
        /// 根据条件查找设备控制器
        /// </summary>
        /// <param name="op">整型数字选择对应操作</param>
        protected void FindEquipmentByCondition(int op)
        {
            List<Equipment> equipments = new List<Equipment>();
            switch (op)
            {
                case (int)EnumFindCondition.ByBigType: //通过大类浏览
                    CtrlCtx.GetEquipmentTypeController().PrintAllEquipmentType(); //先打印所有的设备种类让用户选择
                    int bigTypeId = EquipmentViews.FindEquipmentByBigType(); //获得大类Id
                    equipments = EquipmentAdaptor.FindEquipmentsByBigType(bigTypeId);
                    break;

                case (int)EnumFindCondition.BySmallType: //通过小类浏览
                    CtrlCtx.GetEquipmentTypeController().PrintAllEquipmentType(); //先打印所有的设备种类让用户选择
                    int smallTypeId = EquipmentViews.FindEquipmentBySmallType(); //获得小类Id
                    equipments = EquipmentAdaptor.FindEquipmentsBySmallType(smallTypeId);
                    break;

                case (int)EnumFindCondition.ByLogicId: //通过编号查询
                    PrintAllEquipment(); //先打印出所有的数据
                    string logicId = EquipmentViews.FindEquipmentByLogicId();
                    equipments = EquipmentAdaptor.FindEquipmentsByLogicId(logicId);
                    break;

                case (int)EnumFindCondition.ByUser: //通过用户查询
                    CtrlCtx.GetUserController().PrintAllUser(); //先打印所有的用户
                    int userId = EquipmentViews.FindEquipmentByUserId();
                    equipments = EquipmentAdaptor.FindEquipmentByUserId(userId);
                    break;

                default:
                    break;
            }
            PrintSelectEquipment(equipments);
        }

        protected void BorrowEquipment()
        {
            CtrlCtx.GetUserController().PrintAllUser(); //先打印所有的用户数据
            CtrlCtx.GetEquipmentController().PrintAllEquipment(); //再打印所有的设备数据
            Dictionary<int,int> dictionary = EquipmentViews.BorrowEquipment(); //得到用户输入的用户Id和要被领用的设备Id
            int result = EquipmentAdaptor.BorrowEquipment(dictionary[0], dictionary[1]);
            if (result == 1)
            {
                EquipmentViews.OperatorSuccess();
            }else if (result == 0)
            {
                EquipmentViews.OperatorFail(); 
            }else if (result == -1)
            {
                EquipmentViews.ShowEquipmentHasBeenBorrowError(); //指定设备已经被占用
            }else if (result == -2)
            {
                EquipmentViews.ShowEquipmentCanNotBeBorrowError(); //设备维修中或已经损坏
            }
            else
            {
                throw new MyException("程序员的错误，请处理",0);
            }
        }

        protected void ReturnEquipment()
        {
            CtrlCtx.GetEquipmentController().PrintAllEquipment(); //先打印所有的设备数据
            int equipmentId = EquipmentViews.ReturnEquipment(); //获得要被归还的设备的Id
            string adminAccount = CtrlCtx.GetAdminController().GetCurrentAdmin().Account; //得到当前用户的账号
            int result = EquipmentAdaptor.ReturnEquipment(equipmentId, adminAccount);
            if (result == 1)
            {
                EquipmentViews.OperatorSuccess();
            }else if (result == 0)
            {
                EquipmentViews.OperatorFail();
            }else if (result == -1)
            {
                EquipmentViews.ShowEquipmentNotBorrowError(); //指定设备未被领用
            }
            else
            {
                throw new MyException("程序员的错误，请处理",0);
            }
        }
    }
}
