namespace UniversitySimulation
{
    public class Student
    {
        public string Name { get; private set; }
        public int Age { get; set; }
        private static int instanceCounter;       
        public int Id { get; private set; }

        internal int NumberOfCourses { get; set; } // encapsulation problem? no bc internal

        public Student(string name)
        {
            Name = name;
            NumberOfCourses = 0;
            Id = ++instanceCounter;
        }
    }

}
