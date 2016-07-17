using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetSystem.Models
{
    public class User
    {
        public User()
        {
            
        }

        [Key]
        public int Id { get; set; }
        public string LogicId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

    }
}