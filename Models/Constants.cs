using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.Models
{
    public static class Constants
    {
        private const string DatabaseFilename = "database.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DatabaseFilename);

    }
}
