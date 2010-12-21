using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DowntoolsSvrExperiment.Utilities
{
    internal class Win32
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WritePrivateProfileString(string lpAppName,
           string lpKeyName, string lpString, string lpFileName);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int RegOpenKeyEx(
          UIntPtr hKey,
          string subKey,
          int ulOptions,
          uint samDesired,
          out UIntPtr hkResult);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegCloseKey(
            UIntPtr hKey);

        [DllImport("Advapi32.dll", EntryPoint = "RegQueryValueExW", CharSet = CharSet.Unicode)]
        static extern int RegQueryValueEx(UIntPtr hKey, [In] string lpValueName, UIntPtr lpReserved, out int lpType, [Out] byte[] lpData, ref int lpcbData);


        public static UIntPtr HKEY_CLASSES_ROOT = new UIntPtr(0x80000000u);
        public static UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(0x80000002u);
        public static UIntPtr HKEY_CURRENT_USER = new UIntPtr(0x80000001u);
        public static UIntPtr HKEY_USERS = new UIntPtr(0x80000003u);

        public static readonly uint KEY_QUERY_VALUE = 0x01;
        public static readonly uint KEY_SET_VALUE = 0x02;
        public static readonly uint KEY_CREATE_SUB_KEY = 0x04;
        public static readonly uint KEY_ENUMERATE_SUB_KEYS = 0x08;
        public static readonly uint KEY_NOTIFY = 0x10;
        public static readonly uint KEY_CREATE_LINK = 0x20;
        public static readonly uint KEY_WOW64_32KEY = 0x200;
        public static readonly uint KEY_WOW64_64KEY = 0x100;
        public static readonly uint KEY_WOW64_RES = 0x300;

        public const int REG_NONE = 0;
        public const int REG_SZ = 1;
        public const int REG_EXPAND_SZ = 2;
        public const int REG_BINARY = 3;
        public const int REG_DWORD = 4;
        public const int REG_DWORD_BIG_ENDIAN = 5;
        public const int REG_LINK = 6;
        public const int REG_MULTI_SZ = 7;
        public const int REG_RESOURCE_LIST = 8;
        public const int REG_FULL_RESOURCE_DESCRIPTOR = 9;
        public const int REG_RESOURCE_REQUIREMENTS_LIST = 10;
        public const int REG_QWORD = 11;

        public static object RegQueryValue(UIntPtr key, string value)
        {
            int error, type = 0, dataLength = 0xfde8;
            int returnLength = dataLength;
            byte[] data = new byte[dataLength];
            while ((error = RegQueryValueEx(key, value, UIntPtr.Zero, out type, data, ref returnLength)) == 0xea)
            {
                dataLength *= 2;
                returnLength = dataLength;
                data = new byte[dataLength];
            }
            if (error == 2)
                return null; // value doesn't exist
            if (error != 0)
                throw new Win32Exception(error);

            switch (type)
            {
                case REG_NONE:
                    return data;
                case REG_DWORD:
                    return new BinaryReader(new MemoryStream(data)).ReadInt32();
                case REG_QWORD:
                    return new BinaryReader(new MemoryStream(data)).ReadInt64();
                case REG_SZ:
                    return Encoding.Unicode.GetString(data, 0, returnLength);
                case REG_EXPAND_SZ:
                    return Environment.ExpandEnvironmentVariables(Encoding.Unicode.GetString(data, 0, returnLength));
                case REG_MULTI_SZ:
                    {
                        var strings = new List<string>();
                        string packed = Encoding.Unicode.GetString(data, 0, returnLength);
                        int start = 0;
                        while (true)
                        {
                            int end = packed.IndexOf('\0', start);
                            if (end < 0 || end == start)
                                break;
                            strings.Add(packed.Substring(start, end - start));
                            start = end + 1;
                        }
                        return strings.ToArray();
                    }
                default:
                    throw new NotSupportedException();
            }
        }

    }
}
