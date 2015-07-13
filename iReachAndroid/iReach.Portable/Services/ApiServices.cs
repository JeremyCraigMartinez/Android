using System;
using iReach.Portable.Interfaces;
using System.Net.Http;
using iReach.Portable.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using iReach.Portable.Helpers;
using Newtonsoft.Json;


namespace iReach.Portable
{
	public class ApiServices : IApiServices
	{
		private IHttpClientHelper _httpClientHelper; 
		public ApiServices (IHttpClientHelper httpClient = null )
		{
			this._httpClientHelper = httpClient;			
		}


		private HttpClient SetupClient ()
		{
			if(_httpClientHelper == null)
				return new HttpClient();

			// The Message handler lets us know who invoked it
			return new HttpClient(_httpClientHelper.MessageHandler);
		}

		// Property Binding of the urls
		public static string ClientId = "id";
		public static string HeaderContent 		= @"application/json";
		public const string ServerUrl			= @"https://104.236.169.12:5025";
					
		private const string GetPatientUrl 		= @"https://104.236.169.12:5025/{0}";
		private const string GetFoodUrl 		= @"https://104.236.169.12:5025/{0}";
		private const string GetGroupUrl 		= @"https://104.236.169.12:5025/{0}";
		private const string GetDoctorUrl 		= @"https://104.236.169.12:5025/{0}";
		private const string GetDocCredsUrl 	= @"https://104.236.169.12:5025/{0}";
		private const string GetPatientCredsUrl = @"https://104.236.169.12:5025/{0}";
		private const string GetAccessToken 	= @"https://104.236.169.12:5025/{0}";
 

		private const string PostNewPatient		= @"https://104.236.169.12:5025/{0}";
		private const string PostFoodData 		= @"https://104.236.169.12:5025/{0}";
						



		public async Task<Patient> GetPatient(string email)
		{
			var client = SetupClient (); // get an HttpClientHelper object

			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			// Preferebly access Tokens for getting patient data
			var request = string.Format (string.Format(GetPatientUrl, email));
			var response = client.GetAsync (request);

			return await DeserializeObjectAsync<Patient> (response);
		}

		public async Task<Patient> GetPatient(string email, string pass)
		{
			var client = SetupClient (); // get an HttpClientHelper object

			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			// Preferebly access Tokens for getting patient data
			var request = string.Format (string.Format(GetPatientCredsUrl + ":{1}", email, pass));
			var response = client.GetAsync (request);

			return await DeserializeObjectAsync<Patient> (response);
		}


		public async void RegisterNewUser (Patient patient)
		{
			var client = SetupClient (); // get an HttpClientHelper object

			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			// Preferebly access Tokens for getting patient data
			var request = string.Format (string.Format(GetPatient, patient.Email));

			var response = client.PostAsync( SerializeObjectAsync(request));

		}
		public async Task<PatientCredsModel> PatientCreds(string email, string pass)
		{
			var client = SetupClient ();
			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			var request = string.Format (string.Format(GetPatientCredsUrl, email));
			var response = client.GetAsync (request);

			return await DeserializeObjectAsync<PatientCredsModel> (response);

		}
		public async Task<DietModel> GetDiet (int foodId)
		{
			var client = SetupClient ();
			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			var request = string.Format (String.Format(GetFoodUrl, foodId));
			var response = client.GetAsync (request);
			return await DeserializeObjectAsync<DietModel> (response);

		}

		public async Task<GroupModel> GetGroup (string email)
		{
			var client = SetupClient ();
			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			var request = string.Format (String.Format(GetGroupUrl, email));
			var response = client.GetAsync (request);

			return await DeserializeObjectAsync<GroupModel> (response);
		}

		public async Task<DoctorModel> GetDoctor (string email)
		{
			var client = SetupClient ();
			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			var request = string.Format (String.Format(GetDoctorUrl, email));
			var response = client.GetAsync (request);
			return await DeserializeObjectAsync<DoctorModel> (response);
		}
		public async Task<DoctorCredsModel> GetDoctorCreds (string email, string pass)
		{
			var client = SetupClient ();
			client.DefaultRequestHeaders
				.Add (new MediaTypeWithQualityHeaderValue (HeaderContent));
			client.Timeout = new TimeSpan (0, 0, 30);	// After 30 sec it will Time out

			var request = string.Format (String.Format(GetDoctorUrl + ":{1}", email, pass));
			var response = client.GetAsync (request);

			return await DeserializeObjectAsync<DoctorCredsModel> (response);
		}
		public  Task<T> DeserializeObjectAsync<T>(string value)
		{
			return Task.Factory.StartNew (() => JsonConvert.DeserializeObject<T> (value));
		}

		public Task<T> SerializeObjectAsync<T>(object o)
		{
			return Task<T>.Factory.StartNew (() => JsonSerializer.Create ().Serialize (new JsonTextWriter(), o));
			
		}
	}
}

