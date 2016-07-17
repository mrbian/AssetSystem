using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Controllers
{
    class ControllerCtx
    {
        public ControllerCtx()
        {
        }

        private AdminController _adminController;
        private EquipmentTypeController _equipmentTypeController;

        public AdminController GetAdminController()
        {
            return _adminController ?? (_adminController = new AdminController());
        }

        public EquipmentTypeController GetEquipmentTypeController()
        {
            return _equipmentTypeController ?? (_equipmentTypeController = new EquipmentTypeController());
        }
    }

}
