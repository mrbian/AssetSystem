using System;
using System.ComponentModel.DataAnnotations;

namespace AssetSystem.Models
{
    public class History
    {
        public History()
        {
            
        }

        [Key]
        public int Id { get; set; }
        public DateTime ReturnDate { get; set; }
        public string EquipmentTitle { get; set; }
    }
}