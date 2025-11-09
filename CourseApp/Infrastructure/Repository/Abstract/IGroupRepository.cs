using Domain.Entities;
using Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Abstract
{
    public interface IGroupRepository : IRepository<Group>
    {
        List<Group> GetByTeacher(string teacher);
        List<Group> GetByRoom(string room);
        List<Group> SearchByName(string name);
        Group? GetByName(string name);
    }
}
