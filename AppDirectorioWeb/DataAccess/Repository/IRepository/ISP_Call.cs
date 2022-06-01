using Dapper;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        #region Public Methods

        void Execute(string procedureName, DynamicParameters param = null);

        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);

        T OneRecord<T>(string procedureName, DynamicParameters param = null);

        T Single<T>(string procedureName, DynamicParameters param = null);

        #endregion Public Methods
    }
}