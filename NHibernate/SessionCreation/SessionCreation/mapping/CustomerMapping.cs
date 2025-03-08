using ExecuteSqlRow;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Util;

namespace mapping
{
    public class CustomerMapping :ClassMapping<Customer>
    {
        public CustomerMapping()
        {
            Id(x =>x.Id, c =>
            {
                c.Generator(Generators.Identity);
                c.Type(NHibernateUtil.Int32);
                c.Column("Id");
                c.UnsavedValue(0);
            });

            Property(c => c.Name, c =>
            {
                c.Column("Name");
                c.NotNullable(true);
                c.Length(50);
                c.Type(NHibernateUtil.AnsiString);
            });
              
            Property(c => c.Balance, c =>
            {
                c.Column("Balance");
                c.NotNullable(true);
                c.Type(NHibernateUtil.Decimal);
            });
            Table("Customers");

        }

    }
}
