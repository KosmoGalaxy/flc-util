using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Foundation;

namespace FullLegitCode.Util
{
    public sealed class Util
    {
        public static IAsyncOperation<IList<byte>> DecodeImage(IList<byte> byteList)
        {
            async Task<IList<byte>> Action()
            {
                byte[] bytes = new byte[byteList.Count];
                byteList.CopyTo(bytes, 0);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
                    PixelDataProvider provider = await decoder.GetPixelDataAsync();
                    List<byte> list = new List<byte>();
                    list.AddRange(provider.DetachPixelData());
                    return list;
                }
            }
            return Action().AsAsyncOperation();
        }

        public static IAsyncOperation<string> GetIp()
        {
            async Task<string> Action()
            {
                IPHostEntry host = await Dns.GetHostEntryAsync(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return null;
            }
            return Action().AsAsyncOperation();
        }
    }
}
