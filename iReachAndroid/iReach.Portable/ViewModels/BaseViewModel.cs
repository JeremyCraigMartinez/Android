using System;
using Cirrious.MvvmCross.ViewModels;
using iReach.Portable.Interfaces;
using System.Threading.Tasks;

namespace iReach.Portable
{
	public class BaseViewModel : MvxViewModel
	{
		internal readonly IApiServices meetupService;
		public BaseViewModel (IApiServices meetupService)
		{
			this.meetupService = meetupService;
		}

		private bool isBusy = false;
		public bool IsBusy
		{ 
			get { return isBusy; }
			set { 
				isBusy = value; 
				RaisePropertyChanged(() => IsBusy); 
				if (IsBusyChanged != null)
					IsBusyChanged (isBusy);
			}
		}

		private bool canLoadMore = false;
		public bool CanLoadMore
		{
			get { return canLoadMore; }
			set { canLoadMore = value; RaisePropertyChanged(() => CanLoadMore); }
		}

		public Action<bool> IsBusyChanged { get; set; }

		private MvxCommand loadMoreCommand;

		public IMvxCommand LoadMoreCommand
		{
			get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand(async () => ExecuteLoadMoreCommand())); }
		}

		protected virtual async Task ExecuteLoadMoreCommand()
		{
		}
	}
}

