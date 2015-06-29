using System;
using System.Collections.Generic;

namespace iReach.Core.Diary
{
	public static class FoodDiary
	{
		static FoodDiary ()
		{
		}

		public static Food GetFood(int id)
		{
			return iReachApi.GetFood (id);
		}
		public static IList<Food> GetFoods()
		{
			return new List<Food> (iReachApi.GetFoods());
		}

		public static int SaveFood(Food item)
		{
			return iReachApi.SaveFood (item);
		}
		public static int DeleteFood(int id)
		{
			return iReachApi.DeleteFood (id);
		}
	}
}

