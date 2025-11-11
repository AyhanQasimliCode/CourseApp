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
            IGroupService groupService = new GroupService(groupRepository, studentRepository);
            IStudentService studentService = new StudentService(studentRepository, groupRepository);


            while (true)
            {
                ShowMenu();
                string choice = Helper.GetValidatedStringAlphaNumeric("Make a choice: ");

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
                        case "11": DeleteStudent(studentService); break;
                        case "12": GetStudentsByAge(studentService); break;
                        case "13": GetStudentsByGroupId(studentService); break;
                        case "14": SearchGroupsByName(groupService); break;
                        case "15": SearchStudentsByNameOrSurname(studentService); break;
                        case "0": return;
                        default:
                            Console.Beep();
                            Helper.TypeWriterMessage("Invalid choice. Try again!", ConsoleColor.Red);
                            break;
                    }
                }
                catch (DuplicateGroupException ex)
                {
                    Console.Beep();
                    Helper.TypeWriterMessage(ex.Message, ConsoleColor.Red);
                }
                catch (NotFoundException ex)
                {
                    Console.Beep();
                    Helper.TypeWriterMessage(ex.Message, ConsoleColor.Red);
                }
                catch (Exception ex)
                {
                    Console.Beep();
                    Helper.TypeWriterMessage("Unexpected error: " + ex.Message, ConsoleColor.Red);
                }
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("=== CourseApp Menu ===", ConsoleColor.Cyan);
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

        #region Group Methods

        public static void CreateGroup(IGroupService groupService)
        {
            var name = Helper.GetValidatedStringLettersOnly("Name: ");
            var teacher = Helper.GetValidatedStringLettersOnly("Teacher: ");
            var room = Helper.GetValidatedStringAlphaNumeric("Room: ");

            groupService.Create(new Group { Name = name, Teacher = teacher, Room = room });
            Console.Beep();
            Helper.TypeWriterMessage("Group created successfully!", ConsoleColor.Green);
        }

        public static void UpdateGroup(IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Update Group ==", ConsoleColor.Yellow);
            GetAllGroups(groupService);
            int id = Helper.GetValidatedInteger("Enter the Id of the Group you want to update: ");
            var existingGroup = groupService.GetById(id);

            Helper.TypeWriterMessage("Leave fields empty if you do not want to change them", ConsoleColor.DarkGray);

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
            Console.Beep();
            Helper.TypeWriterMessage("Group updated successfully!", ConsoleColor.Green);
        }

        public static void DeleteGroup(IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Delete Group ==", ConsoleColor.Yellow);
            var id = Helper.GetValidatedInteger("Group Id: ");
            groupService.Delete(id);
            Console.Beep();
            Helper.TypeWriterMessage("Group deleted successfully!", ConsoleColor.Green);
        }

        public static void GetGroupById(IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Get Group By Id ==", ConsoleColor.Yellow);
            int id = Helper.GetValidatedInteger("Group Id: ");
            var group = groupService.GetById(id);
            PrintGroup(group);
        }

        public static void GetGroupsByTeacher(IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Get Groups By Teacher ==", ConsoleColor.Yellow);
            var teacher = Helper.GetValidatedStringLettersOnly("Teacher: ");
            var list = groupService.GetByTeacher(teacher);
            foreach (var g in list) PrintGroup(g);
        }

        public static void GetGroupsByRoom(IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Get Groups By Room ==", ConsoleColor.Yellow);
            var room = Helper.GetValidatedStringAlphaNumeric("Room: ");
            var list = groupService.GetByRoom(room);
            foreach (var g in list) PrintGroup(g);
        }

        public static void GetAllGroups(IGroupService groupService)
        {
            Helper.TypeWriterMessage("== All Groups ==", ConsoleColor.Yellow);
            var groupList = groupService.GetAll();
            if (!groupList.Any())
            {
                Helper.TypeWriterMessage("No groups available.", ConsoleColor.Red);
                return;
            }
            foreach (var group in groupList)
                PrintGroup(group);
        }

        public static void SearchGroupsByName(IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Search Groups By Name ==", ConsoleColor.Yellow);
            var name = Helper.GetValidatedStringLettersOnly("Name search: ");
            var list = groupService.SearchByName(name);
            foreach (var g in list) PrintGroup(g);
        }

        #endregion

        #region Student Methods

        public static void CreateStudent(IStudentService studentService, IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Create Student ==", ConsoleColor.Yellow);

            var name = Helper.GetValidatedStringLettersOnly("Name: ");
            var surname = Helper.GetValidatedStringLettersOnly("Surname: ");
            var age = Helper.GetValidatedInteger("Age: ");

            Console.Write("Assign to group? (y/n): ");
            var yn = Console.ReadLine();
            Group? group = null;
            if (!string.IsNullOrEmpty(yn) && yn.Trim().ToLower() == "y")
            {
                var groups = groupService.GetAll();
                foreach (var g in groups) Console.WriteLine($"Id:{g.Id} Name:{g.Name}");
                var groupId = Helper.GetValidatedInteger("Group Id: ");
                group = groupService.GetById(groupId);
            }

            studentService.Create(new Student { Name = name, Surname = surname, Age = age, Group = group });
            Console.Beep();
            Helper.TypeWriterMessage("Student created successfully!", ConsoleColor.Green);
        }

        public static void UpdateStudent(IStudentService studentService, IGroupService groupService)
        {
            Helper.TypeWriterMessage("== Update Student ==", ConsoleColor.Yellow);
            int id = Helper.GetValidatedInteger("Student Id: ");
            var existing = studentService.GetById(id);

            Helper.TypeWriterMessage("Leave fields empty if you do not want to change them", ConsoleColor.DarkGray);

            Console.Write($"Name ({existing.Name}): ");
            string name = Console.ReadLine();

            Console.Write($"Surname ({existing.Surname}): ");
            string surname = Console.ReadLine();

            Console.Write($"Age ({existing.Age}): ");
            string ageInput = Console.ReadLine();
            int age = string.IsNullOrWhiteSpace(ageInput) ? 0 : int.Parse(ageInput);

            Console.Write("Assign to group? (y/n): ");
            var yn = Console.ReadLine();
            Group? group = null;
            if (!string.IsNullOrEmpty(yn) && yn.Trim().ToLower() == "y")
            {
                var groups = groupService.GetAll();
                foreach (var g in groups) Console.WriteLine($"Id:{g.Id} Name:{g.Name}");
                var groupId = Helper.GetValidatedInteger("Group Id: ");
                group = groupService.GetById(groupId);
            }

            var studentUpdate = new Student
            {
                Id = id,
                Name = string.IsNullOrWhiteSpace(name) ? null : name,
                Surname = string.IsNullOrWhiteSpace(surname) ? null : surname,
                Age = age,
                Group = group
            };

            studentService.Update(studentUpdate);
            Console.Beep();
            Helper.TypeWriterMessage("Student updated successfully!", ConsoleColor.Green);
        }

        public static void DeleteStudent(IStudentService studentService)
        {
            Helper.TypeWriterMessage("== Delete Student ==", ConsoleColor.Yellow);
            int id = Helper.GetValidatedInteger("Student Id: ");
            studentService.Delete(id);
            Console.Beep();
            Helper.TypeWriterMessage("Student deleted successfully!", ConsoleColor.Green);
        }

        public static void GetStudentById(IStudentService studentService)
        {
            Helper.TypeWriterMessage("== Get Student By Id ==", ConsoleColor.Yellow);
            int id = Helper.GetValidatedInteger("Student Id: ");
            var student = studentService.GetById(id);
            PrintStudent(student);
        }

        public static void GetStudentsByAge(IStudentService studentService)
        {
            Helper.TypeWriterMessage("== Get Students By Age ==", ConsoleColor.Yellow);
            int age = Helper.GetValidatedInteger("Age: ");
            var list = studentService.GetByAge(age);
            foreach (var s in list) PrintStudent(s);
        }

        public static void GetStudentsByGroupId(IStudentService studentService)
        {
            Helper.TypeWriterMessage("== Get Students By Group Id ==", ConsoleColor.Yellow);
            int groupId = Helper.GetValidatedInteger("Group Id: ");
            var list = studentService.GetByGroupId(groupId);
            foreach (var s in list) PrintStudent(s);
        }

        public static void SearchStudentsByNameOrSurname(IStudentService studentService)
        {
            Helper.TypeWriterMessage("== Search Students By Name or Surname ==", ConsoleColor.Yellow);
            var search = Helper.GetValidatedStringLettersOnly("Search: ");
            var list = studentService.SearchByNameOrSurname(search);
            foreach (var s in list) PrintStudent(s);
        }

        #endregion

        #region Print Helpers
        public static void PrintGroup(Group group)
        {
            Console.WriteLine($"Id:{group.Id} | Name:{group.Name} | Teacher:{group.Teacher} | Room:{group.Room}");
        }

        public static void PrintStudent(Student student)
        {
            var group = student.Group != null ? $"{student.Group.Id}:{student.Group.Name}" : "NoGroup";
            Console.WriteLine($"Id:{student.Id} | {student.Name} {student.Surname} | Age:{student.Age} | Group:{group}");
        }
        #endregion
    }
}
