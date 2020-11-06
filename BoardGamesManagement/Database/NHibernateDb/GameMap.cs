using System;
using BoardGamesManagement.Domain;
using FluentNHibernate.Automapping.Steps;
using NHibernate;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace BoardGamesManagement.Database.NHibernateDb
{
    public class GameMap : ClassMapping<Game>
    {
        public GameMap()
        {
            Id(p => p.Id, p =>
            {
                p.Generator(Generators.Guid);
                p.Type(NHibernateUtil.Guid);
                p.Column("Id");
                p.UnsavedValue(Guid.NewGuid());
            });

            Property(p => p.Name, p =>
             {
                 p.Length(20);
                 p.Type(NHibernateUtil.StringClob);
                 p.NotNullable(true);
             });


            Property(p => p.MinRecommendedAge, p =>
            {
                p.Type(NHibernateUtil.Int32);
                p.NotNullable(true);
            });

            Property(p => p.MaxPlayers, p =>
            {
                p.Type(NHibernateUtil.Int32);
                p.NotNullable(true);
            });

            Property(p => p.MinPlayers, p =>
            {
                p.Type(NHibernateUtil.Int32);
                p.NotNullable(true);
            });

            Bag(p => p.GameHistory, p =>
            {
                p.Key(k => k.Column(col => col.Name("Id")));
                p.Cascade(Cascade.All);
                
            }, action => action.OneToMany());
            Table("Games");
        }
    }
}
