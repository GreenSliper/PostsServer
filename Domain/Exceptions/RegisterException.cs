using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Exceptions
{
    public class RegisterException : Exception
    {
        List<string> errors;
        public RegisterException(params string[] errors)
        {
            this.errors = new List<string>(errors);
        }
        public RegisterException(IEnumerable<string> errors)
        {
            this.errors = new List<string>(errors);
        }

        public override string Message => String.Concat(errors.Select(x=>x + "\n"));
    }
}
