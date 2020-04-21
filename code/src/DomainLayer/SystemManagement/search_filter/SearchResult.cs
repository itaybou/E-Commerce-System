using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.SystemManagement.search_filter
{
    public class SearchResult
    {
        private List<ProductInventory> _productResults;
        private List<string> _additionalKeywords;
        private bool corrected;

        public SearchResult(List<ProductInventory> productResults, List<string> additionalKeywords)
        {
            ProductResults = productResults;
            AdditionalKeywords = additionalKeywords;
            Corrected = AdditionalKeywords != null && AdditionalKeywords.Count > 0;
        }

        public List<ProductInventory> ProductResults { get => _productResults; set => _productResults = value; }
        public List<string> AdditionalKeywords { get => _additionalKeywords; set => _additionalKeywords = value; }
        public bool Corrected { get => corrected; set => corrected = value; }
    }
}
