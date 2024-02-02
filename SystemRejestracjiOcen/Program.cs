using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SystemRejestracjiOcen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Program do rejestracji studentów i ich ocen z przedmiotów.\n" +
                "Wybierz opcję z menu:\n" +
                "1. Dodaj studenta/ usuń studenta\n" +
                "2. Dodaj przedmiot/ usuń przedmiot\n" +
                "3. Dodaj ocenę/ usuń ocenę\n" +
                "4. Wyświetl dane\n" +
                "5. Zakończ i zapisz\n");
                
                Registrar registrar = new Registrar();
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1: //dodaj studenta
                            Console.WriteLine("1. Dodaj studenta\n" +
                                "2. Usuń studenta");
                            try
                            {
                                int firstChoice = Convert.ToInt32(Console.ReadLine());
                                if (firstChoice == 1)
                                {
                                    Console.WriteLine("Opcja dodaj studenta. Podaj imię:");
                                    string firstName = Console.ReadLine();
                                    Console.WriteLine("Podaj nazwisko:");
                                    string lastName = Console.ReadLine();
                                    Console.WriteLine("Podaj numer studenta:");
                                    int indexNumber = Convert.ToInt32(Console.ReadLine());
                                    registrar.AddStudent(new Student(firstName, lastName, indexNumber));
                                    //registrar.DisplayStudentData();
                                }
                                else if (firstChoice == 2) // chyba nie działa
                                {
                                    Console.WriteLine("Opcja usuń studenta");
                                    Console.WriteLine("Podaj imię:");
                                    string firstName = Console.ReadLine();
                                    Console.WriteLine("Podaj nazwisko:");
                                    string lastName = Console.ReadLine();
                                    Console.WriteLine("Podaj numer studenta:");
                                    int indexNumber = Convert.ToInt32(Console.ReadLine());
                                    registrar.Students.Remove(new Student(firstName, lastName, indexNumber));
                                    //registrar.DisplayStudentData();
                                }
                                else
                                {
                                    Console.WriteLine("Błędna opcja.");
                                }
                                //registrar.DisplayStudentData();
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 2: //dodaj/ usun przedmiot
                            Console.WriteLine("1. Dodaj przedmiot\n" +
                                "2. Usuń przedmiot");
                            int secondChoice = Convert.ToInt32(Console.ReadLine());
                            if (secondChoice == 1)
                            {
                                Console.WriteLine("Opcja dodaj przedmiot. Podaj nazwę przedmiotu: ");
                                string subjectName = Console.ReadLine();
                                Console.WriteLine("Podaj kod przedmiotu: ");
                                string subjectCode = Console.ReadLine();
                                registrar.AddSubject(new Subject(subjectName, subjectCode));
                                //registrar.DisplaySubjectData();
                            }
                            else if (secondChoice == 2)
                            {
                                Console.WriteLine("Opcja usuń przedmiot. Podaj nazwę przedmiotu: ");
                                string subjectName = Console.ReadLine();
                                Console.WriteLine("Podaj kod przedmiotu: ");
                                string subjectCode = Console.ReadLine();
                                registrar.RemoveSubject(new Subject(subjectName, subjectCode));
                                //registrar.DisplaySubjectData();
                            }
                            else
                            {
                                Console.WriteLine("Błędna opcja.");
                            }
                            break;

                        case 3: //dodaj/ usuń ocenę
                            Console.WriteLine("1. Dodaj ocenę\n" +
                                "2. Usuń ocenę");
                            int thirdChoice = Convert.ToInt32(Console.ReadLine());
                            if (thirdChoice == 1)
                            {
                                Console.WriteLine("Wpisz ocenę: ");
                                int gradeValue = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Ocena z przedmiotu: ");
                                //do dokończenia Subject subject =  
                            }
                            break;

                        case 4: //chyba nie działa
                            registrar.DisplayStudentData();
                            Console.WriteLine("Wybierz opcję:\n" +
                                "1. Wyświetl studentów\n" +
                                "2. Wyświetl przedmioty\n" +
                                "3. Wyświetl oceny");
                            int fourthChoice = Convert.ToInt32(Console.ReadLine());
                            if (fourthChoice == 1)
                            {
                                registrar.DisplayStudentData();
                            }
                            else if (fourthChoice == 2)
                            {
                                registrar.DisplaySubjectData();
                            }
                            else if (fourthChoice == 3)
                            {
                                registrar.DisplayGradeData();
                            }
                            break;

                        case 5: //zakończ
                            Console.WriteLine("Naciśnij dowolny przycisk.");
                            Console.ReadKey();
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }

    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IndexNumber { get; set; }
        public List<Grade> Grades { get; set; }

        public Student(string firstName, string lastName, int indexNumber) 
        { 
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IndexNumber = indexNumber;
            this.Grades = new List<Grade>();
        }
    }

    class Subject
    {
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }

        public Subject(string subjectName, string subjectCode)
        {
            this.SubjectName = subjectName;
            this.SubjectCode = subjectCode;
        }
    }

    class Grade
    {
        public string GradeValue { get; set; }
        public Subject SubjectAssign { get; set; }

        public Grade(string gradeValue, Subject subjectAssign) 
        {  
            this.GradeValue = gradeValue; 
            this.SubjectAssign = subjectAssign;
        }
    }

    class Registrar
    {
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set;}
        public List<Grade> Grades { get;set;} 

        public Registrar()
        {
            this.Students = new List<Student>();
            this.Subjects = new List<Subject>();
            this.Grades = new List<Grade>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
        }

        public void AddGrade(Grade grade, Student student)
        {
            Grades.Add(grade);
            student.Grades.Add(grade);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        public void RemoveSubject(Subject subject)
        {
            Subjects.Remove(subject);
        }

        public void RemoveGrade(Grade grade, Student student)
        {
            Grades.Remove(grade);
            student.Grades.Remove(grade);
        }

        public void DisplayStudentData()
        {
            foreach (var student in Students)
            {
                Console.WriteLine($"Student: {student.FirstName} {student.LastName}, numer indeksu: {student.IndexNumber}");
            }
        }

        public void DisplaySubjectData()
        {
            foreach (var subject in Subjects)
            {
                Console.WriteLine($"Przedmiot: {subject.SubjectName}, kod przedmiotu: {subject.SubjectCode}");
            }
        }

        public void DisplayGradeData()
        {
            foreach (var grade in Grades)
            {
                Console.WriteLine($"Przedmiot: {grade.SubjectAssign}, ocena: {grade.GradeValue}");
            }
        }
    }
}
