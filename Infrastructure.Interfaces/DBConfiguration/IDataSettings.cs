using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.DBConfiguration
{
    public interface IDataSettings
    {
        string DefaultConnection { get; set; }
    }
}
