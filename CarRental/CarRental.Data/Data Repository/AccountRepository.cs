﻿using CarRental.Business.Entities;
using CarRental.Data.Contract.Repository_Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace CarRental.Data.Data_Repository
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        public Account GetByLogin(string login)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                return (from e in context.AccountSet
                        where e.LoginEmail == login
                        select e).FirstOrDefault();
            }
        }

        // Just normal Entity Framework
        protected override Account AddEntity(CarRentalContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override IEnumerable<Account> GetEntities(CarRentalContext entityContext)
        {
            return from e in entityContext.AccountSet
                   select e;
        }

        protected override Account GetEntity(CarRentalContext entityContext, int id)
        {
            var query = (from e in entityContext.AccountSet
                         where e.AccountID == id
                         select e);
            return query.FirstOrDefault();
        }

        protected override Account UpdateEntity(CarRentalContext entityContext, Account entity)
        {
            var query = (from e in entityContext.AccountSet
                         where e.AccountID == entity.AccountID
                         select e);
            return query.FirstOrDefault();
        }
    }
}
