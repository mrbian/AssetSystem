using System;
using System.Data.Entity;
using System.Globalization;

namespace AssetSystem.Library
{
    public static class Util
    {
        /// <summary>
        /// 生成唯一的数字型ID作为logic_id
        /// </summary>
        /// <returns>长度为6的logic_id</returns>
        public static string GenerateIntId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter
                .ToInt64(buffer,0)
                .ToString()
                .Substring(0,6);
        }
        
        /// <summary>
        /// 字符串转DateTime，字符串统一格式为 yyyy.mm.dd
        /// 例如： 2016.7.18
        /// </summary>
        /// <param name="dateTime">日期字符串</param>
        /// <returns>DateTime格式的日期</returns>
        public static DateTime StringToDateTime(string dateTime)
        {
            DateTime dt;
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy.MM.dd";
            dt = Convert.ToDateTime(dateTime, dtFormat);  //验证输入的日期合法
            return dt;
        }

        /// <summary>
        /// 扩展entityframework 清空一个表中所有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbSet"></param>
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}