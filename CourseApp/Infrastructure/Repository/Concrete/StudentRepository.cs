using Domain.Entities;
using Infrastructure.Data;
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
            AppDbContext<Student>.datas.Add(data);
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
            return AppDbContext<Student>.datas.FirstOrDefault(x => x.Id == id);
        }

        public List<Student> SearchByNameOrSurname(string keyword)
        {
            throw new NotImplementedException();
        }

        public void Update(Student data)
        {
            var student = GetById(data.Id);
            if (student != null)
            {
                student.Name = data.Name;
                student.Surname = data.Surname;
                student.Age = data.Age;
                student.Group = data.Group;
            }
        }
    }
}
