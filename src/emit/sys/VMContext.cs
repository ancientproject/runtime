namespace ancient.runtime.emit.sys
{
    using System.Collections.Generic;
    using System.Reflection;

    public class VMContext
    {
        public IDictionary<ushort, ExternSignature> methods = new Dictionary<ushort, ExternSignature>();

        public void Add(string sign, MethodInfo method)
        {
            var index = Module.CompositeIndex(sign);
            if(methods.ContainsKey(index))
                return;
            var @extern = Module.Composite(sign, index);
            @extern.method = method;
            methods.Add(index, @extern);
        }

        public ExternStatus Find(ushort sign, out ExternSignature signature)
        {
            signature = null;
            if (!methods.ContainsKey(sign))
                return ExternStatus.MethodNotFound;
            signature = methods[sign];
            if (signature?.method is null)
                return ExternStatus.SigFault;
            if (!signature.method.IsStatic)
                return ExternStatus.MethodNotStatic;
            if (!signature.method.IsSecurityCritical)
                return ExternStatus.SecurityFault;


            return ExternStatus.Found;
        }
    }
}