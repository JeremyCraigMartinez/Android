using System;
using iReach.Portable.Interfaces;
using System.Threading.Tasks;

namespace iReach.Portable.Interfaces.Database
{
	interface IFoodDiary
	{
		Task<DietModel> LookupFood (string FoodName);
		Task<DietModel> AddFood(string Foodname, int FoodId);
	}

}

