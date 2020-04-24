using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class SearchResultModel
    {
        private ICollection<ProductModel> _productResults;
        private ICollection<string> _additionalKeywords;
        private bool corrected;

        public SearchResultModel(List<ProductModel> productResults, List<string> additionalKeywords)
        {
            ProductResults = productResults;
            AdditionalKeywords = additionalKeywords;
            Corrected = AdditionalKeywords != null && AdditionalKeywords.Count > 0;
        }

        public ICollection<ProductModel> ProductResults { get => _productResults; set => _productResults = value; }
        public ICollection<string> AdditionalKeywords { get => _additionalKeywords; set => _additionalKeywords = value; }
        public bool Corrected { get => corrected; set => corrected = value; }
    }
}
