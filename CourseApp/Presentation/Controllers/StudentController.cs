using Application.Exceptions;
using Application.Helpers;
using Application.Service.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = Domain.Entities.Group;

namespace Presentation.Controllers
{
    public class StudentController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public StudentController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public  void Create()
        {
            Helper.TypeWriterMessage("== Create Student ==", ConsoleColor.Yellow);

            var name = Helper.GetValidatedStringLettersOnly("Name: ");
            var surname = Helper.GetValidatedStringLettersOnly("Surname: ");
            var age = Helper.GetValidatedInteger("Age: ");

            Console.Write("Assign to group? (y/n): ");
            var yn = Console.ReadLine();
            Group? group = null;

            if (yn?.Trim().ToLower() == "y")
            {
                while (true)
                {
                    try
                    {
                        var groups = _groupService.GetAll();
                        if (!groups.Any())
                        {
                            Helper.TypeWriterMessage("No groups available!", ConsoleColor.Red);
                            break;
                        }

                        foreach (var g in groups)
                            Console.WriteLine($"Id:{g.Id} Name:{g.Name}");

                        int groupId = Helper.GetValidatedInteger("Group Id: ");
                        group = _groupService.GetById(groupId);
                        break;
                    }
                    catch (NotFoundException ex)
                    {
                        Console.Beep();
                        Helper.TypeWriterMessage(ex.Message, ConsoleColor.Red);
                        Helper.TypeWriterMessage("Please enter a valid Group Id", ConsoleColor.Yellow);
                    }
                }
            }

            _studentService.Create(new Student { Name = name, Surname = surname, Age = age, Group = group });
            Console.Beep();
            Helper.TypeWriterMessage("Student created successfully", ConsoleColor.Green);
        }

        public void Update()
        {
            Helper.TypeWriterMessage("== Update Student ==", ConsoleColor.Yellow);

            int id = Helper.GetValidatedInteger("Student Id: ");
            var existing = _studentService.GetById(id);

            Helper.TypeWriterMessage("Leave fields empty if you do not want to change them", ConsoleColor.DarkGray);

            string name;
            while (true)
            {
                Console.Write($"Name ({existing.Name}): ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || name.All(char.IsLetter))
                    break;

                Console.Beep();
                Helper.TypeWriterMessage("Please enter letters only", ConsoleColor.Red);
            }

            string surname;
            while (true)
            {
                Console.Write($"Surname ({existing.Surname}): ");
                surname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(surname) || surname.All(char.IsLetter))
                    break;

                Console.Beep();
                Helper.TypeWriterMessage("Please enter letters only", ConsoleColor.Red);
            }

            int age = existing.Age;
            while (true)
            {
                Console.Write($"Age ({existing.Age}): ");
                string ageInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ageInput))
                    break; 

                if (int.TryParse(ageInput, out int parsedAge))
                {
                    age = parsedAge;
                    break;
                }

                Console.Beep();
                Helper.TypeWriterMessage("Please enter digits only", ConsoleColor.Red);
            }
            Console.Write("Change group? (y/n): ");
            var yn = Console.ReadLine();
            Domain.Entities.Group? group = existing.Group;

            if (yn?.Trim().ToLower() == "y")
            {
                while (true)
                {
                    try
                    {
                        var groups = _groupService.GetAll();
                        foreach (var g in groups)
                            Console.WriteLine($"Id:{g.Id} Name:{g.Name}");

                        int groupId = Helper.GetValidatedInteger("Group Id: ");
                        group = _groupService.GetById(groupId);
                        break;
                    }
                    catch (NotFoundException ex)
                    {
                        Console.Beep();
                        Helper.TypeWriterMessage(ex.Message, ConsoleColor.Red);
                        Helper.TypeWriterMessage("Please enter a valid Group Id", ConsoleColor.Yellow);
                    }
                }
            }

            _studentService.Update(new Student
            {
                Id = id,
                Name = string.IsNullOrWhiteSpace(name) ? existing.Name : name,
                Surname = string.IsNullOrWhiteSpace(surname) ? existing.Surname : surname,
                Age = age,
                Group = group
            });

            Console.Beep();
            Helper.TypeWriterMessage("Student updated successfully", ConsoleColor.Green);
        }
        public void Delete()
        {
            Helper.TypeWriterMessage("== Delete Student ==", ConsoleColor.Yellow);
            int id = Helper.GetValidatedInteger("Student Id: ");

            try
            {
                _studentService.Delete(id);
                Console.Beep();
                Helper.TypeWriterMessage("Student deleted successfully", ConsoleColor.Green);
            }
            catch (NotFoundException ex)
            {
                Console.Beep();
                Helper.TypeWriterMessage(ex.Message, ConsoleColor.Red);
            }
        }
        public void GetById()
        {
            Helper.TypeWriterMessage("== Get Student By Id ==", ConsoleColor.Yellow);
            int id = Helper.GetValidatedInteger("Student Id: ");

            try
            {
                var student = _studentService.GetById(id);
                PrintStudent(student);
            }
            catch (NotFoundException ex)
            {
                Helper.TypeWriterMessage(ex.Message, ConsoleColor.Red);
            }
        }
        public void GetByAge()
        {
            Helper.TypeWriterMessage("== Get Students By Age ==", ConsoleColor.Yellow);
            int age = Helper.GetValidatedInteger("Age: ");
            var list = _studentService.GetByAge(age);

            if (!list.Any())
                Helper.TypeWriterMessage("No students found with this age", ConsoleColor.Red);
            else
                list.ForEach(PrintStudent);
        }
        public void GetByGroupId()
        {
            Helper.TypeWriterMessage("== Get Students By Group Id ==", ConsoleColor.Yellow);
            int groupId = Helper.GetValidatedInteger("Group Id: ");
            var list = _studentService.GetByGroupId(groupId);

            if (!list.Any())
                Helper.TypeWriterMessage("No students found for this group", ConsoleColor.Red);
            else
                list.ForEach(PrintStudent);
        }

        public void SearchByNameOrSurname()
        {
            Helper.TypeWriterMessage("== Search Students By Name or Surname ==", ConsoleColor.Yellow);
            var search = Helper.GetValidatedStringLettersOnly("Search: ");
            var list = _studentService.SearchByNameOrSurname(search);

            if (!list.Any())
                Helper.TypeWriterMessage("No matching students found", ConsoleColor.Red);
            else
                list.ForEach(PrintStudent);
        }
        private void PrintStudent(Student student)
        {
            var group = student.Group != null ? $"{student.Group.Id}:{student.Group.Name}" : "No Group";
            Console.WriteLine($"Id:{student.Id} | {student.Name} {student.Surname} | Age:{student.Age} | Group:{group}");
        }
    }
}
