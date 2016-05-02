using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Extensions;

namespace SimpleDataGenerator.Tests.Core.Specimens
{
    public class SpecimenBuilderFixture
    {
        public PropertyInfo GetProperty<TEntity>(Expression<Func<TEntity, object>> expression)
        {
            return expression.GetPropertyInfo();
        }
    }
}
