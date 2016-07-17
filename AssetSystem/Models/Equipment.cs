using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetSystem.Models
{
    public class Equipment
    {
        public Equipment()
        {
            
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string LogicId { get; set; }
        public string Title { get; set; }
        public double Worth { get; set; }

        public DateTime PurchasingDate { get; set; }
        
        /// <summary>
        /// 设备的状态
        /// 0 : 报废
        /// 1 : 正常
        /// 2 : 维修中
        /// </summary>
        public int State { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
        public virtual EquipmentType EquipmentType { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual User User { get; set; }
    }
}