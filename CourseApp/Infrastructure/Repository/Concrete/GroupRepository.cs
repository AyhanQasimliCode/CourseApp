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
    public class GroupRepository : IGroupRepository
    {
        public void Create(Group data)
        {
            AppDbContext<Group>.datas.Add(data);
        }

        public void Delete(int id)
        {
            var group = GetById(id);
            if (group != null)
            {
                AppDbContext<Group>.datas.Remove(group);
            }
        }

        public List<Group> GetAll()
        {
            return AppDbContext<Group>.datas.ToList();
        }

        public Group? GetById(int id)
        {
            return AppDbContext<Group>.datas.FirstOrDefault(x => x.Id == id);
        }

        public Group? GetByName(string name)
        {
            return AppDbContext<Group>.datas.Find(g => g.Name.ToLower() == name.ToLower());
        }

        public List<Group> GetByRoom(string room)
        {
            var result = new List<Group>();

            foreach (var group in AppDbContext<Group>.datas)
            {
                if (group.Room.ToLower() == room.ToLower())
                    result.Add(group);
            }

            return result;
        }

        public List<Group> GetByTeacher(string teacher)
        {
            var result = new List<Group>();

            foreach (var group in AppDbContext<Group>.datas)
            {
                if (group.Teacher.ToLower() == teacher.ToLower())
                    result.Add(group);
            }

            return result;
        }

        public List<Group> SearchByName(string name)
        {
            string lowerName = name.ToLower();
            return AppDbContext<Group>.datas.Where(g => g.Name.ToLower().Contains(lowerName)).ToList();
        }

        public void Update(Group data)
        {
            var groupList = AppDbContext<Group>.datas;
            var existingGroup = groupList.FirstOrDefault(g => g.Id == data.Id);
            if (existingGroup != null)
            {
                existingGroup.Name = data.Name;
                existingGroup.Room = data.Room;
                existingGroup.Teacher = data.Teacher;
            }
            
        }
    }
}
