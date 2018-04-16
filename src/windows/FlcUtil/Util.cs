using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace FullLegitCode.Util
{
    public sealed class Util
    {
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
