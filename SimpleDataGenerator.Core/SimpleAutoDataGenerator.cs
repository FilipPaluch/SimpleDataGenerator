using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Core.Mapping.Interfaces;

namespace SimpleDataGenerator.Core
{
    public class SimpleAutoDataGenerator : Fixture
    {
        public void WithConfiguration(IEnumerable<EntityConfiguration> entityConfigurations)
        {
            var customSpecimens = entityConfigurations.SelectMany(x => x.GetCustomSpecimens());
            CustomizeFixture(customSpecimens);
        }

        public void WithConfiguration(EntityConfiguration entityConfiguration)
        {
            var customSpecimens = entityConfiguration.GetCustomSpecimens();
            CustomizeFixture(customSpecimens);
        }

        private void CustomizeFixture(IEnumerable<ISpecimenBuilder> customSpecimens)
        {
            foreach (var customSpecimen in customSpecimens)
            {
                Customizations.Add(customSpecimen);
            }
        }

    }
}
