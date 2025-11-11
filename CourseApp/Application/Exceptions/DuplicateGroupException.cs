using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DuplicateGroupException : Exception
    {
        public DuplicateGroupException(string name) : base($"Group name '{name}' already exists. Group name must be unique."
)
        { }

    }
}
