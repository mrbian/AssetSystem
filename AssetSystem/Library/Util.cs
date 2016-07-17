using System;

namespace AssetSystem.Library
{
    public class Util
    {
        public Util()
        {
            
        }

        /// <summary>
        /// 生成唯一的数字型ID作为logic_id
        /// </summary>
        /// <returns>长度为6的logic_id</returns>
        public string GenerateIntId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter
                .ToInt64(buffer,0)
                .ToString()
                .Substring(0,6);
        }
    }
}