using ECommerceSystem.DomainLayer.UserManagement;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer.serializers
{
    class CartSerializer : BaseSerializer, IBsonSerializer
    {
        public Type ValueType => typeof(UserShoppingCart);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is UserShoppingCart cart)
            {
                context.Writer.WriteString(cart.UserID.ToString());
            }
            else throw new NotSupportedException("Invalid serialization: expected " + nameof(UserShoppingCart));
        }
    }
}
