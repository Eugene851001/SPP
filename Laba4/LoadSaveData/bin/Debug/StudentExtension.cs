using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendsStudent
{
    public static class StudentExtension
    {
        public static void Study(this DTO.Student student)
        {
            student.Age++;
        }
    }
}
