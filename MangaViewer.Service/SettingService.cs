using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaViewer.Model;


namespace MangaViewer.Service
{
    public static class SettingService
    {

        static Setting _appSetting = null;
        public static Setting APPSetting
        {
            get
            {
                if (_appSetting == null)
                {
                    LoadSetting();
                }
                return _appSetting;
            }
        }
        public static void SaveSetting()
        {
            string serialResult = MySerialize.JsonSerialize((object)_appSetting);
            FileService.SaveFileInLocalByText(Constant.settingFolder, Constant.settingFile, serialResult);
        }

        public async static void LoadSetting()
        {
            string result = await FileService.LoadFileInLocalByText(Constant.settingFolder, Constant.settingFile);

            //throw new NotImplementedException();
            if (result != string.Empty)
            {
                _appSetting = MySerialize.JsonDeserialize<Setting>(result);
            }
            else
            {
                _appSetting = new Setting();
                _appSetting.WebSite = WebSiteEnum.Comic131;
            }

        }

        public static WebSiteEnum GetWebSite()
        {
            return APPSetting.WebSite;
        }

        public static void SetWebSite(WebSiteEnum webSite)
        {
            APPSetting.WebSite = webSite;
            SaveSetting();
        }
    }
}
