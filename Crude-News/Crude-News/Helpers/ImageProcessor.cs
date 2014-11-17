using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace CrudeNews.Helpers
{
    public static class ImageProcessor
    {
        private const int AVATAR_HEIGHT = 400;
        private const int AVATAR_WIDTH = 250;
        public static bool IsValidImage(HttpPostedFileBase file)
        {
            return Path.GetExtension(file.FileName).ToLower() == ".jpg"
                    || Path.GetExtension(file.FileName).ToLower() == ".png"
                    || Path.GetExtension(file.FileName).ToLower() == ".gif"
                    || Path.GetExtension(file.FileName).ToLower() == ".jpeg";
        }

        public static void SaveAsAvatar(HttpPostedFileBase file, string path)
        {
            using (var avatar = ResizeImageToAvatar(file))
            {
                avatar.Save(path);
            }

        }

        private static Image ResizeImageToAvatar(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            if (!IsValidImage(file))
            {
                throw new ArgumentException("File must be an image file");
            }

            Image resizedImage;
            using (var fullSizeImage = Image.FromStream(file.InputStream))
            {
                int currentAvatarWidth = AVATAR_WIDTH;

                fullSizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                fullSizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                if (fullSizeImage.Width <= currentAvatarWidth)
                {
                    currentAvatarWidth = fullSizeImage.Width;
                }

                int newHeight = fullSizeImage.Height * AVATAR_WIDTH / fullSizeImage.Width;
                if (newHeight > AVATAR_HEIGHT)
                {
                    currentAvatarWidth = fullSizeImage.Width * AVATAR_HEIGHT / fullSizeImage.Height;
                    newHeight = AVATAR_HEIGHT;
                }

                resizedImage = fullSizeImage.GetThumbnailImage(currentAvatarWidth, newHeight, null, IntPtr.Zero);
            }

            return resizedImage;
        }
    }
}