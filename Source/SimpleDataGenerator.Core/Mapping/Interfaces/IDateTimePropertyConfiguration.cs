using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Mapping.Interfaces
{
    public interface IDateTimePropertyConfiguration : IPropertyConfiguration<DateTime>
    {
        void InRange(DateTime fromDate, DateTime toDate);
    }
}
