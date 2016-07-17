using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Controllers
{
    class BaseController
    {
        public BaseController()
        {
            CtrlCtx = CtrlCtx ?? new ControllerCtx(); //防止创建多个上下文对象造成内存泄漏
        }

        protected ControllerCtx CtrlCtx;
    }
}
