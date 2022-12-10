using CookBookApp.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace CookBookApp.Helpers
{
    public class ImageHelper
    {
        public static async Task<byte[]> selectImageAsByteArray()
        {
            byte[] result = null;
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
                result = TypeConverter.streamToByteArrayConverter(stream);
            return result;
        }

        public static byte[] getFlagAsByteArray(string languageName)
        {
            byte[] result = null;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ImageHelper)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"CookBookApp.Assets.Flags.flag_{languageName}.png");
            if (stream != null)
                result = TypeConverter.streamToByteArrayConverter(stream);
            return result;
        }
    }
}