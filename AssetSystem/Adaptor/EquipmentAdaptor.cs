using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Library;
using AssetSystem.Models;
using AssetSystem.Views;

namespace AssetSystem.Adaptor
{
    class EquipmentAdaptor : BaseAdaptor
    {
        public EquipmentAdaptor() :base()
        {
            
        }

        /// <summary>
        /// 添加一个设备
        /// </summary>
        /// <param name="title">设备的标题</param>
        /// <param name="worth">设备的价格</param>
        /// <param name="dateTime">设备的购买日期</param>
        /// <param name="remark">备注</param>
        /// <param name="equipmentTypeId">设备所属小类的Id</param>
        /// <param name="adminId">当前管理员的Id</param>
        /// <returns>
        /// 0 : 输入的设备种类Id不合法,
        /// 1 : 操作成功
        /// </returns>
        public int AddEquipment(string title,double worth,DateTime dateTime,string remark,int equipmentTypeId,int adminId)
        {
            var equipmentType = DbCtx.EquipmentTypes
                .FirstOrDefault(et => et.Id == equipmentTypeId);
            if (equipmentType == null || equipmentType.Type == 0)
            {
                return 0;
            }
            var admin = DbCtx.Admins
                .FirstOrDefault(a => a.Id == adminId); //根据adminId得到admin，不能直接传进来admin给这个DbCtx用！！！
            if (admin == null)
            {
                throw new MyException("此处需要有管理操作",-1);
            }
            string logicId = Util.GenerateIntId();
            DbCtx.Equipments.Add(new Equipment()
            {
                LogicId = logicId,
                Title = title,
                Worth = worth,
                PurchasingDate = dateTime,
                Remark = remark,
                State = 1,   //默认正常
                EquipmentType = equipmentType,
                User = null,
                Admin = admin
            });
            DbCtx.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 删除一个设备
        /// </summary>
        /// <param name="Id">设备的Id</param>
        /// <returns>
        /// 0 : 对应Id的设备不存在
        /// 1 : 操作成功
        /// </returns>
        public int DeleteEquipment(int Id)
        {
            var equipment = DbCtx.Equipments
                .FirstOrDefault(e => e.Id == Id);
            if (equipment == null) //此Id不存在
            {
                return 0;  //返回0
            }
            DbCtx.Entry(equipment).State = EntityState.Deleted;
            DbCtx.SaveChanges();
            return 1;
        }


        /// <summary>
        /// 根据Id返回对应设备
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>查找到的设备，可为空</returns>
        public Equipment GetEquipmentById(int Id)
        {
            return DbCtx.Equipments
                .FirstOrDefault(e => e.Id == Id);
        }

        /// <summary>
        /// 更新所给equipment
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        public int UpdateEquipment(Equipment equipment)
        {
            DbCtx.Entry(equipment).State = EntityState.Modified;
            DbCtx.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 根据大类查找设备
        /// </summary>
        /// <param name="bigTypeId"></param>
        /// <returns>设备List数组</returns>
        public List<Equipment> FindEquipmentsByBigType(int bigTypeId)
        {
            List<Equipment> equipments = new List<Equipment>();
            var bigEquipmentType = DbCtx.EquipmentTypes
                .FirstOrDefault(et => et.Id == bigTypeId);
            if (bigEquipmentType == null || bigEquipmentType.Type == 1) //如果没有或者是小类，返回空
            {
                return equipments; //返回空
            }
            List<EquipmentType> smallEquipmentTypes = DbCtx.EquipmentTypes
                .Where(et => et.BigEquipmentType.Id == bigTypeId)
                .ToList(); //根据大类Id获得所有小类
            if (smallEquipmentTypes.Count == 0) //如果没有小类
            {
                return equipments; //返回空
            }
            foreach (var smallEquipment in smallEquipmentTypes) //向结果中加入每个小类下的设备
            {
                equipments.AddRange(DbCtx.Equipments
                    .Where(e => e.EquipmentType.Id == smallEquipment.Id)
                    .ToList()); 
            }
            return equipments;
        }

        /// <summary>
        /// 根据小类的Id查找
        /// </summary>
        /// <param name="smallTypeId"></param>
        /// <returns>查找的List结果集</returns>
        public List<Equipment> FindEquipmentsBySmallType(int smallTypeId)
        {
            List<Equipment> equipments = new List<Equipment>();
            var smallEquipmentType = DbCtx.EquipmentTypes
                .FirstOrDefault(et => et.Id == smallTypeId);
            if (smallEquipmentType == null || smallEquipmentType.Type == 0) //如果没有或者是大类，返回空List
            {
                return equipments;
            }
            equipments = DbCtx.Equipments
                .Where(e => e.EquipmentType.Id == smallTypeId)
                .ToList(); //按照小类查找设备
            return equipments;
        }

        /// <summary>
        /// 按照逻辑Id查找
        /// </summary>
        /// <param name="logicId">设备的逻辑Id</param>
        /// <returns>查询的结果List</returns>
        public List<Equipment> FindEquipmentsByLogicId(string logicId)
        {
            List<Equipment> equipments = new List<Equipment>();
            equipments.Add(DbCtx.Equipments
                .FirstOrDefault(e => e.LogicId == logicId));
            return equipments;
        }

        /// <summary>
        /// 根据用户的Id查询并返回结果集
        /// </summary>
        /// <param name="userId">用户的Id</param>
        /// <returns>查询结果</returns>
        public List<Equipment> FindEquipmentByUserId(int userId)
        {
            List<Equipment> equipments = new List<Equipment>();
            equipments = DbCtx.Equipments
                .Where(e => e.User.Id == userId)
                .ToList();
            return equipments;
        }

        /// <summary>
        /// 得到数据库中的所有的设备
        /// </summary>
        /// <returns>List型设备数据</returns>
        public List<Equipment> GetAllEquipments()
        {
            return DbCtx.Equipments.ToList();
        }

        /// <summary>
        /// 领用某一设备
        /// </summary>
        /// <param name="userId">要领用设备的用户Id</param>
        /// <param name="equipmentId">被领用设备的Id</param>
        /// <returns>
        /// 1 : 操作成功
        /// 0 : 没有对应的user或者没有对应的设备
        /// -1 : 设备已经被占用
        /// -2 : 设备在维修或已经报废
        /// </returns>
        public int BorrowEquipment(int userId, int equipmentId)
        {
            var user = DbCtx.Users
                .FirstOrDefault(u => u.Id == userId); //得到用户
            var equipment = DbCtx.Equipments
                .FirstOrDefault(e => e.Id == equipmentId); //得到设备
            if (user == null || equipment == null)
            {
                return 0;
            }
            if (equipment.User != null) //设备被占用
            {
                return -1;
            }
            if (equipment.State != 0) //设备在维修或者报废
            {
                return -2;
            }
            equipment.User = user; //修改用户为指定用户
            DbCtx.Entry(equipment).State = EntityState.Modified;
            DbCtx.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 设备的归还
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns>
        /// 0 : 指定Id的设备不存在
        /// 1 : 操作成功
        /// -1 : 指定设备未被领用
        /// </returns>
        public int ReturnEquipment(int equipmentId,string adminAccount)
        {
            var equipment = DbCtx.Equipments
                .FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null)
            {
                return 0;
            }
            if (equipment.User == null)
            {
                return -1;
            }
            DbCtx.Histories.Add(new History()
            {
                ReturnDate = DateTime.Now,
                EquipmentTitle = equipment.Title,
                UserName = equipment.User.Name,
                AdminAccount = adminAccount
            }); //添加归还记录

            equipment.User = null;  //删除当前领用用户
            DbCtx.Entry(equipment).State = EntityState.Modified;
            DbCtx.SaveChanges();
            return 1;
        }
    }
}
