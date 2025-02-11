﻿namespace Core.Common.Contract
{
    public interface IBusinessEngineFactory
    {
        // Doing the same work as DataRepositoryFactory
        T GetBusinessEngine<T>() where T : IBusinessEngine;
    }
}
