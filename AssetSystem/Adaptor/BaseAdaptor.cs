using AssetSystem.Models;

namespace AssetSystem.Adaptor
{
    public class BaseAdaptor
    {
        public TheContext DbCtx;
        public BaseAdaptor()
        {
            DbCtx = DbCtx ?? new TheContext();
        }
    }
}