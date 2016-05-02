using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Extensions;
using SimpleDataGenerator.Core.Mapping.Interfaces;

namespace SimpleDataGenerator.Core.Mapping.Implementations
{

    public class EntityConfiguration
    {

        private readonly List<IPropertyConfiguration> _configurations;

        public EntityConfiguration()
        {
            
            _configurations = new List<IPropertyConfiguration>();
        }

        protected void AddConfiguration(IPropertyConfiguration propertyConfiguration)
        {
            _configurations.Add(propertyConfiguration);
        }

        public IEnumerable<ISpecimenBuilder> GetCustomSpecimens()
        {
            return _configurations.Select(x => x.CustomSpecimen);
        } 
    }

    public class EntityConfiguration<TEntity> : EntityConfiguration
        where TEntity : class
    {


        #region NumericProperty

        public INumericPropertyConfiguration<int> For(Expression<Func<TEntity, int>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<double> For(Expression<Func<TEntity, double>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<float> For(Expression<Func<TEntity, float>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<short> For(Expression<Func<TEntity, short>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<long> For(Expression<Func<TEntity, long>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<decimal> For(Expression<Func<TEntity, decimal>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<int?> For(Expression<Func<TEntity, int?>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<double?> For(Expression<Func<TEntity, double?>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<float?> For(Expression<Func<TEntity, float?>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<short?> For(Expression<Func<TEntity, short?>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<long?> For(Expression<Func<TEntity, long?>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }

        public INumericPropertyConfiguration<decimal?> For(Expression<Func<TEntity, decimal?>> expression)
        {
            return CreateNumericPropertyConfiguration(expression);
        }
        #endregion

        #region StringProperty

        public IStringPropertyConfiguration For(Expression<Func<TEntity, string>> expression)
        {
            return CreateStringPropertyConfiguration(expression);
        }

        #endregion

        #region DateTimeProperty

        public IDateTimePropertyConfiguration For(Expression<Func<TEntity, DateTime>> expression)
        {
            return CreateDateTimePropertyConfiguration(expression);
        }

        public IDateTimePropertyConfiguration For(Expression<Func<TEntity, DateTime?>> expression)
        {
            return CreateDateTimePropertyConfiguration(expression);
        }

        #endregion

        #region BooleanProperty

        public IPropertyConfiguration<bool> For(Expression<Func<TEntity, bool>> expression)
        {
            return CreatePrimitiveProperty(expression);
        }

        public IPropertyConfiguration<bool?> For(Expression<Func<TEntity, bool?>> expression)
        {
            return CreatePrimitiveProperty(expression);
        }

        #endregion

        #region EntityProperty

        public IPropertyConfiguration<TInverse> For<TInverse>(Expression<Func<TEntity, TInverse>> expression) where TInverse : class
        {
            return CreatePrimitiveProperty(expression);
        }

        #endregion

        #region HelpMethods

        private INumericPropertyConfiguration<TProperty> CreateNumericPropertyConfiguration<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var configuration = new NumericPropertyConfiguration<TProperty>(expression.GetPropertyInfo());
            AddConfiguration(configuration);
            return configuration;
        }

        private IDateTimePropertyConfiguration CreateDateTimePropertyConfiguration<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var configuration = new DateTimePropertyConfiguration(expression.GetPropertyInfo());
            AddConfiguration(configuration);
            return configuration;
        }

        private IStringPropertyConfiguration CreateStringPropertyConfiguration<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var configuration = new StringPropertyConfiguration(expression.GetPropertyInfo());
            AddConfiguration(configuration);
            return configuration;
        }

        private IPropertyConfiguration<TProperty> CreatePrimitiveProperty<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var configuration = new PropertyConfiguration<TProperty>(expression.GetPropertyInfo());
            AddConfiguration(configuration);
            return configuration;
        }

        #endregion



    }

 
}
