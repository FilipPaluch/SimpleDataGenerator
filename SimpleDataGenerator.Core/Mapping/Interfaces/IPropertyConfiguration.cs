using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;

namespace SimpleDataGenerator.Core.Mapping.Interfaces
{

    public interface IPropertyConfiguration
    {
        ISpecimenBuilder CustomSpecimen { get; }
    }

    public interface IPropertyConfiguration<in TProperty> : IPropertyConfiguration
    {
        IPropertyConfiguration<TProperty> WithConstValue(TProperty value);
        IPropertyConfiguration<TProperty> IsEmpty();
    }
}
