using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetSystem.Controllers.Enum
{
    /// <summary>
    /// 设备种类操作的enum
    /// </summary>
    public enum EnumEquipmentTypeOperatorOptions
    {
        Add = 1,
        Delete = 2,
        PrintAll = 3,
        Exit = 4
    }

    /// <summary>
    /// 设备操作的enum
    /// </summary>
    public enum EnumEquipmentOpOptions
    {
        Add = 1,
        Delete = 2,
        Update = 3,
        PrintAll = 4,
        Find = 5,
        Borrow = 6,
        Return = 7,
        Exit = 8
    }

    /// <summary>
    /// 判断用户管理操作的enum
    /// </summary>
    public enum EnumUserOpOptions
    {
        PrintAll = 1,
        Exit = 2
    }

    public enum EnumFindCondition
    {
        ByBigType = 1,  //按照大类浏览
        BySmallType = 2, //按照小类浏览
        ByLogicId = 3, //按照设备的编号浏览
        ByUser = 4, //按照使用者浏览
    }
}
