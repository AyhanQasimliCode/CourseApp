using Application.Exceptions;
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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        public StudentService(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }

        public void Create(Student student)
        {
            if (student.Group != null)
            {
                var g = _groupRepository.GetById(student.Group.Id);
                if (g == null)
                {
                    throw new NotFoundException("Group not found for student.");
                }
                student.Group = g;
            }

            _studentRepository.Create(student);
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

        public Student GetById(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                throw new NotFoundException("Student not found.");
            }
            return student;
        }

        public List<Student> SearchByNameOrSurname(string keyword)
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            var existing = _studentRepository.GetById(student.Id);
            if (existing == null)
                throw new NotFoundException("Student not found.");

            if (!string.IsNullOrWhiteSpace(student.Name))
                existing.Name = student.Name;

            if (!string.IsNullOrWhiteSpace(student.Surname))
                existing.Surname = student.Surname;

            if (student.Age != 0) 
                existing.Age = student.Age;

            if (student.Group != null)
            {
                var g = _groupRepository.GetById(student.Group.Id);
                if (g == null)
                    throw new NotFoundException("Group not found for student.");
                existing.Group = g;
            }
            else if (student.Group == null)
            {
            }

            _studentRepository.Update(existing);
        }

    }
}
