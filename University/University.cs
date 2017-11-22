using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversitySimulation
{
    public class University
    {
        public string Info { get; private set; }
        private List<Course> courses;
        private List<Student> students;
        private List<StudentCourse> StudentCourses;

        public string Courses
        {
            get
            {
                var result = "Available Courses:\t";
                foreach (var course in courses)
                {
                    result += course.Title + "\t";
                }
                return result;
            }
        }
        public string Students
        {
            get
            {
                var result = "University Students:\t";
                foreach (var student in students)
                {
                    result += student.Name + "\t";
                }
                return result;
            }
        }

        private string message;

        public University(string name)
        {
            Info = name;
            courses = new List<Course>();
            students = new List<Student>();
            StudentCourses = new List<StudentCourse>();
        }


        public string AddUniStudent(Student student)
        {
            if (student == null) 
            {
                throw new ArgumentNullException();
            }

            message = $"Student {student.Name} successfully added to University {Info}.\n";
            students.Add(student);
            return message;
        }


        public string AddCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException();
            }
            courses.Add(course);
            return $"Course {course.Title} succesfully added to University {Info}.\n";
        }      

        public string Register(Student student, Course course)
        {
            message = "";
            if (student == null || course == null)
            {
                throw new ArgumentNullException();
            }

            if (!courses.Contains(course))
            {
                return $"There is no course {course.Title} available! See Available Courses.\n";
            }

            if (!students.Contains(student))
            {
                message += AddUniStudent(student);
            }

            if (student.NumberOfCourses >= 5) //edw exw 8emataki
            {
                return "A student can register up to 5 courses.\n";
            }

            /// here is the important part

            var studentCourse = StudentCourses.SingleOrDefault(i => i.Student.Id == student.Id && i.Course.Id == course.Id);
            // an h lista to periexei hdh epistefetai alliws epistefei null 

            if (studentCourse != null) // 
            {
                if (studentCourse.IsComplete)
                {
                    return $"Student {student.Name} has already passed course {course.Title} with grade {studentCourse.Grade}.\n";
                }

                if (studentCourse.IsActive)
                {
                    return $"Student {student.Name} is already registered to {course.Title}.\n";
                }

                studentCourse.IsActive = true;
                student.NumberOfCourses++;
                return $"Student {student.Name} successfully registered to course {course.Title} again.\n";
                // I don't save previous registers & grades  // lush vale id sto Studentcourse?!////////          
            }

            studentCourse = new StudentCourse(student, course); // edw ftiaxnw kainourio opote den 3erw an 8a 
            StudentCourses.Add(studentCourse);

            studentCourse.IsActive = true;

            student.NumberOfCourses++;
            message += $"Student {student.Name} successfully registered to course {course.Title}.\n";

            return message;
        }

        public string AddGrade(Student student, Course course, double grade)
        {
            if (student == null || course == null)
            {
                throw new ArgumentNullException();
            }

            if (!courses.Contains(course))
            {
                return $"Course {course.Title} not found.\n";
            }

            if (!students.Contains(student))
            {
                return $"Student {student.Name} not found.\n";
            }

            if (grade < 0 || grade > 10)
            {
                return $"Grade {grade} must be between 0 and 10.\n";
            }

            var studentCourse = StudentCourses.SingleOrDefault(i => i.Student.Id == student.Id && i.Course.Id == course.Id);
            // an h lista to periexei hdh epistefetai alliws epistefei null 
            if (studentCourse == null)
            {
                return $"Student {student.Name} is not registered to course {course.Title}.\n";
            }

            if (studentCourse.IsComplete)
            {
                return $"Student {student.Name} has already passed course {course.Title} with grade {studentCourse.Grade}.\n";
            }

            if (!studentCourse.IsActive)
            {
                return $"Student {student.Name} is not registered to {course.Title} this semester.\n";
            }

            studentCourse.AddGrade(grade);
            student.NumberOfCourses--;
            return $"Student {student.Name} unregistered from course {course.Title} with grade {grade}.\n";
        }

        public double? GetMedian(Student student, bool passingGradesOnly, out string message)
        {
            if (student == null)
            {
                throw new ArgumentNullException();
            }

            if (!students.Contains(student))
            {
                message = $"Student {student.Name} not found.\n";
                return 0;
            }

            if (passingGradesOnly)
            {
                var averagePassing = StudentCourses
                   .Where(i => i.Student.Id == student.Id && i.IsComplete == true) // h equals gia to prwto
                   .Average(i => i.Grade);

                if (averagePassing == null)
                {
                    message = $"Student {student.Name} hasn't successfully completed any courses.\n";
                    return 0;
                }

                message = $"Student's {student.Name} median of complete courses is {averagePassing,3}.\n";

                return averagePassing;
            }

            var averageAll = StudentCourses
                .Where(i => i.Student.Id == student.Id && i.Grade != null)
                .Average(i => i.Grade);

            if (averageAll == null)
            {
                message = $"Student {student.Name} hasn't completed any courses.\n";
                return 0;
            }


            message = $"Student's {student.Name} median of all courses is {averageAll,3}.\n";
            return averageAll;

            //allos tropos :
            //if (passingGradesOnly)
            //{
            //        var passingGradesOfStudent = new List<StudentCourse>();
            //    double? sumPassing = 0.0;

            //    foreach (var studentcourse in StudentCourses)
            //    {
            //        if (studentcourse.Student.Id == student.Id && studentcourse.IsComplete)
            //        {
            //            passingGradesOfStudent.Add(studentcourse);
            //            sumPassing += studentcourse.Grade;
            //        }
            //    }

            //    if (passingGradesOfStudent.Count <= 0)
            //    {
            //        message = $"Student {student.Name} hasn't successfully completed any courses.\n";
            //        return 0;
            //    }

            //    var passingGradesAverage = sumPassing / passingGradesOfStudent.Count;
            //    message = $"Student's {student.Name} median of complete courses is {passingGradesAverage}.\n";

            //    return passingGradesAverage;
            //}

            //
            //var allGradesOfStudent = new List<StudentCourse>();
            //double? sumAll = 0.0;
            //foreach (var studentcourse in StudentCourses)
            //{
            //    if( studentcourse.Student.Id == student.Id && studentcourse.Grade != null)
            //    {
            //        allGradesOfStudent.Add(studentcourse);
            //        sumAll += studentcourse.Grade;
            //    }
            //}

            //if (allGradesOfStudent.Count <= 0)
            //{
            //    message = $"Student {student.Name} hasn't completed any courses.\n";
            //    return 0;
            //}

            //var allGradesAverage = sumAll / allGradesOfStudent.Count;
            //message = $"Student's {student.Name} median of all courses is {allGradesAverage}.\n";
            //return allGradesAverage;
        }

        public IEnumerable<Student> BestStudent(Course course, bool passingGradesOnly, out string message, DateTime startingDate, DateTime endingDate)
        {
            message = "";
            if (course == null)
            {
                throw new NullReferenceException();
            }

            if (!courses.Contains(course))
            {
                message = $"Course {course.Title} not found.\n";
                return null;
            }

            if (passingGradesOnly)
            {
                var maxGrade = StudentCourses
                   .Where(i => i.Course.Id == course.Id && i.IsComplete == true) // h equals gia to prwto
                   .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
                   .Max(i => i.Grade);

                if (maxGrade == null)
                {
                    message = $"There are no passing grades available for course {course.Title}.\n";
                    return null;
                }

                var bestStudents = StudentCourses
                   .Where(i => i.Course.Id == course.Id && i.IsComplete == true)
                   .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
                   .Where(i => i.Grade == maxGrade)
                   .Select(i => i.Student);

                foreach (var st in bestStudents)
                {
                    message += $"Student {st.Name} is the best in course {course.Title} with grade {maxGrade}.\n";
                }

                return bestStudents;
            }

            var maxGradeAll = StudentCourses
                .Where(i => i.Course.Id == course.Id && i.Grade != null) // h equals gia to prwto
                .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
                .Max(i => i.Grade);

            if (maxGradeAll == null)
            {
                message = $"There are no grades available for course {course.Title}.\n";
                return null;
            }

            var bestStudentsAll = StudentCourses
               .Where(i => i.Course.Id == course.Id && i.Grade != null)
               .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
               .Where(i => i.Grade == maxGradeAll)
               .Select(i => i.Student);

            foreach (var st in bestStudentsAll)
            {
                message += $"Student {st.Name} is the best in course {course.Title} with grade {maxGradeAll}.\n";
            }

            return bestStudentsAll;
        }

        public IEnumerable<Student> BestStudent(Course course, bool passingGradesOnly, out string message)
        {
            DateTime startingDate = DateTime.MinValue;
            DateTime endingDate = DateTime.Now;

            var bestStudents = BestStudent(course, passingGradesOnly, out message, startingDate, endingDate);
            return bestStudents;
        }

        public IEnumerable<Student> WorstStudent(Course course, bool passingGradesOnly, out string message, DateTime startingDate, DateTime endingDate)
        {
            message = "";
            if (course == null)
            {
                throw new NullReferenceException();
            }

            if (!courses.Contains(course))
            {
                message = $"Course {course.Title} not found.\n";
                return null;
            }

            if (passingGradesOnly)
            {
                var minGrade = StudentCourses
                   .Where(i => i.Course.Id == course.Id && i.IsComplete == true) // h equals gia to prwto
                   .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
                   .Min(i => i.Grade);

                if (minGrade == null)
                {
                    message = $"There are no passing grades available for course {course.Title}.\n";
                    return null;
                }

                var worstStudents = StudentCourses
                   .Where(i => i.Course.Id == course.Id && i.IsComplete == true)
                   .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
                   .Where(i => i.Grade == minGrade)
                   .Select(i => i.Student);

                foreach (var st in worstStudents)
                {
                    message += $"Student {st.Name} is the best in course {course.Title} with grade {minGrade}.\n";
                }

                return worstStudents;
            }

            var minGradeAll = StudentCourses
                .Where(i => i.Course.Id == course.Id && i.Grade != null) // h equals gia to prwto
                .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
                .Min(i => i.Grade);

            if (minGradeAll == null)
            {
                message = $"There are no grades available for course {course.Title}.\n";
                return null;
            }

            var worstStudentsAll = StudentCourses
               .Where(i => i.Course.Id == course.Id && i.Grade != null)
               .Where(i => i.AddedGrade >= startingDate && i.AddedGrade <= endingDate)
               .Where(i => i.Grade == minGradeAll)
               .Select(i => i.Student);

            foreach (var st in worstStudentsAll)
            {
                message += $"Student {st.Name} is the best in course {course.Title} with grade {minGradeAll}.\n";
            }

            return worstStudentsAll;
        }

        public IEnumerable<Student> WorstStudent(Course course, bool passingGradesOnly, out string message)
        {
            DateTime startingDate = DateTime.MinValue;
            DateTime endingDate = DateTime.Now;

            var worstStudents = WorstStudent(course, passingGradesOnly, out message, startingDate, endingDate);
            return worstStudents;
        }

    }
}


