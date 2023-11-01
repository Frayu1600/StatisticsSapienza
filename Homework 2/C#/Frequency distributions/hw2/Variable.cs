using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2
{
    internal class Variable
    {
        public readonly string name;
        public readonly bool isQuantitative;
        public Variable(string name, bool isQuantitative)
        {
            this.name = name;
            this.isQuantitative = isQuantitative;
        }
    }
}
