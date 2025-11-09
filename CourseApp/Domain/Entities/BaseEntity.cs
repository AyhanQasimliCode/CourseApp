using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        private static int _idCounter = 0;
        public int Id { get; private set; }

        protected BaseEntity()
        {
            Id = ++_idCounter;
        }
    }
}
