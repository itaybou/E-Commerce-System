using ECommerceSystem.DomainLayer.UserManagement.security;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.SystemManagement.logger
{
    [Serializable]
    public class TraceAttribute : OnMethodBoundaryAspect
    {
        private readonly string category;
        private readonly string PSWD_PARAM_NAME = "pswd";

        public TraceAttribute(string category)
        {
            this.category = category;
        }

        public string Category { get { return category; } }
        public override void OnEntry(MethodExecutionArgs args)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < args.Arguments.Count; ++i)
            {
                var paramValue = args.Arguments.GetArgument(i);
                paramValue = args.Method.GetParameters()[i].Name.Equals(PSWD_PARAM_NAME) ? Encryption.encrypt((string)paramValue) : paramValue;
                stringBuilder.AppendFormat("({0}: type: {1}, name: {2}, value: {3}) ", 
                    i, args.Arguments.GetArgument(i).GetType().Name, args.Method.GetParameters()[i].Name, paramValue);
            }
            SystemLogger.LogMethodInfo(stringBuilder.ToString());
        }
    }
}
