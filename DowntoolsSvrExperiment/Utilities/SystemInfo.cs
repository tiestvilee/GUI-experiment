using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DowntoolsSvrExperiment.Utilities
{
    internal sealed class SystemInfo
    {
        private readonly SystemInfoNative m_SysInfoNative;

        private SystemInfo(SystemInfoNative sysInfoNative)
        {
            m_SysInfoNative = sysInfoNative;
        }

        public ProcessorArchitecture ProcessorArchitecture
        {
            get
            {
                return (ProcessorArchitecture)m_SysInfoNative.ProcessorArchitecture;
            }
        }

        public static SystemInfo GetNativeSystemInfo()
        {
            var native = new SystemInfoNative();

            GetNativeSystemInfo(ref native);

            return new SystemInfo(native);
        }

        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo(ref SystemInfoNative lpSystemInfo);

        [StructLayout(LayoutKind.Sequential)]
        private struct SystemInfoNative
        {
            internal ushort ProcessorArchitecture;
            internal ushort Reserved;
            internal uint PageSize;
            internal IntPtr MinimumApplicationAddress;
            internal IntPtr MaximumApplicationAddress;
            internal IntPtr ActiveProcessorMask;
            internal uint NumberOfProcessors;
            internal uint ProcessorType;
            internal uint AllocationGranularity;
            internal ushort ProcessorLevel;
            internal ushort ProcessorRevision;
        }
    }

    public enum ProcessorArchitecture
    {
        None = -1,
        X86 = 0,
        IA64 = 6,
        AMD64 = 9
    }
}
