using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;

namespace SimpleDataGenerator.Core.Customization.Specimens
{
    public class EmtpySpecimenBuilder: ISpecimenBuilder
    {
        private readonly PropertyInfo _selectedProperty;
        public EmtpySpecimenBuilder(PropertyInfo propertyInfo)
        {
            _selectedProperty = propertyInfo;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var property = request as PropertyInfo;
            if (property != null && property.Equals(_selectedProperty))
            {
                return property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null;
            }
            
            return new NoSpecimen();
        }

    }
}
