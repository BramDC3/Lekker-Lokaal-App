using LekkerLokaalApp.Data;
using LekkerLokaalApp.UWP.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_UWP))]

namespace LekkerLokaalApp.UWP.Data
{
    public class SQLite_UWP : ISQLite
    {
        public SQLite_UWP() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var dbName = "TestDb.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}
