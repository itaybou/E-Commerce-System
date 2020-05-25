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
    internal class ProductDictionarySerializer : BaseSerializer, IBsonSerializer
    {
        public Type ValueType => typeof(Dictionary<Guid, Tuple<Product, int>>);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var result = new Dictionary<Guid, Tuple<Product, int>>();
            context.Reader.ReadStartArray();
            while (context.Reader.State != BsonReaderState.EndOfArray)
            {
                try
                {
                    context.Reader.ReadStartDocument();
                    var id = new Guid(context.Reader.ReadString());
                    var quantity = context.Reader.ReadInt32();
                    context.Reader.ReadEndDocument();
                    var product = Data().Products.GetByIdOrNull(id, p => p.Id);
                    result.Add(id, Tuple.Create(product, quantity));
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
            if (value is Dictionary<Guid, Tuple<Product, int>> products)
            {
                context.Writer.WriteStartArray();
                foreach (var product in products.Values)
                {
                    context.Writer.WriteStartDocument();
                    context.Writer.WriteName("product");
                    context.Writer.WriteString(product.Item1.Id.ToString());
                    context.Writer.WriteName("quantity");
                    context.Writer.WriteInt32(product.Item2);
                    context.Writer.WriteEndDocument();
                }
                context.Writer.WriteEndArray();
            }
            else throw new NotSupportedException("Invalid serialization: expected " + nameof(List<Product>));
        }
    }
}
