using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Create(T data);
        void Update(T data);
        void Delete(int id);
        T? GetById(int id);
        List<T> GetAll();
    }
}
