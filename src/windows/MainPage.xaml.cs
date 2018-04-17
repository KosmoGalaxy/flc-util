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
            _GetIp();
            _DecodeImage();
        }

        async void _GetIp()
        {
            string ip = await Util.GetIp();
            Debug.WriteLine("[FlcUtilTest] ip: " + ip);
        }

        async void _DecodeImage()
        {
            StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Test\test.jpg");
            using (Stream fileStream = await file.OpenStreamForReadAsync())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    List<byte> list = new List<byte>();
                    list.AddRange(memoryStream.ToArray());
                    list = (List<byte>)await Util.DecodeImage(list);
                    byte[] bytes = list.ToArray();
                    WriteableBitmap bitmap = new WriteableBitmap(1920, 1080);
                    await bitmap.PixelBuffer.AsStream().WriteAsync(bytes, 0, bytes.Length);
                    image.Source = bitmap;
                }
            }
        }
    }
}
