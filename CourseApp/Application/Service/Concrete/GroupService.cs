using Application.Exceptions;
using Application.Service.Abstract;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repository.Abstract;
using Infrastructure.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Concrete
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;

        public GroupService(IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }

        public void Create(Group group)
        {
            var existingGroup = _groupRepository.GetByName(group.Name);
            if (existingGroup != null)
                throw new DuplicateGroupException(group.Name);
            _groupRepository.Create(group);
        }

        public void Delete(int id)
        {
            var existing = _groupRepository.GetById(id);
            if (existing == null)
                throw new NotFoundException("Group not found");

            var studentsInGroup = _studentRepository.GetByGroupId(id);
            foreach (var student in studentsInGroup)
            {
                student.Group = null;
                _studentRepository.Update(student);
            }
            _groupRepository.Delete(id);
        }


        public List<Group> GetAll()
        {
           return _groupRepository.GetAll();
        }

        public Group GetById(int id)
        {
            var group = _groupRepository.GetById(id);
            if (group == null)
            {
                throw new NotFoundException("Group not found");
            }
            return group;
        }

        public List<Group> GetByRoom(string room)
        {
            var groups = _groupRepository.GetByRoom(room);

            if (groups == null || groups.Count == 0)
                throw new NotFoundException($"No groups found for rooms '{room}'.");

            return groups;
        }

        public List<Group> GetByTeacher(string teacher)
        {
            var groups = _groupRepository.GetByTeacher(teacher);

            if (groups == null || groups.Count == 0)
                throw new NotFoundException($"No groups found for teacher '{teacher}'.");

            return groups;
        }


        public List<Group> SearchByName(string name)
        {
            var groups = _groupRepository.SearchByName(name);

            if (groups == null || groups.Count == 0)
                throw new NotFoundException($"No groups found with name containing '{name}'.");

            return groups;
        }

        public void Update(Group group)
        {
            var existingGroup = _groupRepository.GetById(group.Id);

            if (existingGroup == null)
                throw new NotFoundException("Group not found");

            if (!string.IsNullOrWhiteSpace(group.Name))
                existingGroup.Name = group.Name;

            if (!string.IsNullOrWhiteSpace(group.Teacher))
                existingGroup.Teacher = group.Teacher;

            if (!string.IsNullOrWhiteSpace(group.Room))
                existingGroup.Room = group.Room;

            _groupRepository.Update(existingGroup);
        }
    }
}
