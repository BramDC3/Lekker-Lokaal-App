using LekkerLokaalApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LekkerLokaalApp.Data
{
    public class HandelaarDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public HandelaarDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Handelaar>();
        }

        public Handelaar GetHandelaar()
        {
            lock (locker)
            {
                if (database.Table<Handelaar>().Count() == 0)
                    return null;
                else
                    return database.Table<Handelaar>().First();
            }
        }

        public int SaveHandelaar(Handelaar handelaar)
        {
            lock (locker)
            {
                if (handelaar.Id != 0)
                {
                    database.Update(handelaar);
                    return handelaar.Id;
                }
                else
                {
                    return database.Insert(handelaar);
                }
            }
        }

        public int DeleteHandelaar(int id)
        {
            lock (locker)
            {
                return database.Delete<Handelaar>(id);
            }
        }
    }
}
