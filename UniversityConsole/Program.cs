using System;
using UniversitySimulation;

namespace UniversityConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            // Just a test for library UniversitySimulation

            var message = "";
            var m = "";


            //create students
            var studentMary = new Student("Mary");
            var studentJohn = new Student("John");
            var studentVivi = new Student("Vivi");

            //create courses
            var courseMaths = new Course("Maths", 30);
            var courseChem = new Course("Chemistry", 20);
            var courseC = new Course("C", 10);
            var courseArts = new Course("Arts", 10);
            var english = new Course("English", 20);
            var philosophy = new Course("Philosophy", 10);
            var poetry = new Course("Poetry", 10);


            //create university
            var uni = new University("UNI");

            //Add courses to university
            message += uni.AddCourse(courseMaths);
            message += uni.AddCourse(courseChem);
            message += uni.AddCourse(courseC);
            message += uni.AddCourse(courseArts);
            message += uni.AddCourse(english);
            message += uni.AddCourse(philosophy);


            //Add students to university
            message += uni.AddUniStudent(studentMary);

            //------------------------------------------------------
            uni.GetMedian(new Student("Vivi"), true, out m); // student not contained
            message += m;
            uni.GetMedian(studentMary, false, out m); // has not completed any
            message += m;

            message += uni.Register(studentMary, courseMaths);
            message += uni.AddGrade(studentMary, courseMaths, 4); // add a not passing grade

            uni.GetMedian(studentMary, true, out m); // has not any passing
            message += m;

            message += uni.Register(studentMary, courseArts);
            message += uni.AddGrade(studentMary, courseArts, 10); // add a passing grade

            message += uni.Register(studentMary, courseChem);
            message += uni.AddGrade(studentMary, courseChem, 9); // add a passing grade

            uni.GetMedian(studentMary, true, out m); // median of passing
            message += m;

            uni.GetMedian(studentMary, false, out m); // median of all
            message += m;

            //Console.WriteLine(message);
            //------------------------------------------------------
            message += "\n";
            uni.BestStudent(new Course("p", 10), true, out m); // course not contained
            message += m;

            uni.BestStudent(courseC, false, out m); // not any grades
            message += m;
            uni.BestStudent(courseMaths, true, out m); // not passing grades
            message += m;

            message += uni.AddUniStudent(studentJohn);
            message += uni.AddUniStudent(studentVivi);


            message += uni.Register(studentJohn, courseArts);
            message += uni.Register(studentVivi, courseArts);

            message += uni.AddGrade(studentJohn, courseArts, 10); // add a passing grade
            message += uni.AddGrade(studentVivi, courseArts, 8); // add a passing grade

            uni.BestStudent(courseArts, true, out m); // list of 2 students (passing)
            message += m;

            message += uni.Register(studentJohn, courseMaths);
            message += uni.Register(studentVivi, courseMaths);

            message += uni.AddGrade(studentJohn, courseMaths, 2); // add a passing grade
            message += uni.AddGrade(studentVivi, courseMaths, 1); // add a passing grade

            uni.BestStudent(courseMaths, false, out m); // list of 1 student (all grades)
            message += m;

            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
