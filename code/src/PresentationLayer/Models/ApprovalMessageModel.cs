using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ApprovalMessageModel
    {
        public string Message { get; set; }
        public string RedirectIfApprovedPath { get; set; }
        public string RedirectIfDisapprovedPath { get; set; }

        public ApprovalMessageModel(string message, string redirectApprove, string redirectDisapprove)
        {
            Message = message;
            RedirectIfApprovedPath = redirectApprove;
            RedirectIfDisapprovedPath = redirectDisapprove;
        }
    }
}
