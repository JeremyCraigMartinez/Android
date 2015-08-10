using IReach.BL.Contracts;
using IReach.DL.SQLite;

namespace IReach.BL
{
	/// <summary>
	/// Represents a Food item.
	/// </summary>
	public class FoodItem : IBusinessEntity
	{
		public FoodItem ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Amount { get; set; }
		// new property	
	}
}

