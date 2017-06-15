using System;

namespace GoMobile.Models
{
    public class Recording : BaseDataObject
	{
		string _description = string.Empty;
		public string Description
        {
			get => _description; 
			set => SetProperty(ref _description, value); 
		}

		byte[] _image;
		public byte[] Image
		{
			get => _image; 
            set => SetProperty(ref _image, value); 
		}

        public DateTime _date = DateTime.Now;

        public DateTime Date
        {
            get => _date; 
        }
	}
}
