using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LekkerLokaalApp.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
