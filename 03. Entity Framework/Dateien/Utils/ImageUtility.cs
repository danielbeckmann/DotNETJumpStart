using System;
using System.IO;
using System.Web;
using System.Web.Helpers;
using DotNETJumpStart.Models;

namespace DotNETJumpStart.Utils
{
    /// <summary>
    /// Provides functions for image handling.
    /// </summary>
    public static class ImageUtility
    {
        /// <summary>
        /// Defines the image upload file path.
        /// </summary>
        private static string UploadPath = "~/Uploads";

        /// <summary>
        /// Gets an image from the http request and saves it to disk.
        /// </summary>
        /// <returns>Image object</returns>
        public static Image SaveImageFromRequest()
        {
            var webImage = WebImage.GetImageFromRequest();
            if (webImage != null)
            {
                var image = new Image();
                image.FileName = ResizeImageAndSaveToDisk(webImage);
                return image;
            }

            throw new Exception("Image cannot be null");
        }

        /// <summary>
        /// Gets an image from the http request and updates it on disk.
        /// </summary>
        /// <param name="image">The existing image object</param>
        /// <returns>The updated image object</returns>
        public static Image GetImageAndUpdateOnDisk(Image image)
        {
            var photo = WebImage.GetImageFromRequest();
            if (photo != null)
            {
                // Delete old image from disk
                if (image != null)
                {
                    DeleteImageFromDisk(image);
                }
                else
                {
                    image = new Image();
                }

                image.FileName = ResizeImageAndSaveToDisk(photo);
            }

            return image;
        }

        /// <summary>
        /// Resizes an image and saves it to disk.
        /// </summary>
        /// <param name="webImage">Web image object</param>
        /// <param name="image">The image object</param>
        private static string ResizeImageAndSaveToDisk(WebImage webImage)
        {
            // TODO: add watermark
            // webImage = webImage.AddTextWatermark();

            // Resize image with web image helper
            webImage = webImage.Resize(1024, 768, true, true);


            // Create random name
            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(webImage.FileName);

            // Set filename and save to disk
            string fileName = string.Format("{0}{1}", guid, extension);
            var path = GetLocalFilePath(fileName);
            webImage.Save(path);

            return fileName;
        }

        /// <summary>
        /// Deletes a given image from the disk.
        /// </summary>
        /// <param name="image">The image object</param>
        public static void DeleteImageFromDisk(Image image)
        {
            if (image != null && !string.IsNullOrEmpty(image.FileName))
            {
                var path = GetLocalFilePath(image.FileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        private static string GetLocalFilePath(string fileName)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(UploadPath), fileName);
        }

        public static string GetFileUri(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName");

            var httpContext = HttpContext.Current;
            var  appPath = string.Format("{0}://{1}{2}{3}",
                                             httpContext.Request.Url.Scheme,
                                             httpContext.Request.Url.Host,
                                             httpContext.Request.Url.Port == 80
                                                 ? string.Empty
                                                 : ":" + httpContext.Request.Url.Port,
                                             httpContext.Request.ApplicationPath);

            var s = string.Format("{0}Uploads/{1}", appPath, fileName);

            return s;
        }
    }
}