using ECommerceSystem.DataAccessLayer.repositories;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer
{
    internal class TransactionManager : ITransactions
    {
        private MongoClient _client;
        private IUserRepository _users;
        private IStoreRepository _stores;
        private IProductRepository _products;

        public TransactionManager(MongoClient client, IUserRepository users, IStoreRepository stores, IProductRepository products)
        {
            _client = client;
            _users = users;
            _stores = stores;
            _products = products;
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
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw new DatabaseException("Database transaction error: transaction aborted.");
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

        public async void AddProductDiscountTransaction(Product product, Store store)
        {
            await BaseTransactionAsync(() =>
            {
                _products.Update(product, product.Id, p => p.Id);
                _stores.Update(store, store.Name, s => s.Name);
            });
        }

        public async void RemoveProductInventoryTransaction(List<Product> products, Store store)
        {
            await BaseTransactionAsync(() =>
            {
                foreach(var product in products)
                {
                    _products.Remove(product, product.Id, p => p.Id);
                }
                _stores.Update(store, store.Name, s => s.Name);
            });
        }

        public async void RemoveProductTransaction(Product product, Store store)
        {
            await BaseTransactionAsync(() =>
            {
                _products.Remove(product, product.Id, p => p.Id);
                _stores.Update(store, store.Name, s => s.Name);
            });
        }

        public async void AddProductPurchasePolicyTransaction(Product product, Store store)
        {
            await BaseTransactionAsync(() =>
            {
                _products.Update(product, product.Id, p => p.Id);
                _stores.Update(store, store.Name, s => s.Name);
            });
        }
    }
}