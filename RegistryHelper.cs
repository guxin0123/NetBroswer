using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBroswer
{
    internal class RegistryHelper
    {
        #region 内部使用
        private static Dictionary<string, RegistryHive> roots = new Dictionary<string, RegistryHive>()
        {
            {"HKEY_CLASSES_ROOT",RegistryHive.ClassesRoot},
            {"HKEY_CURRENT_USER",RegistryHive.CurrentUser},
            {"HKEY_LOCAL_MACHINE",RegistryHive.LocalMachine},
            {"HKEY_USERS",RegistryHive.Users},
            {"HKEY_CURRENT_CONFIG",RegistryHive.CurrentConfig}
        };
        private RegistryKey GetRoot(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return null;
            path = path.Trim('\\', '/');
            foreach (var i in roots)
            {
                if (path.StartsWith(i.Key))
                {
                    return RegistryKey.OpenBaseKey(i.Value,
                                           Environment.Is64BitOperatingSystem
                                               ? RegistryView.Registry64
                                               : RegistryView.Registry32);
                }
            }
            return null;
        }

        private static ValueTuple<string, RegistryKey> PrunePath(string path)
        {
            path = path.Trim('\\', '/').Replace('/', '\\');
            RegistryKey baseKey = null;
            foreach (var i in roots)
            {
                if (path.StartsWith(i.Key))
                {
                    baseKey = RegistryKey.OpenBaseKey(i.Value,
                                           Environment.Is64BitOperatingSystem
                                               ? RegistryView.Registry64
                                               : RegistryView.Registry32);
                    path = path.Substring(i.Key.Length).Trim('\\');
                    break;
                }
            }
            if (baseKey == null)
            {
                baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                                           Environment.Is64BitOperatingSystem
                                               ? RegistryView.Registry64
                                               : RegistryView.Registry32);
            }
            return new ValueTuple<string, RegistryKey>(path, baseKey);
        }
        #endregion

        #region 判断、列出数据值的关键字、删除项或数据值
        /// <summary>
        /// 判断注册表项或值是否存在
        /// </summary>
        /// <param name="path">项路径</param>
        /// <param name="keyname">值的名称</param>
        /// <returns></returns>
        public static bool Exists(string path, string keyname = null)
        {
            var res = PrunePath(path);
            var root = res.Item2;
            path = res.Item1;
            RegistryKey key = root.OpenSubKey(path);
            if (key == null) return false;
            if (keyname == null) return true;
            return key.GetValueNames().ToList().Contains(keyname);
        }

        /// <summary>
        /// 返回注册项下的所有数据值名称,如果不存在这个项就返回null
        /// </summary>
        /// <param name="path">注册项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <returns></returns>
        public static List<string> ListDataKeys(string path)
        {
            var res = PrunePath(path);
            var root = res.Item2;
            path = res.Item1;
            RegistryKey key = root.OpenSubKey(path);
            if (key == null) return null;
            return key.GetValueNames().ToList();
        }

        /// <summary>
        /// 删除指定的项(包括子项和数据值)
        /// </summary>
        /// <param name="path">注册项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        public static void DeletePath(string path)
        {
            if (Exists(path))
            {

                ValueTuple<string, RegistryKey> res = PrunePath(path);
                path = res.Item1;
                RegistryKey root = res.Item2;
                root.DeleteSubKeyTree(path);
            }
        }

        /// <summary>
        /// 删除指定项下的指定数据值
        /// </summary>
        /// <param name="path">注册项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">注册项下的数据值关键字,如:teststring</param>
        public static void DeleteValue(string path, string keyname)
        {
            if (Exists(path, keyname))
            {

                ValueTuple<string, RegistryKey> res = PrunePath(path);
                path = res.Item1;
                RegistryKey root = res.Item2;
                RegistryKey key = root.OpenSubKey(path, true);
                key.DeleteValue(keyname);
            }
        }
        #endregion

        #region 读取注册表的值
        private static object GetValue(string path, string keyname = "")
        {
            var res = PrunePath(path);
            var root = res.Item2;
            path = res.Item1;
            RegistryKey key = root.OpenSubKey(path);
            if (key == null) return null;
            var obj = key.GetValue(keyname);
            return obj;
        }

        /// <summary>
        /// 读取注册项下的字符串数据(REG_SZ和REG_EXPAND_SZ),如果keyname为空就是读取默认值
        /// </summary>
        /// <param name="path">项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据值名称,如:string_a</param>
        /// <returns></returns>
        public static string GetString(string path, string keyname = "")
        {
            var obj = GetValue(path, keyname);
            if (obj == null) return null;
            return obj as string;
        }

        /// <summary>
        /// 读取注册项下的字节数据(REG_BINARY)
        /// </summary>
        /// <param name="path">项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据值名称,如:binary_123456789</param>
        /// <returns></returns>
        public static byte[] GetByteArray(string path, string keyname)
        {
            var obj = GetValue(path, keyname);
            if (obj == null) return null;
            return obj as byte[];
        }

        /// <summary>
        /// 读取注册项下的字节数据(REG_DWORD)
        /// </summary>
        /// <param name="path">项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据值名称,如:dword</param>
        /// <returns></returns>
        public static Int32? GetInt32(string path, string keyname)
        {
            var obj = GetValue(path, keyname);
            if (obj == null) return null;
            return (Int32)obj;
        }

        /// <summary>
        /// 读取注册项下的字节数据(REG_QWORD)
        /// </summary>
        /// <param name="path">项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据值名称,如:qword</param>
        /// <returns></returns>
        public static Int64? GetInt64(string path, string keyname)
        {
            var obj = GetValue(path, keyname);
            if (obj == null) return null;
            return (Int64)obj;
        }

        /// <summary>
        /// 综合GetInt32和GetInt64方法,可以取出REG_DWORD和REG_QWORD类型的数据
        /// </summary>
        /// <param name="path">项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据值名称,如:testint</param>
        /// <returns></returns>
        public static int? GetInt(string path, string keyname)
        {
            var obj = GetValue(path, keyname);
            if (obj == null) return null;
            if (obj is Int32) return (int)obj;
            if (obj is Int64) return (int)(long)obj;
            return null;
        }

        /// <summary>
        /// 读取注册项下的字节数据(REG_MULTI_SZ)
        /// </summary>
        /// <param name="path">项路径,如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据值名称,如:string_multi</param>
        /// <returns></returns>
        public static string[] GetStringArray(string path, string keyname)
        {
            var obj = GetValue(path, keyname);
            if (obj == null) return null;
            return obj as string[];
        }
        #endregion

        #region 设置注册项的数据值
        private static void SetValue(string path, string keyname, object value, RegistryValueKind dataKind)
        {
            var res = PrunePath(path);
            var root = res.Item2;
            path = res.Item1;
            RegistryKey key = root.CreateSubKey(path, true);
            key.SetValue(keyname, value, dataKind);
        }
        /// <summary>
        /// 设置注册项的默认值,注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="value">字符串,类型为REG_SZ</param>
        public static void SetDefault(string path, string value)
        {
            SetValue(path, "", value, RegistryValueKind.String);
        }

        /// <summary>
        /// 如果指定的项(如果不存在则新建)
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        public static void CreatePath(string path)
        {
            var res = PrunePath(path);
            var root = res.Item2;
            path = res.Item1;
            root.CreateSubKey(path, true);
        }

        /// <summary>
        /// 给指定注册项设置键值对(REG_SZ),注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据的名称</param>
        /// <param name="value">数据值</param>
        public static void SetString(string path, string keyname, string value = "")
        {
            if (string.IsNullOrWhiteSpace(keyname)) throw new Exception("必须指定数据值的关键字,如果想设置【(默认)】,那么调用SetDefault(string path,string value)方法");
            SetValue(path, keyname, value, RegistryValueKind.String);
        }

        /// <summary>
        /// 给指定注册项设置键值对(REG_EXPAND_SZ),注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据的名称</param>
        /// <param name="value">数据值,如:xiao%windir%ming</param>
        public static void SetStringExpand(string path, string keyname, string value = "")
        {
            if (string.IsNullOrWhiteSpace(keyname)) throw new Exception("必须指定数据值的关键字,如果想设置【(默认)】,那么调用SetDefault(string path,string value)方法");
            SetValue(path, keyname, value, RegistryValueKind.ExpandString);
        }

        /// <summary>
        /// 给指定注册项设置键值对(REG_MULTI_SZ),注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据的名称</param>
        /// <param name="value">数据值,如:new string[2] { "xiaoming", "xi" }</param>
        public static void SetStringArray(string path, string keyname, string[] value)
        {
            if (string.IsNullOrWhiteSpace(keyname)) throw new Exception("必须指定数据值的关键字,如果想设置【(默认)】,那么调用SetDefault(string path,string value)方法");
            if (value == null) throw new Exception("必须指定一个数组!");
            SetValue(path, keyname, value, RegistryValueKind.MultiString);
        }

        /// <summary>
        /// 给指定注册项设置键值对(REG_DWORD),注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据的名称</param>
        /// <param name="value">数据值,如:456</param>
        public static void SetInt32(string path, string keyname, Int32 value)
        {
            if (string.IsNullOrWhiteSpace(keyname)) throw new Exception("必须指定数据值的关键字,如果想设置【(默认)】,那么调用SetDefault(string path,string value)方法");
            SetValue(path, keyname, value, RegistryValueKind.DWord);
        }

        /// <summary>
        /// 给指定注册项设置键值对(REG_QWORD),注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据的名称</param>
        /// <param name="value">数据值,如:789</param>
        public static void SetInt64(string path, string keyname, Int64 value)
        {
            if (string.IsNullOrWhiteSpace(keyname)) throw new Exception("必须指定数据值的关键字,如果想设置【(默认)】,那么调用SetDefault(string path,string value)方法");
            SetValue(path, keyname, value, RegistryValueKind.QWord);
        }

        /// <summary>
        /// 综合SetInt32和SetInt64,存储的是RED_DWORD类型,注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据的名称</param>
        /// <param name="value">数据值,如:789</param>
        public static void SetInt(string path, string keyname, int value)
        {
            if (string.IsNullOrWhiteSpace(keyname)) throw new Exception("必须指定数据值的关键字,如果想设置【(默认)】,那么调用SetDefault(string path,string value)方法");
            SetValue(path, keyname, value, RegistryValueKind.DWord);
        }

        /// <summary>
        /// 给指定注册项设置键值对(REG_BINARY),注意：如果指定的path不存在则会新建
        /// </summary>
        /// <param name="path">注册项路径,比如:HKEY_CURRENT_CONFIG\test\test1</param>
        /// <param name="keyname">数据的名称</param>
        /// <param name="value">数据值,如:new byte[2] { 0x20, 0x45 }</param>
        public static void SetByteArray(string path, string keyname, byte[] value)
        {
            if (string.IsNullOrWhiteSpace(keyname)) throw new Exception("必须指定数据值的关键字,如果想设置【(默认)】,那么调用SetDefault(string path,string value)方法");
            SetValue(path, keyname, value, RegistryValueKind.Binary);
        }
        #endregion
    }
}
