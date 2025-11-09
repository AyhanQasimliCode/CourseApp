using Domain.Entities;
using Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Concrete
{
    public class GroupRepository : IGroupRepository
    {
        public void Create(Group data)
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

        public Group? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Group? GetByName(string name)
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

        public void Update(Group data)
        {
            throw new NotImplementedException();
        }
    }
}
