using System;
using System.Collections.Generic;
using DowntoolsSvrExperiment.Utilities;

namespace DowntoolsSvrExperiment.Connection
{
    public class GetLocalInstances
    {
        public virtual IEnumerable<string> LocalInstances()
        {
            var returnValue = new List<string>();
            returnValue.AddRange(LocalInstances(0u));

            if (Is64BitMachine)
            {
                var auxFlag = Is64BitProcess ? Win32.KEY_WOW64_32KEY : Win32.KEY_WOW64_64KEY;
                returnValue.AddRange(LocalInstances(auxFlag));
            }

            return returnValue;
        }

        private static IEnumerable<string> LocalInstances(uint flag)
        {
            // Get local instances for us
            var mssqlKey = UIntPtr.Zero;
            try
            {
                Win32.RegOpenKeyEx(Win32.HKEY_LOCAL_MACHINE, @"Software\Microsoft\Microsoft SQL Server", 0, Win32.KEY_QUERY_VALUE | flag, out mssqlKey);

                if (mssqlKey == UIntPtr.Zero)
                {
                    return new List<string>();
                }

                var instances = Win32.RegQueryValue(mssqlKey, "InstalledInstances") as string[];
                if (instances == null)
                {
                    return new List<string>();
                }

                return new List<string>(instances);
            }
            finally
            {
                if (mssqlKey != UIntPtr.Zero)
                {
                    Win32.RegCloseKey(mssqlKey);
                }
            }
        }

        public static bool Is64BitMachine
        {
            get { return SystemInfo.GetNativeSystemInfo().ProcessorArchitecture == ProcessorArchitecture.AMD64; }
        }

        public static bool Is64BitProcess
        {
            get
            {
                return IntPtr.Size == 8;
            }
        }

    }
}
