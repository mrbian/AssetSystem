using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetSystem.Models
{
    public class EquipmentType
    {
        public EquipmentType()
        {
            
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 设备资产的标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 设备资产的类型
        /// 0 ： 大类
        /// 1 ： 小类
        /// </summary>
        public int Type { get; set; }

        public virtual EquipmentType BigEquipmentType { get; set; }
        public virtual ICollection<EquipmentType> SmallEquipmentTypes { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}