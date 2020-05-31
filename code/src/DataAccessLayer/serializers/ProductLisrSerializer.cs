using ECommerceSystem.DomainLayer.StoresManagement;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer.serializers
{
    internal class ProductListSerializer : BaseSerializer, IBsonSerializer
    {
        public Type ValueType => typeof(List<Product>);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var result = new List<Product>();
            context.Reader.ReadStartArray();
            while (context.Reader.State != BsonReaderState.EndOfArray)
            {
                try
                {
                    var id = new Guid(context.Reader.ReadString());
                    result.Add(Data().Products.GetByIdOrNull(id, p => p.Id));
                }
                catch (Exception)
                {
                    break;
                }
            }
            context.Reader.ReadEndArray();
            return result;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is List<Product> products)
            {
                context.Writer.WriteStartArray();
                foreach (var product in products)
                    context.Writer.WriteString(product.Id.ToString());
                context.Writer.WriteEndArray();
            }
            else throw new NotSupportedException("Invalid serialization: expected " + nameof(List<Product>));
        }
    }
}
