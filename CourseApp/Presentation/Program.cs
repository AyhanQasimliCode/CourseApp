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
                    switch (choice)
                    {
                        case "1": CreateGroup(groupService); break;
                        case "2": UpdateGroup(groupService); break;
                        case "3": DeleteGroup(groupService); break;
                        case "4": GetGroupById(groupService); break;
                        case "5": GetGroupsByTeacher(groupService); break;
                        case "6": GetGroupsByRoom(groupService); break;
                        case "7": GetAllGroups(groupService); break;
                        case "8": CreateStudent(studentService, groupService); break;
                        case "9": UpdateStudent(studentService, groupService); break;
                        case "10": GetStudentById(studentService); break;

                        case "14": SearchGroupsByName(groupService); break;
                        case "0": return;
                    }
                }
                catch (DuplicateGroupException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }

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
        public static void DeleteGroup(IGroupService groupService)
        {
            Console.WriteLine("== Delete Group ==");
            var id = Helper.GetValidatedInteger("Group Id: ");
            groupService.Delete(id);
            Console.WriteLine("Group deleted.");
        }
        public static void GetGroupById(IGroupService groupService)
        {
            Console.WriteLine("== Get Group By Id ==");
            int id = Helper.GetValidatedInteger("Group Id: ");
            var group = groupService.GetById(id);
            PrintGroup(group);

        }
        public static void GetGroupsByTeacher(IGroupService groupService)
        {
            Console.WriteLine("== Get Groups By Teacher ==");
            var teacher = Helper.GetValidatedStringLettersOnly("Teacher: ");
            var list = groupService.GetByTeacher(teacher);
            list.ForEach(PrintGroup);
        }
        public static void GetGroupsByRoom(IGroupService groupService)
        {
            Console.WriteLine("== Get Groups By Room ==");
            var room = Helper.GetValidatedStringAlphaNumeric("Room: ");
            var list = groupService.GetByRoom(room);
            list.ForEach(PrintGroup);
        }
        public static void GetAllGroups(IGroupService groupService)
        {
            Console.WriteLine("== All Groups ==");
            var groupList = groupService.GetAll();
            if (!groupList.Any()) 
                Console.WriteLine("No groups.");
            groupList.ForEach(PrintGroup);
        }
        public static void SearchGroupsByName(IGroupService service)
        {
            Console.WriteLine("== Search Groups By Name ==");
            var groupName = Helper.GetValidatedStringAlphaNumeric("Name search: ");
            var list = service.SearchByName(groupName);
            list.ForEach(PrintGroup);
        }
        public static void CreateStudent(IStudentService studentService, IGroupService groupService)
        {
            Console.WriteLine("== Create Student ==");
            var name = Helper.GetValidatedStringLettersOnly("Name (letters only): ");
            var surname = Helper.GetValidatedStringLettersOnly("Surname (letters only): ");
            var age = Helper.GetValidatedInteger("Age: ");

            Console.Write("Assign to group? (y/n): ");
            var yn = Console.ReadLine();
            Group? group = null;
            if (!string.IsNullOrEmpty(yn) && yn.Trim().ToLower() == "y")
            {
                var groups = groupService.GetAll();
                groups.ForEach(g => Console.WriteLine($"Id:{g.Id} Name:{g.Name}"));
                var gid = Helper.GetValidatedInteger("Group Id: ");
                group = groupService.GetById(gid);
            }

            studentService.Create(new Student { Name = name, Surname = surname, Age = age, Group = group });
            Console.WriteLine("Student created.");
        }
        public static void UpdateStudent(IStudentService studentService, IGroupService groupService)
        {
            Console.WriteLine("== Update Student ==");
            var id = Helper.GetValidatedInteger("Student Id: ");
            var existing = studentService.GetById(id);

            Console.WriteLine("Leave fields empty if you do not want to change them.");

            Console.Write($"Name ({existing.Name}): ");
            string name = Console.ReadLine();

            Console.Write($"Surname ({existing.Surname}): ");
            string surname = Console.ReadLine();

            Console.Write($"Age ({existing.Age}): ");
            string ageInput = Console.ReadLine();
            int age = 0;
            if (!string.IsNullOrWhiteSpace(ageInput))
                age = int.Parse(ageInput);

            Console.Write("Assign to group? (y/n): ");
            var yn = Console.ReadLine();
            Group? group = null;
            if (!string.IsNullOrEmpty(yn) && yn.Trim().ToLower() == "y")
            {
                var groups = groupService.GetAll();
                groups.ForEach(g => Console.WriteLine($"Id:{g.Id} Name:{g.Name}"));
                var gid = Helper.GetValidatedInteger("Group Id: ");
                group = groupService.GetById(gid);
            }

            var studentToUpdate = new Student
            {
                Id = id,
                Name = string.IsNullOrWhiteSpace(name) ? null : name,
                Surname = string.IsNullOrWhiteSpace(surname) ? null : surname,
                Age = age,
                Group = group
            };

            studentService.Update(studentToUpdate);
            Console.WriteLine("Student updated.");
        }

        public static void GetStudentById(IStudentService sService)
        {
            Console.WriteLine("== Get Student By Id ==");
            var id = Helper.GetValidatedInteger("Student Id: ");
            var s = sService.GetById(id);
            PrintStudent(s);
        }
        public static void PrintGroup(Group group)
        {
            Console.WriteLine($"Id:{group.Id} | Name:{group.Name} | Teacher:{group.Teacher} | Room:{group.Room}");
        }
        public static void PrintStudent(Student s)
        {
            var group = s.Group != null ? $"{s.Group.Id}:{s.Group.Name}" : "NoGroup";
            Console.WriteLine($"Id:{s.Id} | {s.Name} {s.Surname} | Age:{s.Age} | Group:{group}");
        }
    }
}
