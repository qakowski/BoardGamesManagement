using Autofac;
using BoardGamesManagement.Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.ConfigurationSchema;
using NHibernate.Transform;

namespace BoardGamesManagement.Database.NHibernateDb
{
    public static class Extensions
    {
        //public static void RegisterSession(this ContainerBuilder builder)
        //{
        //    var config = new Configuartion();
        //    var sessionFactory = config.BuildSessionFactory();

        //    builder.RegisterInstance(sessionFactory).As<ISessionFactory>().SingleInstance();
        //    builder.Register(p =>
        //    {
        //        return sessionFactory.OpenSession();
        //    }).As<ISession>().InstancePerLifetimeScope();
        //}

        //public static void RegisterRepository<TEntity>(this ContainerBuilder builder) where TEntity : BaseEntity
        //{
        //    builder.Register(context => new NHibRepository<TEntity>(context.Resolve<ISession>())).As<IRepository<TEntity>>().InstancePerLifetimeScope();
        //}

        public static void RegisterSessionFactory(this ContainerBuilder builder, string connectionString)
        {
            var mapper = new NHibernate.Mapping.ByCode.ModelMapper();
            mapper.AddMappings(typeof(Extensions).Assembly.ExportedTypes);
            var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Validate;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });

            configuration.AddMapping(domainMapping);

            builder.Register(p =>
            {
                return configuration.BuildSessionFactory();
            }).As<ISessionFactory>().SingleInstance();
        }

        public static void RegisterSession(this ContainerBuilder builder)
        {
            builder.Register(context => context.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerLifetimeScope();
        }

        public static void RegisterNHibRepository<TEntity>(this ContainerBuilder builder) where TEntity : BaseEntity
        {
            builder.Register(context => new NHibRepository<TEntity>(context.Resolve<ISession>())).As<IRepository<TEntity>>().InstancePerLifetimeScope();
        }

    }
}
