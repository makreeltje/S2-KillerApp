using System;
using System.Collections.Generic;
using System.Text;

namespace PMDB_docker.Data.Movie
{
    class UserDataException : Exception
    {
        public UserDataException(string message) : base(message)
        {

        }
    }
}
