using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClassLibrary1
{
    public class NativeInvokes
    {
        [DllImport("freetype", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FT_Init_FreeType(out IntPtr alibrary);

        [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoadPackagedLibrary([MarshalAs(UnmanagedType.LPWStr)] string libFileName, int reserved = 0);

        [DllImport("kernel32")]
        public static extern uint GetLastError();

        public static void Init()
        {
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

            int ret;
            var moduelName = typeof(NativeInvokes).Assembly.GetName().Name;
            ret = LoadPackagedLibrary(moduelName + @"\runtimes\" + archPrefix + @"\zlib1.dll");
            Trace.WriteLine("zlib1 loaded, module: {0}", ret.ToString());
            ret = LoadPackagedLibrary(moduelName + @"\runtimes\" + archPrefix + @"\brotlicommon.dll");
            Trace.WriteLine("brotlicommon loaded, module: {0}", ret.ToString());
            ret = LoadPackagedLibrary(moduelName + @"\runtimes\" + archPrefix + @"\brotlidec.dll");
            Trace.WriteLine("brotlidec loaded, module: {0}", ret.ToString());
            ret = LoadPackagedLibrary(moduelName + @"\runtimes\" + archPrefix + @"\freetype.dll");
            Trace.WriteLine("freetype loaded, module: {0}", ret.ToString());
        }
    }
}
