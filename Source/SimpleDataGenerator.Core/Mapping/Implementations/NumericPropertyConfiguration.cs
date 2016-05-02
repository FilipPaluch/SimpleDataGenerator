using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using SimpleDataGenerator.Core.Customization.Specimens;
using SimpleDataGenerator.Core.Mapping.Interfaces;
using SimpleDataGenerator.Core.Model;

namespace SimpleDataGenerator.Core.Mapping.Implementations
{
    public class NumericPropertyConfiguration<TProperty> : PropertyConfiguration<TProperty>, INumericPropertyConfiguration<TProperty>
    {

        public NumericPropertyConfiguration(PropertyInfo propertyInfo)
            :base(propertyInfo)
        {
            
        }

        public void InRange(TProperty minimum, TProperty maximum)
        {
            AddCustomSpecimen(new RangedNumberSpecimenBuilder(SelectedProperty, new NumberRange(minimum, maximum)));
        } 
  
    }
}
