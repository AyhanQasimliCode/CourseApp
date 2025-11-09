using Domain.Entities;
using Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        public void Create(Student data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetByAge(int age)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public Student? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> SearchByNameOrSurname(string keyword)
        {
            throw new NotImplementedException();
        }

        public void Update(Student data)
        {
            throw new NotImplementedException();
        }
    }
}
