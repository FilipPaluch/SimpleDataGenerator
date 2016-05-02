using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;

namespace SimpleDataGenerator.Core.Customization.Specimens
{
    public class EmailAddressSpecimenBuilder : ISpecimenBuilder
    {
        private readonly PropertyInfo _selectedProperty;
        public EmailAddressSpecimenBuilder(PropertyInfo propertyInfo)
        {
            _selectedProperty = propertyInfo;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (IsPropertyConfiguredAsEmail(request))
            {
                return CreateEmailAddress(context);
            }

            return new NoSpecimen();
        }

        private object CreateEmailAddress(ISpecimenContext context)
        {
            var address = (MailAddress)context.Resolve(typeof(MailAddress));
            return address.Address;
        }

        private bool IsPropertyConfiguredAsEmail(object request)
        {
            var property = request as PropertyInfo;
            return property != null && property.Equals(_selectedProperty);
        }



    }
}
