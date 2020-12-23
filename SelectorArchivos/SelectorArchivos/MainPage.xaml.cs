using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
namespace SelectorArchivos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //MultiplesImagenes
        private async void btnSelectedMultipleImages_Clicked(object sender, EventArgs e)
        {
            var pickResult = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image(s)"
            });

            if(pickResult != null)
            {
                var imageList = new List<ImageSource>();
                foreach(var image in pickResult)
                {
                    var stream = await image.OpenReadAsync();
                    imageList.Add(ImageSource.FromStream(() => stream));

                }
                collectionImages.ItemsSource = imageList;
            }
        }

        //Una imagen
        private async void btnSelectImage_Clicked(object sender, EventArgs e)
        {
            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image"
            });

            if (pickResult != null)
            {
               // lblNameFileSelect.Text = pickResult.FileName;
                var stream = await pickResult.OpenReadAsync();
                imgSelectedImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        //Un pdf
        private async void btnSelectPdf_Clicked(object sender, EventArgs e)
        {

            var custonFile = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                {DevicePlatform.iOS, new []{ "com.adobe.pdf" } },
                  {DevicePlatform.Android, new []{ "application/pdf" } },
                    {DevicePlatform.UWP, new []{ ".pdf" } },
                      {DevicePlatform.Tizen, new []{ "*/*" } },
                        {DevicePlatform.macOS, new []{ "pdf" } }
            });

            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
               // FileTypes = FilePickerFileType.Images,
               FileTypes = custonFile,
                PickerTitle = "Pick an image"
            });

            if (pickResult != null)
            {
                lblPdfName.Text = pickResult.FileName;
                var stream = await pickResult.OpenReadAsync();
                imgSelectedImage.Source = ImageSource.FromStream(() => stream);
            }
        }
    }
}
