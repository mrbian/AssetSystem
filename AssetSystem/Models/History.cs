using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetSystem.Models
{
    public class History
    {
        public History()
        {
            
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ReturnDate { get; set; }
        public string EquipmentTitle { get; set; }
    }
}