using Application.Exceptions;
using Application.Service.Abstract;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repository.Abstract;
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
        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
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
            throw new NotImplementedException();
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
                throw new NotFoundException("Group not found.");
            }
            return group;
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
