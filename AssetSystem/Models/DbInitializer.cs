using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AssetSystem.Library;

namespace AssetSystem.Models
{
    public class DbInitializer
    {
        public DbInitializer()
        {
        }


        public void Seed()
        {
            DeleteAllData();
            AdminSeed();
            UserSeed();
            EquipmentTypeSeed();
            EquipmentSeed();
            HistorySeed();
        }

        
        /// <summary>
        /// 清空所有的数据
        /// </summary>
        public void DeleteAllData()
        {
            using (var dbCtx = new TheContext())
            {
                dbCtx.Histories.Clear();
                dbCtx.Equipments.Clear();
                dbCtx.EquipmentTypes.Clear();
                dbCtx.Users.Clear();
                dbCtx.Admins.Clear();
                dbCtx.SaveChanges();
            }
        }
    
        /// <summary>
        /// 管理员数据Seed
        /// </summary>
        public void AdminSeed()
        {
            Console.WriteLine("管理员Seed");
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

        /// <summary>
        /// 用户数据Seed
        /// </summary>
        public void UserSeed()
        {
            Console.WriteLine("用户Seed");
            using (var dbCtx = new TheContext())
            {
                IList<User> defaultUsers = new List<User>();

                for (var i = 0; i < 3; i++)
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

        /// <summary>
        /// 设备类型Seed
        /// </summary>
        public void EquipmentTypeSeed()
        {
            Console.WriteLine("设备类型Seed");
            using (var dbCtx = new TheContext())
            {
                IList<EquipmentType> bigEquipmentTypes = new List<EquipmentType>();//大类数据Seed

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
                dbCtx.SaveChanges();

                IList<EquipmentType> smallEquipmentTypes = new List<EquipmentType>(); //小类数据Seed

                for (var i = 0; i < 5; i++)
                {
                    smallEquipmentTypes.Add(new EquipmentType()
                    {
                        Title = "小类" + i.ToString(),
                        Type = 1,
                        BigEquipmentType = bigEquipmentTypes[i] //设置小类所属大类的Id
                    });
                }

                foreach (var smallEquipmentType in smallEquipmentTypes)
                {
                    dbCtx.EquipmentTypes.Add(smallEquipmentType);
                }

                dbCtx.SaveChanges();
            }
        }


        /// <summary>
        /// 设备数据Seed
        /// </summary>
        public void EquipmentSeed()
        {
            Console.WriteLine("设备Seed");
            using (var dbCtx = new TheContext())
            {
                IList<EquipmentType> smallEquipmentTypes = dbCtx
                    .EquipmentTypes
                    .Where(et => et.Type == 1)
                    .ToList();
                IList<User> users = dbCtx.Users
                    .ToList();
                var admin = dbCtx.Admins.First();
                foreach (var smallEquipmentType in smallEquipmentTypes)
                {
                    foreach (var user in users)
                    {
                        for (var i = 0; i < 2; i++)
                        {
                            dbCtx.Equipments.Add(new Equipment()
                            {
                                Title = "设备" + i,
                                LogicId = Util.GenerateIntId(),
                                Worth = 20,
                                PurchasingDate = new DateTime(2016, 7, 18).Date,
                                State = 1,
                                Remark = "备注",
                                EquipmentType = smallEquipmentType,
                                User = user,
                                Admin = admin
                            });
                        }
                    }
                }
                dbCtx.SaveChanges();
            }
        }


        /// <summary>
        /// 借记历史Seed
        /// </summary>
        public void HistorySeed()
        {
            Console.WriteLine("借记历史Seed");
        }
    }
}