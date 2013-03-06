using MangaViewer.Foundation.Controls;
using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Service
{
    public static class MangaService
    {

        static WebSiteEnum WebType 
        {
            get
            {
                return SettingService.GetWebSite();
            }
        }
        /// <summary>
        ///   get web image and save into temp folder, return local path uri
        /// </summary>
        public static Task<string> GetIamgeByImageUrl(MangaPageItem page)
        {
            return Task.Run<string>(() =>
            {
                //Get Image
                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                return mPattern.GetImageByImageUrl(page);
            });
        }

        public static Task<ObservableCollection<MangaPageItem>> GetPageList(MangaChapterItem chapter)
        {

            return Task.Run<ObservableCollection<MangaPageItem>>(() =>
            {

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<string> pageUrlList = mPattern.GetPageList(chapter.Url);
                ObservableCollection<MangaPageItem> mangaPageList = new ObservableCollection<MangaPageItem>();

                for (int i = 1; i <= pageUrlList.Count; i++)
                {
                    //string imagePath = mPattern.GetImageUrl(pageUrlList[i-1]);
                    mangaPageList.Add(new MangaPageItem("page-" + i, string.Empty, pageUrlList[i - 1], chapter, i, pageUrlList.Count));

                }
                return mangaPageList;
            });
        }

        public static Task<ObservableCollection<MangaChapterItem>> GetChapterList(MangaMenuItem menu)
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

        //Menu
        public static Task<HubMenuGroup> GetTopMangaGroup()
        {
            return Task.Run<HubMenuGroup>(() =>
            {
                var group = new HubMenuGroup("TopGroup", "热门连载", string.Empty, string.Empty, string.Empty);
                ObservableCollection<MangaMenuItem> topMangaMenu = new ObservableCollection<MangaMenuItem>();

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<TitleAndUrl> topMenuList = mPattern.GetTopMangaList();
                for (int i = 0; i < topMenuList.Count; i++ )
                {
                    //string imagePath = mPattern.GetImageUrl(pageUrlList[i-1]);
                    topMangaMenu.Add(new MangaMenuItem("menu-" + i, topMenuList[i].Title,topMenuList[i].ImagePath , group, topMenuList[i - 1].Url,HubItemSizes.PrimaryItem,"White"));
                }
                return group;
            });
        }
        public static Task<HubMenuGroup> GetMyMangaGroup()
        {
            return Task.Run<HubMenuGroup>(() =>
            {
                var group = new HubMenuGroup("MyGroup", "我的收藏", string.Empty, string.Empty, string.Empty);
                List<MangaMenuItem> menuList = SettingService.GetMyMangaMenuList();
                ObservableCollection<MangaMenuItem> myMangaMenu = new ObservableCollection<MangaMenuItem>();
                foreach (MangaMenuItem mi in menuList)
                {
                   group.Items.Add(mi);

                }
                return group;
            });
        }

        public static Task<ObservableCollection<HubMenuGroup>> GetMainMenu()
        {
            return Task.Run<ObservableCollection<HubMenuGroup>>(() =>
            {
                ObservableCollection<HubMenuGroup> MenuGroups = new ObservableCollection<HubMenuGroup>();

                if (WebType == WebSiteEnum.Local || WebType == WebSiteEnum.Comic131)
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

                MenuGroups.Add(GetTopMangaGroup().Result);
                
                //MenuGroups.Add();
                return MenuGroups;
            });
        }
    }


}
