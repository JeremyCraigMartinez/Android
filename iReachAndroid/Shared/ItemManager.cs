using System;
using System.Collections.Generic;
using IReach.BL;

namespace IReach.BL.Managers
{
	public static class ItemManager
	{
		static ItemManager ()
		{
		}
		
		public static FoodItem GetFood(int id)
		{
			return DAL.FoodItemRepository.GetFood(id);
		}
		
		public static IList<FoodItem> GetFoods ()
		{
			return new List<FoodItem>(DAL.FoodItemRepository.GetFoods());
		}
		
		public static int SaveFood (FoodItem item)
		{
			return DAL.FoodItemRepository.SaveFood(item);
		}
		
		public static int DeleteFood(int id)
		{
			return DAL.FoodItemRepository.DeleteFood(id);
		}
		
	}
}