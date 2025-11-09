using Application.Service.Abstract;
using Domain.Entities;
using Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Concrete
{
    public class StudentService : IGroupService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public void Create(Group group)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll()
        {
            throw new NotImplementedException();
        }

        public Group GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetByRoom(string room)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetByTeacher(string teacher)
        {
            throw new NotImplementedException();
        }

        public List<Group> SearchByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
