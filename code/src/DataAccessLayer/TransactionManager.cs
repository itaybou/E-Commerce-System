using ECommerceSystem.DataAccessLayer.repositories;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer
{
    internal class TransactionManager : ITransactions
    {
        private MongoClient _client;
        private IUserRepository _users;
        private IStoreRepository _stores;

        public TransactionManager(MongoClient client, IUserRepository users, IStoreRepository stores)
        {
            _client = client;
            _users = users;
            _stores = stores;
        }

        private async Task BaseTransactionAsync(Action transactionLogic)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    transactionLogic();
                    await session.CommitTransactionAsync();
                }
                catch (Exception e)
                {
                    await session.AbortTransactionAsync();
                }
            }
        }

        public async void OpenStoreTransaction(User owner, Store opened)
        {
            await BaseTransactionAsync(() =>
            {
                _users.Update(owner, owner.Guid, u => u.Guid);
                _stores.Insert(opened);
            });
        }
    }
}