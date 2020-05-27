using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using System;
using static ECommerceSystem.DomainLayer.UserManagement.Subscribed;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    internal static class EntityMap
    {
        internal static void RegisterClassMaps()
        {
            var pack = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
            pack = new ConventionPack() { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElementsConvention", pack, t => true);
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);

            BsonClassMap.RegisterClassMap<CompositePurchasePolicy>(disc =>
            {
                disc.SetDiscriminator("CompPolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<AndPurchasePolicy>(disc =>
            {
                disc.SetDiscriminator("AndPolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<OrPurchasePolicy>(disc =>
            {
                disc.SetDiscriminator("OrPolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<XORPurchasePolicy>(disc =>
            {
                disc.SetDiscriminator("XorPolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<DaysOffPolicy>(disc =>
            {
                disc.SetDiscriminator("DaysOffPolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<LocationPolicy>(disc =>
            {
                disc.SetDiscriminator("LocationPolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<MinPricePerStorePolicy>(disc =>
            {
                disc.SetDiscriminator("MinStorePricePolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<ProductQuantityPolicy>(disc =>
            {
                disc.SetDiscriminator("ProdQuantPolicy");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<ConditionalStoreDiscount>(disc =>
            {
                disc.SetDiscriminator("StoreDiscount");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<CompositeDiscountPolicy>(disc =>
            {
                disc.SetDiscriminator("CompDiscount");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<ConditionalProductDiscount>(disc =>
            {
                disc.SetDiscriminator("CondProdDiscount");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<VisibleDiscount>(disc =>
            {
                disc.SetDiscriminator("VisProdDiscount");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<AndDiscountPolicy>(disc =>
            {
                disc.SetDiscriminator("AndCompDiscount");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<OrDiscountPolicy>(disc =>
            {
                disc.SetDiscriminator("OrCompDiscount");
                disc.AutoMap();
            });

            BsonClassMap.RegisterClassMap<XORDiscountPolicy>(disc =>
            {
                disc.SetDiscriminator("XorCompDiscount");
                disc.AutoMap();
            });

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