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
            var student = GetById(id);
            if (student != null)
            {
                AppDbContext<Student>.datas.Remove(student);
            }
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetByAge(int age)
        {
            return AppDbContext<Student>.datas.FindAll(x => x.Age == age);
        }


        public List<Student> GetByGroupId(int groupId)
        {
            return AppDbContext<Student>.datas.FindAll(x => x.Group != null && x.Group.Id == groupId);
        }

        public Student? GetById(int id)
        {
            return AppDbContext<Student>.datas.FirstOrDefault(x => x.Id == id);
        }

        public List<Student> SearchByNameOrSurname(string keyword)
        {
            string lowerKeyword = keyword.ToLower();
            return AppDbContext<Student>.datas.FindAll(x =>x.Name.ToLower().Contains(lowerKeyword) || x.Surname.ToLower().Contains(lowerKeyword));
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
