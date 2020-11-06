using System;
using System.Collections.Generic;
using Autofac;
using BoardGamesManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#if EF
namespace BoardGamesManagement.Database
{
    public static class Extensions
    {
        public static void RegisterContext<TContext>(this ContainerBuilder builder) where TContext : DbContext
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<DatabaseOptions>("DatabaseOptions");
                return options;
            })
            .InstancePerLifetimeScope()
            .AsSelf();

            builder.Register(context =>
            {
                var serviceProvider = context.Resolve<IServiceProvider>();
                var options = context.Resolve<DatabaseOptions>();
                var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions).UseSqlServer(options.ConnectionString);

                return optionsBuilder.Options;
            })
            .As<DbContextOptions<TContext>>()
            .InstancePerLifetimeScope(); ;

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }


        public static void RegisterRepository<TEntity>(this ContainerBuilder builder) where TEntity : BaseEntity
        {
            builder.Register(context =>
                new Repository<TEntity>(context.Resolve<BoardGamesContext>())
            ).As<IRepository<TEntity>>().InstancePerLifetimeScope();
        }

        private static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
}
#endif