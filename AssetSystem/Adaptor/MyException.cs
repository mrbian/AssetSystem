using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Adaptor
{
    public class MyException : ApplicationException
    {
        public MyException(string message, int errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public int ErrorCode;
    }
}
