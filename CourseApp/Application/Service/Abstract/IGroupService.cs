using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Abstract
{
    public interface IGroupService
    {
        void Create(Group group);
        void Update(Group group);
        void Delete(int id);
        Group GetById(int id);
        List<Group> GetByTeacher(string teacher);
        List<Group> GetByRoom(string room);
        List<Group> GetAll();
        List<Group> SearchByName(string name);

        
    }
}
