using System;
using iReach.Portable.Interfaces;
using Cirrious.MvvmCross.ViewModels;

namespace iReach.Portable
{
	public class LoginInViewModel : BaseViewModel
	{
		private IAuthenticate _login;

		public LoginInViewModel (IApiServices webApiService, IAuthenticate login) : base(webApiService)
		{
				
			this._login = login;
			ExecuteLoginCommand ();
		}

		private IMvxCommand loginCommand;
		private IMvxCommand ExecuteLoginCommand
		{
			get { return loginCommand ?? (loginCommand = new MvxCommand (ExecuteLoginCommand)); } 		
		}
	}
}

