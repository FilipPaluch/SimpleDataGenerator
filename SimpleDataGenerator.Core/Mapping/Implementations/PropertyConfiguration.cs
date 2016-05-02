using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Customization.Specimens;
using SimpleDataGenerator.Core.Mapping.Interfaces;

namespace SimpleDataGenerator.Core.Mapping.Implementations
{

    public class PropertyConfiguration<TProperty> : IPropertyConfiguration<TProperty>
    {
        protected PropertyInfo SelectedProperty { get; private set; }

        private ISpecimenBuilder _customSpecimen;
        public ISpecimenBuilder CustomSpecimen
        {
            get { return _customSpecimen; }
        } 

        public PropertyConfiguration(PropertyInfo propertyInfo)
        {
            SelectedProperty = propertyInfo;
        }

        protected void AddCustomSpecimen(ISpecimenBuilder specimenBuilder)
        {
            _customSpecimen = specimenBuilder;
        }

        public IPropertyConfiguration<TProperty> WithConstValue(TProperty value)
        {
            AddCustomSpecimen(new ConstValueSpecimenBuilder(SelectedProperty, value));
            return this;
        }

        public IPropertyConfiguration<TProperty> IsEmpty()
        {
            AddCustomSpecimen(new EmtpySpecimenBuilder(SelectedProperty));
            return this;
        }

    }
}
