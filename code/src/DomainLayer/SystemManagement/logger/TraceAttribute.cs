using ECommerceSystem.DomainLayer.UserManagement.security;
using PostSharp.Aspects;
using System;
using System.Text;

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
                var argArguments = args.Arguments.GetArgument(i) == null ? "null" : args.Arguments.GetArgument(i);
                stringBuilder.AppendFormat("{0}, {1}, {2}, {3}" + (i != args.Arguments.Count - 1 ? ", " : ""),
                    i, argArguments.GetType().Name, args.Method.GetParameters()[i].Name, paramValue);
            }
            SystemLogger.LogMethodInfo(stringBuilder.ToString());
        }
    }
}