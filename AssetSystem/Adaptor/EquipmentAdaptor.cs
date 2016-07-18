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

        public int UpdateEquipment(Equipment equipment)
        {
            DbCtx.Entry(equipment).State = EntityState.Modified;
            DbCtx.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 得到数据库中的所有的设备
        /// </summary>
        /// <returns>List型设备数据</returns>
        public List<Equipment> GetAllEquipments()
        {
            return DbCtx.Equipments.ToList();
        }
    }
}
