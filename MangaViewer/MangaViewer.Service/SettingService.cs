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
    public  class SettingService
    {

        public SettingService()
        {
        }
        public bool IsLoaded = false;
        Setting _appSetting = null;
        public  Setting APPSetting
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
        public async  Task<bool> SaveSetting()
        {
            return await Task.Run<bool>(() =>
            {
                try
                {
                    string serialResult = MySerialize.JsonSerialize((object)_appSetting);
                    FileService.SaveFileInLocalByText(Constant.SETTINGFOLDER, Constant.SETTINGFILE, serialResult);
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                }

            });
        }

        public async  Task<bool> LoadSetting()
        {
            return await Task.Run<bool>(async () =>
            {
                try
                {
                    string result = await FileService.LoadFileInLocalByText(Constant.SETTINGFOLDER, Constant.SETTINGFILE);

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
                    IsLoaded = true;
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                }

            });

        }

        public  WebSiteEnum GetWebSite()
        {
            while (!IsLoaded)
            {

            }
            return APPSetting.WebSite;
        }

        public  void SetWebSite(WebSiteEnum webSite)
        {
            APPSetting.WebSite = webSite;
            SaveSetting();
        }
        public  void SetWebSite(string webSite)
        {
            APPSetting.WebSite = (WebSiteEnum)Enum.Parse(typeof(WebSiteEnum), webSite);
            SaveSetting();
        }

        public  bool AddFavouriteMenu(MangaMenuItem menu)
        {
            return APPSetting.AddFavouriteMenu(menu);


        }
        public  void RemoveFavouriteMenu(MangaMenuItem menu)
        {
            APPSetting.RemoveFavouriteMenu(menu);
        }

        public  List<MangaMenuItem> GetMyMangaMenuList()
        {
            List<MangaMenuItem> menuList = null;
            if (APPSetting.FavouriteMenu != null)
            {

                menuList = (from s in APPSetting.FavouriteMenu
                            select s.MenuItem).ToList();
            }
            return menuList;
        }

        public  bool CheckFavourtie(MangaMenuItem selectedMenu)
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
