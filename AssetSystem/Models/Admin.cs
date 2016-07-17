using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetSystem.Models
{
    public class Admin
    {
        public Admin()
        {
            
        }

        [Key]
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}