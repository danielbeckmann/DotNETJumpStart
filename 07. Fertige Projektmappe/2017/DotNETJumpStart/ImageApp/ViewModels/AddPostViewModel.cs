using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;
using ImageApp.DataModel;
using ImageApp.Utils;

namespace ImageApp.ViewModels
{
    /// <summary>
    /// The viewmodel for the addpost page.
    /// </summary>
    public class AddPostViewModel : BindableBase
    {
        private string title;

        private BitmapImage previewImage;

        private StorageFile imageFile;

        public AddPostViewModel()
        {
            this.PickFileCommand = new DelegateCommand(this.PickImage);
            this.AddPostCommand = new DelegateCommand(this.AddImageAndPost, this.CanAddImageAndPost);
        }

        /// <summary>
        /// Gets or sets the new images title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set
            {
                this.SetProperty(ref this.title, value);
                this.AddPostCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Sets the new image file.
        /// </summary>
        public BitmapImage PreviewImage
        {
            get { return this.previewImage; }
            set
            {
                this.SetProperty(ref this.previewImage, value);
                this.AddPostCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the command to pick a file.
        /// </summary>
        public DelegateCommand PickFileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to add a post.
        /// </summary>
        public DelegateCommand AddPostCommand { get; set; }

        /// <summary>
        /// Determines whether a post can be added.
        /// </summary>
        private bool CanAddImageAndPost(object obj)
        {
            return !string.IsNullOrWhiteSpace(this.title) && this.imageFile != null;
        }

        /// <summary>
        /// Uses the filepicker to open an image.
        /// </summary>
        private async void PickImage(object obj)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                var image = new BitmapImage();
                image.SetSource(stream);

                // Set file reference
                this.imageFile = file;

                // Set preview image
                this.PreviewImage = image;
            }
        }

        /// <summary>
        /// Loads the byte data from a StorageFile
        /// </summary>
        /// <param name="file">The file to read</param>
        private async Task<byte[]> ReadFileAsync(StorageFile file)
        {
            byte[] fileBytes = null;
            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }

            return fileBytes;

        }

        /// <summary>
        /// Adds an image and then the post to the api.
        /// </summary>
        /// <param name="obj"></param>
        private async void AddImageAndPost(object obj)
        {
            var imageId = await this.AddImageAsync();
            if (imageId != null)
            {
                if (await this.AddPostAsync(imageId.Value))
                {
                    NavigationService.Current.GoBack();
                }
            }
        }

        /// <summary>
        /// Posts an image to the api.
        /// </summary>
        /// <returns></returns>
        private async Task<int?> AddImageAsync()
        {
            using (var client = new RestClient("http://localhost:55298/api/"))
            {
                var request = new RestRequest("images", Method.POST);
                var data = await ReadFileAsync(this.imageFile);
                request.AddFile(FileParameter.Create("file", data, this.imageFile.Name));

                try
                {
                    var image = await client.Execute<Image>(request);
                    return image.Data.Id;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Adds a post to the api.
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        private async Task<bool> AddPostAsync(int imageId)
        {
            using (var client = new RestClient("http://localhost:55298/api/"))
            {
                var request = new RestRequest("posts", Method.POST);

                var addPost = new Post { ImageId = imageId, Title = this.title, UserIdentifier = DeviceUtils.DeviceId };
                request.AddJsonBody(addPost);

                try
                {
                    await client.Execute(request);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
