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


        public static bool AddFavouriteMenu(MangaMenuItem menu)
        {
            try
            {
                //init size
                menu.SetSize(MangaViewer.Foundation.Controls.HubItemSizes.SecondarySmallItem);
                FavouriteMangaItem fMenu = new FavouriteMangaItem(menu, APPSetting.WebSite);
                APPSetting.FavouriteMenu.Add(fMenu);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }


        }
        public static void RemoveFavouriteMenu(MangaMenuItem menu)
        {
            FavouriteMangaItem fMenu = APPSetting.GetFavouriteItem(menu);
            APPSetting.FavouriteMenu.Remove(fMenu);
        }

        public static List<MangaMenuItem> GetMyMangaMenuList()
        {
            List<MangaMenuItem> menuList = null;
            if (APPSetting.FavouriteMenu != null)
            {

                menuList = (from s in APPSetting.FavouriteMenu
                            select s.menuItem).ToList();
            }
            return menuList;
        }
    }
}
