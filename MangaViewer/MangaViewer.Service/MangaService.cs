
using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace MangaViewer.Service
{
    public  class MangaService
    {
        public MangaService(SettingService setting)
        {
            SettingService = setting;
            //WebType = setting;
        }
        #region  Property
        public SettingService SettingService { get; set; }
        private  int groupItemMaxNum = 15;

        WebSiteEnum WebType
        {
            get { return SettingService.GetWebSite(); }
        }

        private  string _menuHtml = "";
        public  string MenuHtml
        {
            get
            {
                if (_menuHtml == string.Empty)
                {
                    MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                    _menuHtml = mPattern.GetHtml(mPattern.WEBSITEURL);

                }
                return _menuHtml;
            }
        }
        #endregion

        #region Function

        #region Page
        /// <summary>
        ///   get web image and save into temp folder, return local path uri
        /// </summary>
        public  Task<string> GetIamgeByImageUrl(MangaPageItem page)
        {
            return Task.Run<string>(() =>
            {
                //Get Image
                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                mPattern.GetImageByImageUrl(page);
                return "";
            });
        }

        public  Task<ObservableCollection<MangaPageItem>> GetPageList(MangaChapterItem chapter)
        {

            return Task.Run<ObservableCollection<MangaPageItem>>(() =>
            {

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<string> pageUrlList = mPattern.GetPageList(chapter.Url);
                ObservableCollection<MangaPageItem> mangaPageList = new ObservableCollection<MangaPageItem>();

                for (int i = 1; i <= pageUrlList.Count; i++)
                {
                    //string imagePath = mPattern.GetImageUrl(pageUrlList[i-1]);
                    MangaPageItem item = new MangaPageItem("page-" + i, string.Empty, pageUrlList[i - 1], chapter, i, pageUrlList.Count);
                    item.WebImageUrl = mPattern.GetImageUrl(item.PageUrl, item.PageNum);
                    mangaPageList.Add(item);

                }
                return mangaPageList;
            });
        }
        #endregion

        #region Chapter
        public  Task<ObservableCollection<MangaChapterItem>> GetChapterList(MangaMenuItem menu)
        {
            return Task.Run<ObservableCollection<MangaChapterItem>>(() =>
            {

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<TitleAndUrl> chapterUrlList = mPattern.GetChapterList(menu.Url);
                ObservableCollection<MangaChapterItem> mangaChapterList = new ObservableCollection<MangaChapterItem>();

                for (int i = 1; i <= chapterUrlList.Count; i++)
                {
                    //string imagePath = mPattern.GetImageUrl(pageUrlList[i-1]);
                    mangaChapterList.Add(new MangaChapterItem("chapter-" + i, chapterUrlList[i - 1].Title, string.Empty, string.Empty, menu, chapterUrlList[i - 1].Url));
                }
                return mangaChapterList;
            });
        }
        #endregion

        #region Menu
        //Menu
        public  Task<HubMenuGroup> GetNewMangeGroup()
        {
            return Task.Run<HubMenuGroup>(() =>
            {

                var group = new HubMenuGroup(MenuType.NewManga.ToString(), "最新漫画", string.Empty, string.Empty, string.Empty);
                ObservableCollection<MangaMenuItem> topMangaMenu = new ObservableCollection<MangaMenuItem>();

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<TitleAndUrl> newMenuList = mPattern.GetNewMangaList(MenuHtml);
                List<Size> sizeArray = new List<Size>() { HubItemSizes.FocusItem, HubItemSizes.SecondarySmallItem, HubItemSizes.SecondarySmallItem, HubItemSizes.SecondarySmallItem };
                List<string> colorArray = new List<string>() { "#FF00B1EC", "#FFA80032", "#FFA80032", "#FFA80032" };
                
                if (newMenuList != null)
                {
                    for (int i = 0; i < newMenuList.Count; i++)
                    {
                        //if (i > groupItemMaxNum) break;
                        MangaMenuItem newItem = null;
                        if (i >= sizeArray.Count)
                        {
                            //大于则用HubItemSizes.OtherSmallItem
                            newItem = new MangaMenuItem("new-" + i, newMenuList[i].Title, newMenuList[i].ImagePath, group, newMenuList[i].Url, HubItemSizes.OtherSmallItem, string.Empty);
                        }
                        else
                        {
                            newItem = new MangaMenuItem("new-" + i, newMenuList[i].Title, newMenuList[i].ImagePath, group, newMenuList[i].Url, sizeArray[i], colorArray[i]);
                        }
                        group.Items.Add(newItem);
                    }
                }

                return group;
            });
        }
        public  Task<HubMenuGroup> GetTopMangaGroup()
        {
            return Task.Run<HubMenuGroup>(() =>
            {
                var group = new HubMenuGroup(MenuType.TopManga.ToString(), "热门连载", string.Empty, string.Empty, string.Empty);
                ObservableCollection<MangaMenuItem> topMangaMenu = new ObservableCollection<MangaMenuItem>();

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<TitleAndUrl> topMenuList = mPattern.GetTopMangaList(MenuHtml);
                List<Size> sizeArray = new List<Size>() { HubItemSizes.FocusItem, HubItemSizes.SecondarySmallItem, HubItemSizes.SecondarySmallItem, HubItemSizes.SecondarySmallItem };
                List<string> colorArray = new List<string>() { "#FF00B1EC", "#FFA80032", "#FFA80032", "#FFA80032" };
                if (topMenuList != null)
                {
                    for (int i = 0; i < topMenuList.Count; i++)
                    {
                        MangaMenuItem newItem = null;
                        if (i >= sizeArray.Count)
                        {
                            //大于则用HubItemSizes.OtherSmallItem
                            newItem = new MangaMenuItem("top-" + i, topMenuList[i].Title, topMenuList[i].ImagePath, group, topMenuList[i].Url, HubItemSizes.OtherSmallItem, string.Empty);
                        }
                        else
                        {
                            newItem = new MangaMenuItem("top-" + i, topMenuList[i].Title, topMenuList[i].ImagePath, group, topMenuList[i].Url, sizeArray[i], colorArray[i]);
                        }
                        group.Items.Add(newItem);
                    }
                }
                return group;
            });
        }
        public  Task<HubMenuGroup> GetMyMangaGroup()
        {
            return Task.Run<HubMenuGroup>(() =>
            {
                var group = new HubMenuGroup(MenuType.MyFavourite.ToString(), "我的收藏", string.Empty, string.Empty, string.Empty);
                List<MangaMenuItem> menuList = SettingService.GetMyMangaMenuList();
                ObservableCollection<MangaMenuItem> myMangaMenu = new ObservableCollection<MangaMenuItem>();
                foreach (MangaMenuItem mi in menuList)
                {
                    mi.SetDefaultSize();
                    group.Items.Add(mi);

                }
                return group;
            });
        }

        public  Task<ObservableCollection<HubMenuGroup>> GetMainMenu()
        {
            return Task.Run<ObservableCollection<HubMenuGroup>>(() =>
            {
                ObservableCollection<HubMenuGroup> MenuGroups = new ObservableCollection<HubMenuGroup>();

                if (WebType == WebSiteEnum.Local)
                {
                    var group1 = new HubMenuGroup("NewGroup", "最新漫画", string.Empty, string.Empty, string.Empty);
                    group1.Items.Add(new MangaMenuItem("New-1", "海贼王", "http://localhost:8800/image/Hub/", group1, "http://comic.131.com/content/shaonian/2104.html", HubItemSizes.FocusItem, string.Empty));
                    group1.Items.Add(new MangaMenuItem("New-2", "火影", "http://localhost:8800/image/Hub/hub-BizPromotion.png", group1, "http://comic.131.com/content/shaonian/2104.html", HubItemSizes.SecondarySmallItem, "#FF00B1EC"));
                    group1.Items.Add(new MangaMenuItem("New-3", "死神", "http://localhost:8800/image/Hub/hub-announcement.png", group1, "http://comic.131.com/content/shaonian/2104.html", HubItemSizes.SecondarySmallItem, "#FFA80032"));
                    group1.Items.Add(new MangaMenuItem("New-4", "猎人", "http://localhost:8800/image/Hub/hub-News.png", group1, "http://comic.131.com/content/shaonian/2104.html", HubItemSizes.SecondarySmallItem, "#FF45008A"));

                    var group2 = new HubMenuGroup("TopGroup", "热门连载", string.Empty, string.Empty, string.Empty);
                    group2.Items.Add(new MangaMenuItem("Top-1", "海贼王", "http://localhost:8800/image/Hub/hub-perb.png", group2, "http://abchina.azurewebsites.net/onlinebanking.htm", HubItemSizes.FocusItem, string.Empty));
                    group2.Items.Add(new MangaMenuItem("Top-2", "死神", "http://localhost:8800/image/Hub/hub-promotion.png", group2, "http://www.abchina.com/cn/CreditCard/default.htm", HubItemSizes.SecondarySmallItem, "#FFB3020A"));
                    group2.Items.Add(new MangaMenuItem("Top-3", "猎人", "http://localhost:8800/image/Hub/hub-Interest1.png", group2, "http://www.abchina.com/cn/CreditCard/default.htm", HubItemSizes.SecondarySmallItem, "#FFD06112"));

                    var group3 = new HubMenuGroup("OverGroup", "热门完结", string.Empty, string.Empty, string.Empty);
                    group3.Items.Add(new MangaMenuItem("Over-1", "海贼王", "http://localhost:8800/image/Hub/hub-generalloan.png", group3, "http://www.abchina.com/cn/Common/Calculator/loan.htm", HubItemSizes.SecondarySmallItem, string.Empty));
                    group3.Items.Add(new MangaMenuItem("Over-2", "火影", "http://localhost:8800/image/Hub/hub-loancalc.png", group3, "http://www.abchina.com/cn/Common/Calculator/LoanComp.htm", HubItemSizes.SecondarySmallItem, string.Empty));
                    group3.Items.Add(new MangaMenuItem("Over-3", "死神", "http://localhost:8800/image/Hub/hub-housecalc.png", group3, "http://www.abchina.com/cn/Common/Calculator/CalcLoanOrRental.htm", HubItemSizes.SecondarySmallItem, string.Empty));
                    group3.Items.Add(new MangaMenuItem("Over-4", "猎人", "http://localhost:8800/image/Hub/hub-morecalc.png", group3, "http://www.abchina.com/cn/PublicPlate/Calculator/", HubItemSizes.OtherSmallItem, "#FFA42900"));

                    MenuGroups.Add(group1);
                    MenuGroups.Add(group2);
                    MenuGroups.Add(group3);
                    MenuGroups.Add(GetMyMangaGroup().Result);
                    return MenuGroups;
                }
                else
                {

                    //My Favourtie
                    MenuGroups.Add(GetMyMangaGroup().Result);
                    //最新
                    MenuGroups.Add(GetNewMangeGroup().Result);
                    //热门
                    MenuGroups.Add(GetTopMangaGroup().Result);

                    //MenuGroups.Add();
                    return MenuGroups;
                }
            });
        }
        #endregion

        #endregion



        public  Task<ObservableCollection<MangaMenuItem>> GetSearchingList(string queryText, int pageNum = 1)
        {
            return Task.Run<ObservableCollection<MangaMenuItem>>(() =>
            {
                ObservableCollection<MangaMenuItem> searchMangaMenu = new ObservableCollection<MangaMenuItem>();

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<TitleAndUrl> MenuList = mPattern.GetSearchingList(queryText, pageNum);
                if (MenuList != null)
                {

                    for (int i = 0; i < MenuList.Count; i++)
                    {
                        MangaMenuItem newItem = null;

                        newItem = new MangaMenuItem("search-" + i, MenuList[i].Title, MenuList[i].ImagePath, null, MenuList[i].Url, HubItemSizes.SecondarySmallItem, string.Empty);
                        searchMangaMenu.Add(newItem);
                    }
                }
                return searchMangaMenu;
            });
        }



        public void GetPageImage(MangaPageItem pageItem)
        {
            MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
            mPattern.GetImageByImageUrl(pageItem, SaveType.Temp);
        }
    }


}
