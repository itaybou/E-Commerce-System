using System.Collections.Generic;

namespace ECommerceSystem.Models
{
    public class PermissionModel
    {
        private bool _isOwner;
        private string _storeName;
        private string _assignedBy;
        private IEnumerable<PermissionType> _permission;

        public PermissionModel(bool isOwner, string assignedBy, string storeName, IEnumerable<PermissionType> permission)
        {
            _isOwner = isOwner;
            _storeName = storeName;
            _permission = permission;
            _assignedBy = assignedBy;
        }

        public bool IsOwner { get => _isOwner; set => _isOwner = value; }
        public string StoreName { get => _storeName; set => _storeName = value; }
        public IEnumerable<PermissionType> PermissionTypes { get => _permission; set => _permission = value; }
        public string AssignedBy { get => _assignedBy; set => _assignedBy = value; }
    }
}