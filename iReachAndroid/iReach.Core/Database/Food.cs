using System;
using SQLite;

namespace iReach.Core
{
	public class Food : IFoodEntity
	{
		public Food ()
		{
			
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public int NDB_NO { get; set; }
		public string Long_Desc { get; set; }
		public string ComName { get; set; }
		public string MealType { get; set; }
	}
}

