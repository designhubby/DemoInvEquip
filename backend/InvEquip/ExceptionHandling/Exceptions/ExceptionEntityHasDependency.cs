using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.ExceptionHandling.Exceptions
{
    public class ExceptionEntityHasDependency : Exception
    {
        public ExceptionEntityHasDependency() { }
        public ExceptionEntityHasDependency(string message) : base($"Entity {message}") { }
        public ExceptionEntityHasDependency(string message, Exception innerexception) : base(message, innerexception) { }
    }
}
