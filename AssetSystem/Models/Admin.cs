using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetSystem.Models
{
    public class Admin
    {
        public Admin()
        {
            
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}