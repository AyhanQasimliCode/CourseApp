using Application.Helpers;
using Application.Service.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public void Create()
        {
            var name = Helper.GetValidatedStringLettersOnly("Name: ");
            var teacher = Helper.GetValidatedStringLettersOnly("Teacher: ");
            var room = Helper.GetValidatedStringAlphaNumeric("Room: ");

            _groupService.Create(new Group { Name = name, Teacher = teacher, Room = room });
            Console.Beep();
            Helper.TypeWriterMessage("Group created successfully!", ConsoleColor.Green);
        }

        public void Update()
        {
            Helper.TypeWriterMessage("== Update Group ==", ConsoleColor.Yellow);
            GetAll();

            int id = Helper.GetValidatedInteger("Enter Group Id: ");
            var existingGroup = _groupService.GetById(id);

            Helper.TypeWriterMessage("Leave fields empty if you do not want to change them", ConsoleColor.DarkGray);

            string name;
            while (true)
            {
                Console.Write($"Name ({existingGroup.Name}): ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || name.All(char.IsLetter))
                    break;

                Console.Beep();
                Helper.TypeWriterMessage("Please enter letters only", ConsoleColor.Red);
            }

            string teacher;
            while (true)
            {
                Console.Write($"Teacher ({existingGroup.Teacher}): ");
                teacher = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(teacher) || teacher.All(char.IsLetter))
                    break;

                Console.Beep();
                Helper.TypeWriterMessage("Please enter letters only", ConsoleColor.Red);
            }

            string room;
            while (true)
            {
                Console.Write($"Room ({existingGroup.Room}): ");
                room = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(room) || room.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
                    break;

                Console.Beep();
                Helper.TypeWriterMessage("Please enter letters or digits only", ConsoleColor.Red);
            }

            var groupToUpdate = new Group
            {
                Id = id,
                Name = string.IsNullOrWhiteSpace(name) ? existingGroup.Name : name,
                Teacher = string.IsNullOrWhiteSpace(teacher) ? existingGroup.Teacher : teacher,
                Room = string.IsNullOrWhiteSpace(room) ? existingGroup.Room : room
            };

            _groupService.Update(groupToUpdate);
            Console.Beep();
            Helper.TypeWriterMessage("Group updated successfully!", ConsoleColor.Green);
        }

        public void Delete()
        {
            int id = Helper.GetValidatedInteger("Group Id: ");
            _groupService.Delete(id);
            Console.Beep();
            Helper.TypeWriterMessage("Group deleted successfully!", ConsoleColor.Green);
        }

        public void GetById()
        {
            int id = Helper.GetValidatedInteger("Group Id: ");
            var group = _groupService.GetById(id);
            PrintGroup(group);
        }

        public void GetByTeacher()
        {
            var teacher = Helper.GetValidatedStringLettersOnly("Teacher: ");
            var list = _groupService.GetByTeacher(teacher);
            foreach (var g in list) PrintGroup(g);
        }

        public void GetByRoom()
        {
            var room = Helper.GetValidatedStringAlphaNumeric("Room: ");
            var list = _groupService.GetByRoom(room);
            foreach (var g in list) PrintGroup(g);
        }

        public void GetAll()
        {
            var list = _groupService.GetAll();
            if (!list.Any())
            {
                Helper.TypeWriterMessage("No groups available.", ConsoleColor.Red);
                return;
            }
            foreach (var g in list) PrintGroup(g);
        }

        public void SearchByName()
        {
            var name = Helper.GetValidatedStringLettersOnly("Search name: ");
            var list = _groupService.SearchByName(name);
            foreach (var g in list) PrintGroup(g);
        }

        private void PrintGroup(Group group)
        {
            Console.WriteLine($"Id:{group.Id} | Name:{group.Name} | Teacher:{group.Teacher} | Room:{group.Room}");
        }
    }
}
