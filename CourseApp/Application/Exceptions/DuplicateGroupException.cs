using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DuplicateGroupException : Exception
    {
        public DuplicateGroupException(string name) : base($"Qrup adı '{name}' artıq mövcuddur. Qrup adı unikal olmalıdır.") { }

    }
}
