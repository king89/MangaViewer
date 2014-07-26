package com.king.mangaviewer.common.util;

import java.util.ArrayList;
import java.util.List;

import android.content.Context;

import com.king.mangaviewer.common.MangaPattern.PatternFactory;
import com.king.mangaviewer.common.MangaPattern.WebSiteBasePattern;
import com.king.mangaviewer.model.MangaChapterItem;
import com.king.mangaviewer.model.MangaMenuItem;
import com.king.mangaviewer.model.MangaPageItem;
import com.king.mangaviewer.model.TitleAndUrl;

public class MangaHelper {
	public MangaHelper(Context context, SettingHelper setting) {
		settingHelper = setting;
		this.context = context;
		// WebType = setting;
	}

	private SettingHelper settingHelper;
	private Context context;
	String menuHtml = "";

	private String getMenuHtml() {
		if (menuHtml.equalsIgnoreCase("")) {
			WebSiteBasePattern pattern = PatternFactory.getPattern(context,
					settingHelper.getWebType());
			return pattern.GetHtml(pattern.WEBSITEURL);
		} else {
			return menuHtml;
		}

	}

	/* Page */
	public String GetIamgeByImageUrl(MangaPageItem page) {
		return null;

	}

	public List<MangaPageItem> GetPageList(MangaChapterItem chapter) {

		WebSiteBasePattern mPattern = PatternFactory.getPattern(context,
				settingHelper.getWebType());
		List<String> pageUrlList = mPattern.GetPageList(chapter.getUrl());
		List<MangaPageItem> mangaPageList = new ArrayList<MangaPageItem>();

		for (int i = 1; i <= pageUrlList.size(); i++) {
			// string imagePath = mPattern.GetImageUrl(pageUrlList[i-1]);
			MangaPageItem item = new MangaPageItem("page-" + i, null, null,
					null, pageUrlList.get(i - 1), chapter, i,
					pageUrlList.size());
			item.setWebImageUrl(mPattern.GetImageUrl(item.getUrl(),
					item.getNowNum()));
			mangaPageList.add(item);

		}
		return mangaPageList;

	}

	/* Chapter */
	public List<MangaChapterItem> getChapterList() {
		return null;
	}

	/* Menu */
	public List<MangaMenuItem> GetNewMangeList() {
		WebSiteBasePattern mPattern = PatternFactory.getPattern(context,
				settingHelper.getWebType());
		String html = getMenuHtml();
		List<TitleAndUrl> pageUrlList = mPattern.GetTopMangaList(html);

		List<MangaMenuItem> menuList = new ArrayList<MangaMenuItem>();
		for (int i = 0; i < pageUrlList.size(); i++) {
			menuList.add(new MangaMenuItem("Menu-" + i, pageUrlList.get(i)
					.getTitle(), null, pageUrlList.get(i).getImagePath(),
					pageUrlList.get(i).getUrl()));
		}

		return menuList;
	}
}
