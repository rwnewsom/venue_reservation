using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    class ReservationSqlDAO
    {
        private readonly string connectionString;
        //Build a list of reservations overlapping requested date.
        //Deny request unless list is null or empty.
        private const string SearchSpaceDate = "";
        private const string ReserveSpace = "";


    }
}
