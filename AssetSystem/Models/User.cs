using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetSystem.Models
{
    public class User
    {
        public User()
        {
            
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LogicId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

    }
}