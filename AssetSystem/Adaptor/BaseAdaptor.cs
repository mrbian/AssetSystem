using AssetSystem.Models;

namespace AssetSystem.Adaptor
{
    public class BaseAdaptor
    {
        public TheContext DbCtx;
        public BaseAdaptor()
        {
            this.DbCtx = new TheContext();
        }
    }
}