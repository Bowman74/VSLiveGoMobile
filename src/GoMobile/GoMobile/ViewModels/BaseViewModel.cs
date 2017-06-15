using GoMobile.Helpers;
using GoMobile.Models;
using GoMobile.Services;

using Xamarin.Forms;

namespace GoMobile.ViewModels
{
	public class BaseViewModel : ObservableObject
	{
		/// <summary>
		/// Get the azure service instance
		/// </summary>
		public IDataStore<Recording> DataStore => DependencyService.Get<IDataStore<Recording>>();

		bool isBusy = false;
		public bool IsBusy
		{
			get => isBusy; 
			set => SetProperty(ref isBusy, value); 
		}
		/// <summary>
		/// Private backing field to hold the title
		/// </summary>
		string title = string.Empty;
		/// <summary>
		/// Public property to set and get the title of the item
		/// </summary>
		public string Title
		{
			get => title; 
			set => SetProperty(ref title, value); 
		}
	}
}

