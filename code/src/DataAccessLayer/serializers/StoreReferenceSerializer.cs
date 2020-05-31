using ECommerceSystem.DomainLayer.StoresManagement;
using MongoDB.Bson.Serialization;
using System;

namespace ECommerceSystem.DataAccessLayer.serializers
{
    internal class StoreReferenceSerializer : BaseSerializer, IBsonSerializer
    {
        public Type ValueType => typeof(Store);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var storeName = context.Reader.ReadString();
            return Data().Stores.GetByIdOrNull(storeName, s => s.Name);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Store store)
            {
                context.Writer.WriteString(store.Name);
            }
            else throw new NotSupportedException("Invalid serialization: expected " + nameof(Store));
        }
    }
}