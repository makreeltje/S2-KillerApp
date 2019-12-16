using System;
using System.Collections.Generic;
using System.Text;

namespace PMDB_docker.Data.Movie
{
    class DataException : Exception
    {
        public DataException(string message) : base(message)
        {

        }
    }
}
