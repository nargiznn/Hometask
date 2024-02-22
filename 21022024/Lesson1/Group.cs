using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1
{
    internal class Group
    {
        private static int _count;
        public Group()
        {
            _count++;
            Id = _count;
        }
        public int Id { get; set; } 
        public string No { get; set; }  
        public byte Limit { get; set; }

        public override string ToString()
        {
            return Id+"-"+No+"-"+Limit;
        }
    }
}
