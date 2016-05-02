using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Model;

namespace SimpleDataGenerator.Core.Customization.Specimens
{

    public class ConstrainedStringSpecimenBuilder : ISpecimenBuilder
    {

        private readonly PropertyInfo _selectedProperty;
        private readonly ConstrainedString _constrainedString;
        public ConstrainedStringSpecimenBuilder(PropertyInfo propertyInfo, ConstrainedString constrainedString)
        {
            _selectedProperty = propertyInfo;
            _constrainedString = constrainedString;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (IsSelectedProperty(request))
            {
                return context.Resolve(new ConstrainedStringRequest(_constrainedString.MinimumLength, _constrainedString.MaximumLength));
            }

            return new NoSpecimen();
        }

        private bool IsSelectedProperty(object request)
        {
            var property = request as PropertyInfo;
            return property != null && property.Equals(_selectedProperty);
        }
    }
}
