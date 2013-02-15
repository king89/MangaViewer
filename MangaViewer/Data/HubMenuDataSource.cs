using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaViewer.Model;
using MangaViewer.Common;
using MangaViewer.Foundation.Controls;

namespace MangaViewer.Data
{
    public sealed class HubMenuDataSource
    {
        private ObservableCollection<HubMenuGroup> _menuGroups = new ObservableCollection<HubMenuGroup>();
        public ObservableCollection<HubMenuGroup> MenuGroups
        {
            get { return this._menuGroups; }
        }

        public HubMenuDataSource()
        {
            string itemContent = string.Empty;

            var group1 = new HubMenuGroup("NewGroup", "最新漫画", string.Empty, string.Empty, string.Empty);
            group1.Items.Add(new MangaMenuItem("New-1", "海贼王", string.Empty, "http://localhost:8800/image/Hub/", string.Empty, itemContent, group1, "http://www.abchina.com/cn/FinancialService/ABCWealth/Products/AllProdcts/?t=安心得利", HubItemSizes.FocusItem, string.Empty));
            group1.Items.Add(new MangaMenuItem("New-2", "火影", string.Empty, "http://localhost:8800/image/Hub/hub-BizPromotion.png", string.Empty, itemContent, group1, "http://www.abchina.com/cn/PublicPlate/ABCPromotion/rss_273.xml", HubItemSizes.SecondarySmallItem, "#FF00B1EC"));
            group1.Items.Add(new MangaMenuItem("New-3", "死神", string.Empty, "http://localhost:8800/image/Hub/hub-announcement.png", string.Empty, itemContent, group1, "http://www.abchina.com/cn/PublicPlate/ServiceNotice/rss_273.xml", HubItemSizes.SecondarySmallItem, "#FFA80032"));
            group1.Items.Add(new MangaMenuItem("New-4", "猎人", string.Empty, "http://localhost:8800/image/Hub/hub-News.png", string.Empty, itemContent, group1, "http://www.abchina.com/cn/AboutABC/NewsCenter/rss_273.xml", HubItemSizes.SecondarySmallItem, "#FF45008A"));

            var group2 = new HubMenuGroup("TopGroup", "热门连载", string.Empty, string.Empty, string.Empty);
            group2.Items.Add(new MangaMenuItem("Top-1", "海贼王", string.Empty, "http://localhost:8800/image/Hub/hub-perb.png", string.Empty, itemContent, group2, "http://abchina.azurewebsites.net/onlinebanking.htm", HubItemSizes.FocusItem, string.Empty));
            group2.Items.Add(new MangaMenuItem("Top-2", "死神", string.Empty, "http://localhost:8800/image/Hub/hub-promotion.png", string.Empty, itemContent, group2, "http://www.abchina.com/cn/CreditCard/default.htm", HubItemSizes.SecondarySmallItem, "#FFB3020A"));
            group2.Items.Add(new MangaMenuItem("Top-3", "猎人", string.Empty, "http://localhost:8800/image/Hub/hub-Interest1.png", string.Empty, itemContent, group2, "http://www.abchina.com/cn/CreditCard/default.htm", HubItemSizes.SecondarySmallItem, "#FFD06112"));

            var group3 = new HubMenuGroup("OverGroup", "热门完结", string.Empty, string.Empty, string.Empty);
            group3.Items.Add(new MangaMenuItem("Over-1", "海贼王", string.Empty, "http://localhost:8800/image/Hub/hub-generalloan.png", "一般贷款计算", itemContent, group3, "http://www.abchina.com/cn/Common/Calculator/loan.htm", HubItemSizes.SecondarySmallItem, string.Empty));
            group3.Items.Add(new MangaMenuItem("Over-2", "火影", string.Empty, "http://localhost:8800/image/Hub/hub-loancalc.png", "看看商业住房贷款和住房公积金贷款这两种不同的贷款方式哪种能适合您。", itemContent, group3, "http://www.abchina.com/cn/Common/Calculator/LoanComp.htm", HubItemSizes.SecondarySmallItem, string.Empty));
            group3.Items.Add(new MangaMenuItem("Over-3", "死神", string.Empty, "http://localhost:8800/image/Hub/hub-housecalc.png", "为了便于比较，本计算器的计算前提是支出相等、且租房费用不变。在此前提下，计算和比较采取购房和租房两种方式各自在一段时间后的净资产值。", itemContent, group3, "http://www.abchina.com/cn/Common/Calculator/CalcLoanOrRental.htm", HubItemSizes.SecondarySmallItem, string.Empty));
            group3.Items.Add(new MangaMenuItem("Over-4", "猎人", string.Empty, "http://localhost:8800/image/Hub/hub-morecalc.png", string.Empty, itemContent, group3, "http://www.abchina.com/cn/PublicPlate/Calculator/", HubItemSizes.OtherSmallItem, "#FFA42900"));

            this.MenuGroups.Add(group1);
            this.MenuGroups.Add(group2);
            this.MenuGroups.Add(group3);
        }
    }
}
