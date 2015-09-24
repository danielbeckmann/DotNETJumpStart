using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using ImageApp.Services;
using ImageApp.DataModel;
using RestSharp.Portable;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace ImageApp.ViewModels
{
    /// <summary>
    /// The viewmodel for the addpost page.
    /// </summary>
    public class AddPostViewModel : BindableBase
    {
        private StorageFile image;
      
        private string title;

        public AddPostViewModel()
        {
            this.AddPostCommand = new DelegateCommand(this.AddImageAndPost, this.CanAddImageAndPost);
            this.PickFileCommand = new DelegateCommand(this.PickImage);
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
        public StorageFile Image
        {
            set
            {
                this.SetProperty(ref this.image, value);
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
            return !string.IsNullOrWhiteSpace(this.title) && this.image != null;
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
            using (var client = new RestClient("http://acando-workshop.azurewebsites.net/api/"))
            {
                var request = new RestRequest("images", HttpMethod.Post);
                var data = await ReadFileAsync(this.image);
                request.AddFile(FileParameter.Create("file", data, this.image.Name));

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
            using (var client = new RestClient("http://acando-workshop.azurewebsites.net/api/"))
            {
                var request = new RestRequest("posts", HttpMethod.Post);

                var addPost = new AddPostRequest { ImageId = imageId, Title = this.title, UserIdentifier = SessionService.DeviceId };
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
        /// Uses the filepicker to open an image.
        /// </summary>
        private void PickImage(object obj)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // Launch file open picker
            openPicker.PickSingleFileAndContinue();
        }
    }
}
