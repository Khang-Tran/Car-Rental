﻿using CarRental.Business.Entities;
using CarRental.Data.Contract.Repository_Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CarRental.Data.Contract.DTOs;

namespace CarRental.Data.Data_Repository
{
    [Export(typeof(IRentalRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    public class RentalRepository : DataRepositoryBase<Rental>, IRentalRepository
    {
        public IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo()
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from r in entityContext.RentalSet
                            where r.ReturnDate == null
                            join a in entityContext.AccountSet on r.AccountID equals a.AccountID
                            join c in entityContext.CarSet on r.CarID equals c.CarID
                            select new CustomerRentalInfo()
                            {
                                Customer = a,
                                Rental = r,
                                Car = c
                            };

                return query.ToList().ToArray(); // Using this because it will make sure the data from the query can be use
                                                 // when the entityContext was disposed
                                                 // It overwrites lazy loading of Entity Framework 
            }
        }

        public IEnumerable<Rental> GetCurrentlyRentedCar()
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.ReturnDate == null
                            select e;
                return query.ToArray().ToList();
            }
        }

        public Rental GetCurrentRentalByCar(int carID)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.CarID == carID && e.ReturnDate == null
                            select e;
                return query.FirstOrDefault();
            }
        }

        public IEnumerable<Rental> GetRentalHistoryByAccount(int accountID)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.AccountID == accountID
                            select e;
                return query.ToArray().ToList();
            }
        }

        public IEnumerable<Rental> GetRentalHistoryByCar(int carID)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.CarID == carID
                            select e;
                return query.ToArray().ToList();
            }
        }


        // Just normal Entity Framework
        protected override Rental AddEntity(CarRentalContext entityContext, Rental entity)
        {
            return entityContext.RentalSet.Add(entity);
        }

        protected override IEnumerable<Rental> GetEntities(CarRentalContext entityContext)
        {
            return from e in entityContext.RentalSet
                   select e;
        }

        protected override Rental GetEntity(CarRentalContext entityContext, int id)
        {
            var query = (from e in entityContext.RentalSet
                         where e.RentalID == id
                         select e);
            return query.FirstOrDefault();
        }

        protected override Rental UpdateEntity(CarRentalContext entityContext, Rental entity)
        {
            var query = (from e in entityContext.RentalSet
                         where e.RentalID == entity.RentalID
                         select e);
            return query.FirstOrDefault();
        }
    }
}
