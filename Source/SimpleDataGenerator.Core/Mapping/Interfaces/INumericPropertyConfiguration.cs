using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Mapping.Interfaces
{
    public interface INumericPropertyConfiguration<in TProperty> : IPropertyConfiguration<TProperty>
    {
        void InRange(TProperty from, TProperty to);
    }
}
