using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Model
{
    public class DateTimeRange
    {
        public DateTimeRange(DateTime fromDate, DateTime toDate)
        {

            FromDate = fromDate;
            ToDate = toDate;
        }
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }
    }
}
