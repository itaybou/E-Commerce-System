using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using System;
using static ECommerceSystem.DomainLayer.UserManagement.Subscribed;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    internal static class EntityMap
    {
        internal static void RegisterClassMaps()
        {
            //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            var pack = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
            pack = new ConventionPack() { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElementsConvention", pack, t => true);
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(typeof(Tuple), new GuidSerializer(BsonType.String));

            BsonClassMap.RegisterClassMap<Store>(store =>
            {
                store.AutoMap();
                store.MapIdMember(s => s.Name);
            });

            BsonClassMap.RegisterClassMap<User>(user =>
            {
                user.AutoMap();
                user.MapIdMember(u => u.Guid);
            });

            BsonClassMap.RegisterClassMap<Subscribed>(user =>
            {
                user.SetDiscriminator("Subscribed");
                user.AutoMap();
            });

            BsonClassMap.RegisterClassMap<SystemAdmin>(admin =>
            {
                admin.SetDiscriminator("Admin");
                admin.AutoMap();
            });

            BsonClassMap.RegisterClassMap<UserDetails>();
            BsonClassMap.RegisterClassMap<UserShoppingCart>();
            BsonClassMap.RegisterClassMap<StoreShoppingCart>();
            BsonClassMap.RegisterClassMap<ProductInventory>();
        }
    }
}