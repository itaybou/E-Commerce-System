using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
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
            //BsonSerializer.RegisterSerializer(typeof(IStoreInterface), BsonSerializer.LookupSerializer<Store>());

            BsonClassMap.RegisterClassMap<Store>(store =>
            {
                store.AutoMap();
                //store.MapIdMember(s => s.Name);
                //user.MapProperty(u => u.Cart).SetElementName("cart");
                //user.MapProperty(u => u.State).SetElementName("state");
            });

            BsonClassMap.RegisterClassMap<User>(user =>
            {
                user.AutoMap();
                user.MapIdMember(u => u.Guid);
                user.MapProperty(u => u.Cart).SetElementName("cart");
                user.MapProperty(u => u.State).SetElementName("state");
            });

            BsonClassMap.RegisterClassMap<Subscribed>(user =>
            {
                user.SetDiscriminator("Subscribed");
                user.AutoMap();
                //user.SetIgnoreExtraElements(true);
                //user.MapCreator(u => new Subscribed(u._uname, u._pswd, u._details.Fname, u._details.Lname, u._details.Email));
            });

            BsonClassMap.RegisterClassMap<SystemAdmin>(admin =>
            {
                admin.SetDiscriminator("Admin");
                admin.AutoMap();
                //admin.MapCreator(u => new SystemAdmin(u._uname, u._pswd, u._details.Fname, u._details.Lname, u._details.Email));
            });

            BsonClassMap.RegisterClassMap<UserDetails>();

            //BsonClassMap.RegisterClassMap<UserDetails>(user => {
            //    user.AutoMap();
            //    user.MapProperty(u => u.Fname).SetElementName("first_name");
            //    user.MapProperty(u => u.Lname).SetElementName("last_name");
            //    user.MapProperty(u => u.Email).SetElementName("email");
            //});

            BsonClassMap.RegisterClassMap<UserShoppingCart>();

            BsonClassMap.RegisterClassMap<StoreShoppingCart>();
            //BsonClassMap.RegisterClassMap<Product>();
        }
    }
}