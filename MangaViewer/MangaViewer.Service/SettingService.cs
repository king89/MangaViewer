using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaViewer.Model;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;


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
        public async static Task<bool> SaveSetting()
        {
            return await Task.Run<bool>(() =>
            {
                try
                {
                    string serialResult = MySerialize.JsonSerialize((object)_appSetting);
                    FileService.SaveFileInLocalByText(Constant.settingFolder, Constant.settingFile, serialResult);
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                }

            });
        }

        public async static Task<bool> LoadSetting()
        {
            return await Task.Run<bool>(async () =>
            {
                try
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
                        _appSetting.WebSite = WebSiteEnum.Local;
                    }
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                }

            });

        }

        public async static Task<WebSiteEnum> GetWebSite()
        {
            if (APPSetting == null)
            {
                await LoadSetting();
            }
            return APPSetting.WebSite;
        }

        public static void SetWebSite(WebSiteEnum webSite)
        {
            APPSetting.WebSite = webSite;
            SaveSetting();
        }
        public static void SetWebSite(string webSite)
        {
            APPSetting.WebSite = (WebSiteEnum)Enum.Parse(typeof(WebSiteEnum), webSite);
            SaveSetting();
        }

        public static bool AddFavouriteMenu(MangaMenuItem menu)
        {
            return APPSetting.AddFavouriteMenu(menu);


        }
        public static void RemoveFavouriteMenu(MangaMenuItem menu)
        {
            APPSetting.RemoveFavouriteMenu(menu);
        }

        public static List<MangaMenuItem> GetMyMangaMenuList()
        {
            List<MangaMenuItem> menuList = null;
            if (APPSetting.FavouriteMenu != null)
            {

                menuList = (from s in APPSetting.FavouriteMenu
                            select s.MenuItem).ToList();
            }
            return menuList;
        }

        public static bool CheckFavourtie(MangaMenuItem selectedMenu)
        {
            if (APPSetting.GetFavouriteItem(selectedMenu) != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
