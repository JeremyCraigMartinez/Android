using System;
using System.Collections.Generic;
using System.IO;
using IReach.BL;

namespace IReach.DAL {
	public class FoodItemRepository {
		DL.FoodItemDatabase db = null;
		protected static string dbLocation;		
		protected static FoodItemRepository me;		
		
		static FoodItemRepository ()
		{
			me = new FoodItemRepository();
		}
		
		protected FoodItemRepository()
		{
			// set the db location
			dbLocation = DatabaseFilePath;
			
			// instantiate the database	
			db = new IReach.DL.FoodItemDatabase(dbLocation);
		}

	 
		
		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "TaskDB.db3";
#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
	            var path = sqliteFilename;
#else 

#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
	            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
#endif
				 
				var path = Path.Combine (libraryPath, sqliteFilename);
#endif		
				return path;	
			}
		}

		public static FoodItem GetFood(int id)
		{
			return me.db.GetItem<FoodItem>(id);
		}
		
		public static IEnumerable<FoodItem> GetFoods ()
		{
			return me.db.GetItems<FoodItem>();
		}
		
		public static int SaveFood (FoodItem item)
		{
			return me.db.SaveItem<FoodItem>(item);
		}

		public static int DeleteFood(int id)
		{
			return me.db.DeleteItem<FoodItem>(id);
		}
	}
}

