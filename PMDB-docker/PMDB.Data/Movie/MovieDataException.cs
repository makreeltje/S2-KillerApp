using System;
using System.Collections.Generic;
using System.Text;

namespace PMDB_docker.Data.Movie
{
    class MovieDataException : Exception
    {
        public MovieDataException(string message) : base(message)
        {

        }
    }
}
