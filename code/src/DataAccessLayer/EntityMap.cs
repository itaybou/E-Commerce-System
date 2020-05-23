using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommerceSystem.DomainLayer.UserManagement.Subscribed;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    static class EntityMap
    {
        internal static void RegisterClassMaps()
        {
            //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            var pack = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);

            //BsonClassMap.RegisterClassMap<Store>(user => {
            //    user.MapIdMember(u => u.Guid);
            //    user.MapProperty(u => u.Name).SetElementName("username");
            //    user.MapProperty(u => u.Password).SetElementName("password");
            //    user.MapProperty(u => u._state).SetElementName("details");
            //    user.MapProperty(u => u.Cart).SetElementName("cart");
            //});

            BsonClassMap.RegisterClassMap<User>(user => {
                user.MapIdMember(u => u.Guid);
                user.AutoMap();
                //user.MapProperty(u => u.Cart).SetElementName("cart");
            });

            BsonClassMap.RegisterClassMap<Subscribed>(user => {
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
        }
    }
}
