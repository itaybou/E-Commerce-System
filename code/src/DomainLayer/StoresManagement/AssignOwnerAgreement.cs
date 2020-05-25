using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class AssignOwnerAgreement
    {
        private Guid _ID;
        private Guid _assignerID;
        private string _asigneeUserName;
        private HashSet<string> _pendingApproval;

        public AssignOwnerAgreement(Guid ID, Guid assignerID, string asigneeUserName, HashSet<string> pendingApproval)
        {
            _ID = ID;
            _assignerID = assignerID;
            _asigneeUserName = asigneeUserName;
            _pendingApproval = pendingApproval;
        }

        public Guid ID { get => _ID; set => _ID = value; }
        public Guid AssignerID { get => _assignerID; set => _assignerID = value; }
        public string AsigneeUserName { get => _asigneeUserName; set => _asigneeUserName = value; }
        public HashSet<string> PendingApproval { get => _pendingApproval; set => _pendingApproval = value; }

        public bool approve(string approver)
        {
            if (_pendingApproval.Contains(approver))
            {
                _pendingApproval.Remove(approver);
                return true;
            }
            else return false;
        }

        public bool isDone()
        {
            return _pendingApproval.Count == 0;
        }

        public bool disapprove(string disapproverUserName)
        {
            if (_pendingApproval.Contains(disapproverUserName))
            {
                return true;
            }
            else return false;
        }
    }
}
