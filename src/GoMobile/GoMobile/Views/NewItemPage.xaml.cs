using System;

using GoMobile.Models;

using Xamarin.Forms;
using GoMobile.ViewModels;

namespace GoMobile.Views
{
	public partial class NewItemPage : ContentPage
	{
		public NewItemPage()
		{
			InitializeComponent();

			var item = new Recording
			{
				Description = "This is a nice description"
			};

            ViewModel = new ItemDetailViewModel(item);

            BindingContext = this;
		}

        private ItemDetailViewModel _viewModel;
        public ItemDetailViewModel ViewModel
        {
            get => _viewModel;
            set => _viewModel = value;
        }

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddItem", ViewModel.Item);
			await Navigation.PopToRootAsync();
		}

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<ItemDetailViewModel>(this, "NoCamera", (obj) =>
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
            });
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ItemDetailViewModel>(this, "NoCamera");
            base.OnDisappearing();
        }
    }
}