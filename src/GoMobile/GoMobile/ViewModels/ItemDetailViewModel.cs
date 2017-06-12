using GoMobile.Constants;
using GoMobile.Models;
using Plugin.Media;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoMobile.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Recording Item { get; set; }
		public ItemDetailViewModel(Recording item = null)
		{
			Item = item;
		}

		int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}

        private ICommand _takePictureCommand;
        public ICommand TakePictureCommand
        {
            get { return _takePictureCommand ?? new Command(async () => await TakePictureAsync()); }
        }

        private async Task TakePictureAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                MessagingCenter.Send(this, MessageConstants.NoCamera);
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Images",
                Name = $"{System.Guid.NewGuid().ToString()}.jpg"
            });

            if (file == null)
                return;

            using (var ms = new MemoryStream())
            {
                await file.GetStream().CopyToAsync(ms);
                Item.Image = ms.ToArray();
            }
        }
	}
}