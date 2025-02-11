﻿using Core.Common.Contract;
using Core.Common.Data;

namespace CarRental.Data.Data_Repository
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, CarRentalContext>
       where T : class, IIdentifiableEntity, new()
    {

    }
}
