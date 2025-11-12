using Application.Exceptions;
using Application.Helpers;
using Application.Service.Abstract;
using Application.Service.Concrete;
using Domain.Entities;
using Infrastructure.Repository.Abstract;
using Infrastructure.Repository.Concrete;
using Presentation.Controllers;
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
            var groupController = new GroupController(groupService);
            var studentController = new StudentController(studentService, groupService);

            while (true)
            {
                ShowMenu();
                string choice = Helper.GetValidatedStringAlphaNumeric("Make a choice: ");

                try
                {
                    switch (choice)
                    {
                        case "1": groupController.Create(); break;
                        case "2": groupController.Update(); break;
                        case "3": groupController.Delete(); break;
                        case "4": groupController.GetById(); break;
                        case "5": groupController.GetByTeacher(); break;
                        case "6": groupController.GetByRoom(); break;
                        case "7": groupController.GetAll(); break;
                        case "8": studentController.Create(); break;
                        case "9": studentController.Update(); break;
                        case "10": studentController.GetById(); break;
                        case "11": studentController.Delete(); break;
                        case "12": studentController.GetByAge(); break;
                        case "13": studentController.GetByGroupId(); break;
                        case "14": groupController.SearchByName(); break;
                        case "15": studentController.SearchByNameOrSurname(); break;
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

    }
}
