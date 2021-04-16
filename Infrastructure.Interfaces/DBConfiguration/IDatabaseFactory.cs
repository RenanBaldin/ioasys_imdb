using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Interfaces.DBConfiguration
{
    public interface IDatabaseFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}
