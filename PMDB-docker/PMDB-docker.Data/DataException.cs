using System;
using System.Collections.Generic;
using System.Text;

namespace PMDB_docker.Data
{
    class DataException : Exception
    {
        public DataException(string message) : base(message)
        {

        }
    }
}
