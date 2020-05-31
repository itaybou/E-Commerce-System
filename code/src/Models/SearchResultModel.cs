using System.Collections.Generic;

namespace ECommerceSystem.Models
{
    public class SearchResultModel
    {
        private ICollection<ProductInventoryModel> _productResults;
        private ICollection<string> _additionalKeywords;
        private bool corrected;

        public SearchResultModel(List<ProductInventoryModel> productResults, List<string> additionalKeywords)
        {
            ProductResults = productResults;
            AdditionalKeywords = additionalKeywords;
            Corrected = AdditionalKeywords != null && AdditionalKeywords.Count > 0;
        }

        public ICollection<ProductInventoryModel> ProductResults { get => _productResults; set => _productResults = value; }
        public ICollection<string> AdditionalKeywords { get => _additionalKeywords; set => _additionalKeywords = value; }
        public bool Corrected { get => corrected; set => corrected = value; }
    }
}