using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Abstract
{
    public interface IStudentService
    {
        void Create(Student student);
        void Update(Student student);
        void Delete(int id);
        Student GetById(int id);
        List<Student> GetByAge(int age);
        List<Student> GetByGroupId(int groupId);
        List<Student> GetAll();
        List<Student> SearchByNameOrSurname(string keyword);
    }
}
