using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class Course
    {
        public string Title { get; private set; }
        public int Ects { get; private set; }
        private static int instanceCounter;
        public int Id { get; private set; }

        public Course(string title, int ects)
        {
            Title = title;
            Ects = ects;
            Id = ++instanceCounter;
        }
    }
}
