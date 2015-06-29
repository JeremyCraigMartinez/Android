using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;

namespace iReach.Core.Database
{
	public class iReachDatabase : SQLiteConnection
	{

		static object locker = new object(	);

		public iReachDatabase (string path) : base(path)
		{
			CreateTable<Food> ();
			
		}

		public IEnumerable<T> GetItems<T> () where T : iReach.Core.IFoodEntity, new()
		{
			// Lock on object to prevent simultanous access from multiple threads
			lock (locker) {

				return (from i in Table<T> ()
				        select i).ToList ();
			}
		}

		public T GetItem<T> (int id) where T : iReach.Core.IFoodEntity, new ()
		{
			lock (locker) {
				return Table<T>().FirstOrDefault(x => x.ID == id);
			}

		}

		public int SaveItem<T> (T item) where T : iReach.Core.IFoodEntity, new() 
		{
			lock (locker) {
				if (item.ID != 0) {
					Update (item);
					return item.ID;
				} else {
					return Insert (item);
				}
			}
		}

		public int DeleteItem<T>(int id) where T : iReach.Core.IFoodEntity, new()	
		{
			return Delete<T> (new T () { ID = id });
		}

	}
}

