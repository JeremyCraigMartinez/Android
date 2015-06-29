using System;
using SQLite;

namespace iReach.Core
{
	public abstract class FoodEntityBase : IFoodEntity
	{
		public FoodEntityBase ()
		{
		}


		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}

