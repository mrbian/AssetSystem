using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Models;

namespace AssetSystem.Adaptor
{
    class EquipmentTypeAdaptor : BaseAdaptor
    {
        public EquipmentTypeAdaptor() :base()
        {
            
        }

        /// <summary>
        /// 添加设备类别
        /// </summary>
        /// <param name="equipmentTypeName">设备类别名称</param>
        /// <param name="type">0代表大类 1代表小类</param>
        /// <param name="bigEquipmentTypeId">大类的id，可空</param>
        /// <returns>
        /// 0：添加小类时其所属大类不存在，需要处理
        /// 1：添加成功
        /// </returns>
        /// todo : 修改返回值为一个JSON，内部既包含对象，又包含状态值
        public int AddEquipmentType(String equipmentTypeName,int type,int? bigEquipmentTypeId)
        {
            if (type == 1 && bigEquipmentTypeId == null)
            {
                throw new MyException("添加小类但未提供大类id",2);
            }
            if (type == 0)
            {
                DbCtx.EquipmentTypes.Add(new EquipmentType()
                {
                    Title = equipmentTypeName,
                    Type = 0
                });
            }
            else if (type == 1)
            {
                var bigEquipmentType = DbCtx.EquipmentTypes
                    .FirstOrDefault(et => et.Id == bigEquipmentTypeId);
                if (bigEquipmentType == null)
                {
                    return 0;
                }
                DbCtx.EquipmentTypes.Add(new EquipmentType()
                {
                    Title = equipmentTypeName,
                    Type = 1,
                    BigEquipmentType =  bigEquipmentType
                });
            }
            else
            {
                throw new MyException("只有大类和小类，请限制用户输入的type",3);
            }
            DbCtx.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 删除一个设备类别
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// -2 : 指定Id的设备小类下面有设备
        /// -1 : 指定Id的设备种类不存在
        /// 0 : 指定Id的设备大类下有小类
        /// 1 : 删除成功
        /// </returns>
        public int DeleteEquipmentType(int id)
        {
            var equipmentType = DbCtx.EquipmentTypes
                .FirstOrDefault( et => et.Id == id);
            if (equipmentType == null)
            {
                return -1;
            }
            if (equipmentType.Type == 0)
            {
                IList<EquipmentType> equipmentTypes = DbCtx.EquipmentTypes
                .Where(et => et.BigEquipmentType.Id == equipmentType.Id)
                .ToList(); //查找当前大类下面是否有小类
                if (equipmentTypes.Count != 0)
                {
                    return 0;
                }
            }else if (equipmentType.Type == 1)
            {
                IList<Equipment> equipments = DbCtx.Equipments
                    .Where(e => e.EquipmentType.Id == equipmentType.Id)
                    .ToList(); //查找当前小类下面是否有设备
                if (equipments.Count != 0)
                {
                    return -2;
                }
            }
            else
            {
                throw new MyException("应该只有大类和小类，请检查数据库",6);
            }
            DbCtx.Entry(equipmentType).State = EntityState.Deleted;
            DbCtx.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 得到所有的设备种类
        /// </summary>
        /// <returns>List类型的设备种类数据</returns>
        public List<EquipmentType> GetAllEquipmentTypes()
        {
            List<EquipmentType> equipmentTypes = new List<EquipmentType>();
            equipmentTypes = DbCtx.EquipmentTypes.ToList();
            return equipmentTypes;
        }
        
    }
}
