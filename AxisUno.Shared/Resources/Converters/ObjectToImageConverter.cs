using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace AxisUno.Resources.Converters
{
    internal sealed class ObjectToImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ImageSource imageSource)
            {
                return imageSource;
            }

            if (value is byte[] bytes)
            {
                return ImageFromBytes(bytes).GetAwaiter().GetResult();
            }

            if (value is string path)
            {
                return path;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private static async Task<BitmapImage> ImageFromBytes(byte[] bytes)
        {
            BitmapImage image = new();

            using var stream = new InMemoryRandomAccessStream();

            await stream.WriteAsync(bytes.AsBuffer());

            stream.Seek(0);

            await image.SetSourceAsync(stream);

            return image;
        }
    }
}
