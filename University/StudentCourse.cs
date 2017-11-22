using System;

namespace UniversitySimulation
{
    internal class StudentCourse
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public double? Grade { get; private set; }  
        internal bool IsActive { get; set; } // encapsulation problem? no because internal
        public bool IsComplete { get; private set; }
        public DateTime AddedGrade { get; private set; }

        //public int Id   // do I need that? NO
        //{
        //    get
        //    {
        //        var id = "Student.Id" + "Course.Id";
        //        return int.Parse(id);
        //    }
        //}

        public StudentCourse(Student student, Course course)
        {
            Student = student;
            Course = course;
            IsActive = false;
            IsComplete = false;
        }

        public void AddGrade(double grade)
        {
            Grade = grade;
            IsActive = false;
            AddedGrade = DateTime.Today;

            if (Grade >= 5)
            {
                IsComplete = true;                
            }
        }
    }
}
