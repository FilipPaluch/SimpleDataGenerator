using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Model
{

    public class ConstrainedString
    {
        public ConstrainedString(int minimumLength, int maximumLength)
        {
            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }

        public int MinimumLength { get; private set; }
        public int MaximumLength { get; private set; }
    }
}
