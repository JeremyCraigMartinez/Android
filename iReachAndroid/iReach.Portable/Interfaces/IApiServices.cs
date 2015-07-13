using System;
using System.Threading.Tasks;
using iReach.Portable.Models;

namespace iReach.Portable.Interfaces
{
	public interface IApiServices
	{
		Task<Patient> GetPatient(string email);
		Task<PatientCredsModel> PatientCreds(string email, string pass);

		Task<DietModel> GetDiet (int foodId);
		Task<GroupModel> GetGroup(string email);

		Task<DoctorModel> GetDoctor (string code);
		Task<DoctorCredsModel> GetDoctorCreds (string email, string pass);
	}
}

