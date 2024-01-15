using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.ExceptionHandling.Exceptions
{
    public class ExceptionEntityNotExists : Exception
    {
        public ExceptionEntityNotExists()
        {

        }
        public ExceptionEntityNotExists(string message) : base($"Entity {message} ")
        {

        }
        public ExceptionEntityNotExists(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
