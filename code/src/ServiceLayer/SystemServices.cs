using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;

namespace ECommerceSystem.ServiceLayer
{
    public class SystemServices
    {
        private SystemManager _systemManager;

        public SystemServices()
        {
            _systemManager = SystemManager.Instance;
        }

        // Use-case 2.5 - search and filter services
        public List<ProductInventory> searchProductsByCategory(Category category)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByCategory(category);
        }

        public List<ProductInventory> searchProductsByName(string prodName)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByName(prodName);
        }

        public List<ProductInventory> searchProductsByKeyword(List<string> keywords)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByKeyword(keywords);
        }

        private List<ProductInventory> applyPriceRangeFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyPriceRangeFilter(from, to);
        }

        private List<ProductInventory> applyStoreRatingFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyStoreRatingFilter(from, to);
        }

        private List<ProductInventory> applyProductRatingFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyProductRatingFilter(from, to);
        }

        private List<ProductInventory> applyCategoryFilter(Category category)
        {
            return _systemManager.SearchAndFilterSystem.applyCategoryFilter(category);
        }

        private List<ProductInventory> cancelFilter(Filters filter)
        {
            return _systemManager.SearchAndFilterSystem.cancelFilter(filter);
        }
    }
}
