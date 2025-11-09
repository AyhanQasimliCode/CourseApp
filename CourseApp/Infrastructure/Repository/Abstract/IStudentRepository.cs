using Domain.Entities;
using Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Abstract
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetByAge(int age);
        List<Student> GetByGroupId(int groupId);
        List<Student> SearchByNameOrSurname(string keyword);
    }
}
