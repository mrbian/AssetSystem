using System.Collections.Generic;
using System.Data.Entity;
using AssetSystem.Library;

namespace AssetSystem.Models
{
    public class DbInitializer
    {
        public DbInitializer()
        {
            Util = new Util();
        }

        public Util Util;

        public void Seed()
        {
            AdminSeed();
            UserSeed();
        }

    
        public void AdminSeed()
        {
            using (var dbCtx = new TheContext())
            {
             
                IList<Admin> defaultAdmins = new List<Admin>();

                for (var i = 0; i < 10; i++)
                {
                    defaultAdmins.Add(new Admin()
                    {
                        Account = "admin" + i.ToString(),
                        Password = "123456",
                        Type = 2
                    });
                }

                foreach (Admin admin in defaultAdmins)
                {
                    dbCtx.Admins.Add(admin);
                }

                dbCtx.SaveChanges();
            }
        }

        public void UserSeed()
        {
            using (var dbCtx = new TheContext())
            {
                IList<User> defaultUsers = new List<User>();

                for (var i = 0; i < 10; i++)
                {
                    defaultUsers.Add(new User()
                    {
                        Name = "user" + i.ToString(),
                        LogicId = Util.GenerateIntId(),
                        Position = "职位" + i,
                        Remark = "备注" + i
                    });
                }

                foreach (User user in defaultUsers)
                {
                    dbCtx.Users.Add(user);
                }
                dbCtx.SaveChanges();
            }
        }

        public void EquipmentTypeSeed()
        {
            using (var dbCtx = new TheContext())
            {
                IList<EquipmentType> bigEquipmentTypes = new List<EquipmentType>();

                for (var i = 0; i < 5; i++)
                {
                    bigEquipmentTypes.Add(new EquipmentType()
                    {
                        Title = "大类" + i.ToString(),
                        Type = 0
                    });                    
                }

                foreach (var bigEquipmentType in bigEquipmentTypes)
                {
                    dbCtx.EquipmentTypes.Add(bigEquipmentType);
                }

                IList<EquipmentType> smallEquipmentTypes = new List<EquipmentType>();

                for (var i = 0; i < 5; i++)
                {
                    smallEquipmentTypes.Add(new EquipmentType()
                    {
                        Title = "小类" + i.ToString(),
                        Type = 1
                    });
                }

                foreach (var smallEquipmentType in smallEquipmentTypes)
                {
                        
                }
            }
        }

        public void EquipmentSeed()
        {
            
        }

        public void HistorySeed()
        {
            
        }
    }
}