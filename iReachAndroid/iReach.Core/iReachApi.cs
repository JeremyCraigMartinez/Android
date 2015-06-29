using System;
using iReach.Core.Helpers;
using System.IO;
using System.Threading.Tasks;
using iReach.Core.Database;
using System.Collections.Generic;
using SQLite;

namespace iReach.Core
{
	public class iReachApi
	{

		public delegate void AnnouncementDelegate(string input);
		public delegate void ServerUpdateDelegate(object o, Response_Type r);


		protected static iReachApi api;
		protected static string dbLocation;
		protected static SQLiteConnection conn;

		iReach.Core.Database.iReachDatabase db = null;

		static iReachApi ()
		{	
			
			api = new iReachApi ();			
		}

		protected iReachApi()
		{
			dbLocation = DatabaseFilePath;

			db = new iReachDatabase (dbLocation);

		}

		public static string DatabaseFilePath {
			get { 

				var sqliteFilename = "food.db";
				string libararyPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);					
				var path = Path.Combine (libararyPath, sqliteFilename);

				return path;
			}
		}

		public static Food GetFood(int id)
		{
			return api.db.GetItem<Food> (id);
		}

		public static IEnumerable<Food> GetFoods()
		{
			return api.db.GetItems<Food> ();
		}

		public static int SaveFood (Food item)
		{
			return api.db.SaveItem<Food> (item);
		}

		public static int DeleteFood(int id)
		{
			return api.db.DeleteItem<Food> (id);
		}

	}
}

