using System;

using GoMobile.Models;
using GoMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if __ANDROID__
using Android.Support.Design.Widget;
using Xamarin.Forms.Platform.Android;
#endif

namespace GoMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;

		public ItemsPage()
		{
            InitializeComponent();

			BindingContext = viewModel = new ItemsViewModel();

#if __ANDROID__

            var actionButton = new FloatingActionButton(Forms.Context);
            actionButton.SetImageResource(GoMobile.Droid.Resource.Drawable.ic_add_white_24dp);
            actionButton.Click += fab_click;

            var actionButtonView = (NativeViewWrapper)actionButton.ToView();

            var content = new ContentView();
            content.Content = actionButtonView;
            content.HorizontalOptions = LayoutOptions.Center;
            content.VerticalOptions = LayoutOptions.EndAndExpand;

            var xConstraint = Constraint.RelativeToParent((parent) =>
            {
                return parent.Width - 100;
            });
            var yConstraint = Constraint.RelativeToParent((parent) =>
            {
                return parent.Height - 100;
            });

            mainLayout.Children.Add(actionButtonView, xConstraint, yConstraint);
            ToolbarItems.Remove(ToolbarItems[0]);
#endif
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Recording;
			if (item == null)
				return;

			await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

			// Manually deselect item
			ItemsListView.SelectedItem = null;
		}

		async void AddItem_Clicked()
		{
			await Navigation.PushAsync(new NewItemPage());
		}

        private void fab_click(object sender, EventArgs e)
        {
            AddItem_Clicked();
        }

        protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Items.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}
	}
}
