using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Mapping.Interfaces
{
    public interface IStringPropertyConfiguration : IPropertyConfiguration<string>
    {
        void WithLength(int length);
        void IsEmail();
        void WithLengthRange(int minimumLength, int maximumLength);
    }
}
