using Application.Exceptions;
using Application.Helpers;
using Application.Service.Abstract;
using Application.Service.Concrete;
using Domain.Entities;
using Infrastructure.Repository.Abstract;
using Infrastructure.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IGroupRepository groupRepository = new GroupRepository();
            IStudentRepository studentRepository = new StudentRepository();
            IGroupService groupService = new GroupService(groupRepository);
            IStudentService studentService = new StudentService(studentRepository, groupRepository);
            while(true)
            {
                ShowMenu();
                Console.WriteLine("Make a choice");
                string choice = Console.ReadLine();
                try
                {
                    switch(choice)
                    {
                        case "1": CreateGroup(groupService); break;
                        case "2": UpdateGroup(groupService); break;
                        case "7": GetAllGroups(groupService); break;




                    }
                }
                catch (DuplicateGroupException ex) { Console.WriteLine(ex.Message); }
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("=== CourseApp Menu ===");
            Console.WriteLine("1 - Create Group");
            Console.WriteLine("2 - Update Group");
            Console.WriteLine("3 - Delete Group");
            Console.WriteLine("4 - Get Group by Id");
            Console.WriteLine("5 - Get groups by Teacher");
            Console.WriteLine("6 - Get groups by Room");
            Console.WriteLine("7 - Get all groups");
            Console.WriteLine("8 - Create Student");
            Console.WriteLine("9 - Update Student");
            Console.WriteLine("10 - Get student by Id");
            Console.WriteLine("11 - Delete student");
            Console.WriteLine("12 - Get students by Age");
            Console.WriteLine("13 - Get students by Group Id");
            Console.WriteLine("14 - Search groups by Name");
            Console.WriteLine("15 - Search students by Name or Surname");
            Console.WriteLine("0 - Exit");
        }
        public static void CreateGroup(IGroupService groupService)
        {
            var name = Helper.GetValidatedStringLettersOnly("Name: ");
            var teacher = Helper.GetValidatedStringLettersOnly("Teacher: ");
            var room = Helper.GetValidatedStringAlphaNumeric("Room: ");
           

            groupService.Create(new Group { Name = name, Teacher = teacher, Room = room });
            Console.WriteLine("Group created.");
        }
        public static void UpdateGroup(IGroupService groupService)
        {
            Console.WriteLine("== Update Group ==");
            GetAllGroups(groupService);
            int id = Helper.GetValidatedInteger("Enter the Id of the Group you want to update: ");
            var existingGroup = groupService.GetById(id); 

            Console.WriteLine("Leave fields empty if you do not want to change them.");

            Console.Write($"Name ({existingGroup.Name}): ");
            string name = Console.ReadLine();

            Console.Write($"Teacher ({existingGroup.Teacher}): ");
            string teacher = Console.ReadLine();

            Console.Write($"Room ({existingGroup.Room}): ");
            string room = Console.ReadLine();

            var groupToUpdate = new Group
            {
                Id = id, 
                Name = string.IsNullOrWhiteSpace(name) ? null : name,
                Teacher = string.IsNullOrWhiteSpace(teacher) ? null : teacher,
                Room = string.IsNullOrWhiteSpace(room) ? null : room
            };

            groupService.Update(groupToUpdate);
            Console.WriteLine("Group updated.");
        }

        static void GetAllGroups(IGroupService groupService)
        {
            Console.WriteLine("== All Groups ==");
            var groupList = groupService.GetAll();
            if (!groupList.Any()) 
                Console.WriteLine("No groups.");
            groupList.ForEach(PrintGroup);
        }
        public static void PrintGroup(Group group)
        {
            Console.WriteLine($"Id:{group.Id} | Name:{group.Name} | Teacher:{group.Teacher} | Room:{group.Room}");
        }
    }
}
