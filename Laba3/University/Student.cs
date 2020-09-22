using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendsStudent;


namespace University
{
    public class Student: DTO.Student
    {

        public Student()
        {
            Age = 18;
            Name = "Dmitriy";
        }

        public void Work()
        {
            this.Study();
        }
    }
}
