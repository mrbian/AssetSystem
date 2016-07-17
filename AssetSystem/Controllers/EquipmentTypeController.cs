using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Adaptor;
using AssetSystem.Controllers.Enum;
using AssetSystem.Views;

namespace AssetSystem.Controllers
{
    class EquipmentTypeController : BaseController
    {
        public EquipmentTypeController():base()
        {
            EquipmentTypeViews = EquipmentTypeViews ?? new EquipmentTypeViews();
            EquipmentTypeAdaptor = EquipmentTypeAdaptor ?? new EquipmentTypeAdaptor();
        }

        public EquipmentTypeViews EquipmentTypeViews;
        public EquipmentTypeAdaptor EquipmentTypeAdaptor;

        public void EquipmentTypeCtrl()
        {
            int op = EquipmentTypeViews.EquipmentTypeCtrl();
            switch (op)
            {
                case (int)OperatorOptions.Add:
                    break;

                case (int)OperatorOptions.Delete:
                    break;

                case (int)OperatorOptions.Update:
                    break;

                case (int)OperatorOptions.Search:
                    break;
            }
        }

        
    }
}
