using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Model
{
    public class NumberRange
    {
        public NumberRange(object minimum, object maximum)
        {
            Maximum = maximum;
            Minimum = minimum;
        }


        public object Minimum { get; private set; }
        public object Maximum { get; private set; }
    }
}
