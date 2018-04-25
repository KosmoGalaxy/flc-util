using FullLegitCode.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlcUtilTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            _Test();
        }

        void _Test()
        {
            //_GetIp();
            _DecodeImage();
        }

        async void _GetIp()
        {
            string ip = await Util.GetIp();
            Debug.WriteLine("[FlcUtilTest.getIp] ip: " + ip);
        }

        async void _DecodeImage()
        {
            StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Test\test.jpg");
            using (Stream fileStream = await file.OpenStreamForReadAsync())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    byte[] encodedBytes = memoryStream.ToArray();
                    byte[] decodedBytes = new byte[1920 * 1080 * 4];
                    DateTime startTime = DateTime.Now;
                    await Util.DecodeImage(encodedBytes, decodedBytes);
                    Debug.WriteLine("[FlcUtilTest.decodeImage] time: " + (DateTime.Now - startTime).TotalMilliseconds);
                    WriteableBitmap bitmap = new WriteableBitmap(1920, 1080);
                    await bitmap.PixelBuffer.AsStream().WriteAsync(decodedBytes, 0, decodedBytes.Length);
                    image.Source = bitmap;
                }
            }
        }
    }
}
