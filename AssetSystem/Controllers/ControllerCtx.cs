using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetSystem.Models;

namespace AssetSystem.Controllers
{
    class ControllerCtx
    {
        public ControllerCtx()
        {
        }



        private AdminController _adminController;
        private EquipmentTypeController _equipmentTypeController;
        private EquipmentController _equipmentController;
        private UserController _userController;

        public AdminController GetAdminController()
        {
            return _adminController ?? (_adminController = new AdminController());
        }

        public EquipmentTypeController GetEquipmentTypeController()
        {
            return _equipmentTypeController ?? (_equipmentTypeController = new EquipmentTypeController());
        }

        public EquipmentController GetEquipmentController()
        {
            return _equipmentController ?? (_equipmentController = new EquipmentController()); 
        }

        public UserController GetUserController()
        {
            return _userController ?? (_userController = new UserController());
        }
    }

}
