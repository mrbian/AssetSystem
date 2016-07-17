using System.Data.Entity;
using System.Linq;
using AssetSystem.Models;

namespace AssetSystem.Adaptor
{
    public class AdminAdaptor : BaseAdaptor
    {
        public AdminAdaptor() : base()
        {
            
        }
        
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns>如果用户账户密码错误，返回null</returns>
        public Admin AdminLogin(string account, string password)
        {
            var admin = this.DbCtx.Admins
                .FirstOrDefault(a => a.Account == account && a.Password == password);
            return admin;
        }

        /// <summary>
        /// 管理员修改密码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>返回修改密码后的Admin，如果不存在或者旧密码错误，则抛出错误</returns>
        public Admin AdminChangePassword(string account, string oldPassword, string newPassword)
        {
            var admin = this.DbCtx.Admins
                .FirstOrDefault(a => a.Account == account && a.Password == oldPassword);
            if (admin == null)
            {
                throw new MyException("越权操作,不存在此用户或者密码错误", -1);
            }
            admin.Password = newPassword;
            this.DbCtx.Entry(admin).State = EntityState.Modified;
            DbCtx.SaveChanges();
            return admin;
        }
    }
}