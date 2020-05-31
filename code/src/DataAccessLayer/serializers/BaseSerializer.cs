namespace ECommerceSystem.DataAccessLayer.serializers
{
    internal abstract class BaseSerializer
    {
        protected IDataAccess Data() => DataAccess.Instance;
    }
}