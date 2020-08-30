using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.InteropServices;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace App1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        [DllImport("freetype", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FT_Init_FreeType(out IntPtr alibrary);

        [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoadPackagedLibrary([MarshalAs(UnmanagedType.LPWStr)] string libFileName, int reserved = 0);

        [DllImport("kernel32")]
        public static extern uint GetLastError();

        public MainPage()
        {
            this.InitializeComponent();

            string archPrefix = "uwp-x86";

            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    archPrefix = "uwp-x86";
                    break;
                case Architecture.X64:
                    archPrefix = "uwp-x64";
                    break;
                case Architecture.Arm:
                case Architecture.Arm64:
                    archPrefix = "uwp-arm";
                    break;
            }

            LoadPackagedLibrary(@"runtimes\" + archPrefix + @"\zlib1.dll");
            LoadPackagedLibrary(@"runtimes\" + archPrefix + @"\brotlicommon.dll");
            LoadPackagedLibrary(@"runtimes\" + archPrefix + @"\brotlidec.dll");
            LoadPackagedLibrary(@"runtimes\" + archPrefix + @"\freetype.dll");

            var ret = FT_Init_FreeType(out var lib);
            System.Diagnostics.Debug.WriteLine("arch: {2} - ret: {0}, lib: {1}", ret, lib, RuntimeInformation.ProcessArchitecture);
        }
    }
}
