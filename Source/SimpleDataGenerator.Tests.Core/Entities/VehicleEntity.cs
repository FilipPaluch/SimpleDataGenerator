using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Tests.Core.Entities
{
    public class VehicleEntity
    {
        public bool IsActive { get; set; }

        public bool? NullableIsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public int IntKilometers { get; set; }

        public float FloatKilometers { get; set; }
        
        public double DoubleKilometers { get; set; }

        public string Name { get; set; }

        public UserEntity User { get; set; }

        public bool? IsDeactivated { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }
    }
}
