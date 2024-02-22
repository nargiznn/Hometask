using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework22_02
{
    internal class Brand
    {

            private static int _count;
            public Brand()
            {
                _count++;
                Id = _count;
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Year { get; set; }

            public override string ToString()
            {
                return Id + " " + Name + " " + Year;
            }
    }
}
