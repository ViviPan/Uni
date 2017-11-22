# Uni

Contains a library University with 4 classes: Student, Course, StudentCourse and University, 
and a Console project that just tests some of the library's utilities.

For a the library University to operate a Univeristy instance needs to be created.
Also Students and Courses should be created in order to add them to the University instance 
using the methods AddUniStudent and AddCourse.
Every Student and every Course are unique.
There are properties (Courses, Students) that return a string with the registered courses and students accordingly.

A Student can register to up to 5 university courses. 
When a student tries to register to an existing course he is automatically added to University.

Every method returns a string that explains what happened.
The program crashes if a null reference is inserted to any of its methods.
Most possible errors just return a message and do nothing else!

Methods:
AddUniStudent (adds a student obj to university)
AddCourse (adds a course obj to university)
Register (connects a course with a student (first time) and/or makes connection Active. 
          A student can have up to 5 active connections)
AddGrade (adds a grade to an active incomplete student-course and makes the connection InActive, 
          if the grade is a passing grade then the connection is Complete
GetMedian (Returns the median of a students at all courses of just the ones with passing course)
BestStudent (Returns the best student on a course all grades or just passing! 
              There is the possibility to find the best student betsween 2 dates)
WorstStudent (The same one about the worst student of a course)

