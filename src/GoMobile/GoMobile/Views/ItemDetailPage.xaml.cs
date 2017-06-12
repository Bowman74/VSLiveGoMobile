
using GoMobile.Constants;
using GoMobile.ViewModels;

using Xamarin.Forms;

namespace GoMobile.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<ItemDetailViewModel>(this, MessageConstants.NoCamera, (obj) =>
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
            });
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ItemDetailViewModel>(this, MessageConstants.NoCamera);
            base.OnDisappearing();
        }
    }
}
