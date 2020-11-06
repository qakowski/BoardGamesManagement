using System;
using BoardGamesManagement.Domain;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace BoardGamesManagement.Database.NHibernateDb
{
    public class GameHistoryMap : ClassMapping<GameHistory>
    {
        public GameHistoryMap()
        {
            Lazy(false);
            Id(p => p.Id, p =>
            {
                p.Generator(Generators.Guid);
                p.Type(NHibernateUtil.Guid);
                p.Column("Id");
                p.UnsavedValue(Guid.NewGuid());
            });

            Property(p => p.Source, p =>
            {
                p.Length(20);
                p.Type(NHibernateUtil.StringClob);
                p.NotNullable(true);
            });

            ManyToOne(p => p.Game, m =>
            {
                m.Cascade(Cascade.All);
            });

            Property(p => p.DisplayDate, p =>
            {
                p.Type(NHibernateUtil.DateTime);
            });

            Table("GameHistories");
        }
    }
}
