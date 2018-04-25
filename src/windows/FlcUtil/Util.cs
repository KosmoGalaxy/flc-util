using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Foundation;

namespace FullLegitCode.Util
{
    public sealed class Util
    {
        static BitmapTransform zeroBitmapTransform = new BitmapTransform();

        public static IAsyncAction DecodeImage([ReadOnlyArray()] Byte[] encodedBytes, IByteArrayWrapper decodedBytes)
        {
            return Task.Run(async () =>
            {
                using (MemoryStream stream = new MemoryStream(encodedBytes))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
                    PixelDataProvider provider = await decoder.GetPixelDataAsync(
                        BitmapPixelFormat.Rgba8,
                        BitmapAlphaMode.Ignore,
                        zeroBitmapTransform,
                        ExifOrientationMode.IgnoreExifOrientation,
                        ColorManagementMode.DoNotColorManage
                    );
                    decodedBytes.Bytes = provider.DetachPixelData();
                }
            })
            .AsAsyncAction();
        }

        public static IAsyncOperation<string> GetIp()
        {
            return Task.Run(async () =>
            {
                IPHostEntry host = await Dns.GetHostEntryAsync(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("ip not found");
            })
            .AsAsyncOperation();
        }
    }
}
