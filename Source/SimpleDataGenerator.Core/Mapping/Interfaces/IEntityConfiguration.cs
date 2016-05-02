using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Mapping.Implementations;

namespace SimpleDataGenerator.Core.Mapping.Interfaces
{

    public interface IEntityConfiguration<TEntity>
    {
        IStringPropertyConfiguration For(Expression<Func<TEntity, string>> expression);
        INumericPropertyConfiguration<int> For(Expression<Func<TEntity, int>> expression);
    }
}
