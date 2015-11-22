using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Parameter
{
    public class Rotate : IParameter
    {
        public double Value;

        public Rotate(double rotate)
        {
            Value = rotate;
        }
    }
}
