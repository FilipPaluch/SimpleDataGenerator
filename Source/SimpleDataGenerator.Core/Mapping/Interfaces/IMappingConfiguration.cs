using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Mapping.Interfaces
{
    public interface IMappingConfiguration<TEntity> where TEntity : class
    {
        IPropertyConfiguration<TProperty> For<TProperty>(Expression<Func<TEntity, TProperty>> expression);
    }
}
