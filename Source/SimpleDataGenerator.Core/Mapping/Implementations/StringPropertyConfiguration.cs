using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Customization.Specimens;
using SimpleDataGenerator.Core.Mapping.Interfaces;
using SimpleDataGenerator.Core.Model;

namespace SimpleDataGenerator.Core.Mapping.Implementations
{
    public class StringPropertyConfiguration : PropertyConfiguration<string>, IStringPropertyConfiguration
    {

        public StringPropertyConfiguration(PropertyInfo propertyInfo)
            :base(propertyInfo)
        {
            
        }

        public void WithLength(int length)
        {
            AddCustomSpecimen(new ConstrainedStringSpecimenBuilder(SelectedProperty, new ConstrainedString(length, length)));
        }

        public void WithLengthRange(int minimumLength, int maximumLength)
        {
            AddCustomSpecimen(new ConstrainedStringSpecimenBuilder(SelectedProperty, new ConstrainedString(minimumLength, maximumLength)));
        }

        public void IsEmail()
        {
            AddCustomSpecimen(new EmailAddressSpecimenBuilder(SelectedProperty));
        }

    }
}
