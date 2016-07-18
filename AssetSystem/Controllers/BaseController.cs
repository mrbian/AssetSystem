using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Library;
using AssetSystem.Models;

namespace AssetSystem.Controllers
{
    class BaseController
    {
        public BaseController()
        {
            CtrlCtx = CtrlCtx ?? new ControllerCtx(); //todo 这里对象的潜拷贝有问题，无法复制_admin，而一元运算符=又不能重载
        }

        protected ControllerCtx CtrlCtx;
    }
}
