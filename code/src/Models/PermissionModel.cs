using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class PermissionModel
    {
        private bool _isOwner;
        private string _storeName;
        private IEnumerable<PermissionType> _permission;

        public PermissionModel(bool isOwner, string storeName, IEnumerable<PermissionType> permission)
        {
            _isOwner = isOwner;
            _storeName = storeName;
            _permission = permission;
        }

        public bool IsOwner { get => _isOwner; set => _isOwner = value; }
        public string StoreName { get => _storeName; set => _storeName = value; }
        public IEnumerable<PermissionType> Permission { get => _permission; set => _permission = value; }
    }
}
