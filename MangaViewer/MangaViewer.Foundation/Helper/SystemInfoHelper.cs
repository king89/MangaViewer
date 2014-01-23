using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration.Pnp;
using Windows.Foundation;
using Windows.Networking.Connectivity;

namespace MangaViewer.Foundation.Helper
{
    public class SystemInfoHelper
    {
        /// <summary>
        /// 获取唯一id（网卡id） 替代imei
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueId()
        {
            try
            {
                var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
                var networkAdapter = connectionProfile.NetworkAdapter;
                return networkAdapter.NetworkAdapterId.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 格林威治时间
        /// </summary>
        /// <returns></returns>
        public static string GetTime()
        {
            return Decimal.ToInt64(Decimal.Divide(DateTime.UtcNow.Ticks - 621355968000000000, 10000)).ToString();
        }
        /// <summary>
        /// 应用程序包名或程序名称
        /// </summary>
        /// <returns></returns>
        public static string GetPKG()
        {
            try
            {
                Package package = Package.Current;
                PackageId packageId = package.Id;
                return packageId.Name;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获得网络类型
        /// </summary>
        /// <returns></returns>
        public static string GetNettype()
        {
            try
            {
                //http://msdn.microsoft.com/en-us/library/windows/apps/windows.networking.connectivity.networkadapter.ianainterfacetype.aspx
                var profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
                var interfaceType = profile.NetworkAdapter.IanaInterfaceType;

                if (interfaceType == 71)
                {
                    return "WIFI";
                }
                else if (interfaceType == 6)
                {
                    return "Ethernet";
                }
                else
                {
                    return "Other";
                }
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// APP的版本号
        /// </summary>
        /// <param name="localAssemblyType">APP随便哪个类的类型</param>
        /// <returns></returns>
        public static string GetAppVersion(Type localAssemblyType)
        {
            try
            {
                AssemblyFileVersionAttribute MyAssemblyFileVersionAttribute = localAssemblyType.GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
                return MyAssemblyFileVersionAttribute.Version;
            }
            catch
            {
                return "";
            }
        }

        private static string DeviceModel;
        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetModel()
        {
            try
            {
                if (DeviceModel != null)
                {
                    return DeviceModel;
                }
                string[] properties = { "System.Devices.ModelName" };
                var containers = await PnpObject.FindAllAsync(PnpObjectType.DeviceContainer, properties);
                foreach (PnpObject container in containers)
                {
                    if (container.Id.Equals("{00000000-0000-0000-FFFF-FFFFFFFFFFFF}"))
                    {
                        DeviceModel = container.Properties["System.Devices.ModelName"].ToString();
                        return DeviceModel;
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 终端屏幕分辨率
        /// </summary>
        /// <returns></returns>
        internal static string GetResolution()
        {
            try
            {
                Rect ret = Windows.UI.Xaml.Window.Current.CoreWindow.Bounds;
                return ret.Width + "*" + ret.Height;
            }
            catch
            {
                return "";
            }
        }


    }
}
