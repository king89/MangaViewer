package com.king.mangaviewer.common.MangaPattern;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;





import com.king.mangaviewer.model.TitleAndUrl;

import android.content.Context;
import android.util.Log;

public class WebIManhua extends WebSiteBasePattern {
	String LOG_TAG = "WebIManhua";

	public WebIManhua(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
		WEBSITEURL = "http://www.imanhua.com";
		CHARSET = "gb2312";
	}

	@Override
	public List<TitleAndUrl> GetTopMangaList(String html) {
		// TODO Auto-generated method stub
		List<TitleAndUrl> topMangaList = new ArrayList<TitleAndUrl>();
		
		try {
			Pattern rGetUl = Pattern
					.compile("(id=[\"']comicList[\"']>.+?</ul>)");
			Matcher m = rGetUl.matcher(html);
			m.find();
			html = m.group();
			Pattern rGetLi = Pattern.compile("(<li>.+?</li>)");
			m = rGetLi.matcher(html);
			Pattern rUrlAndTitle = Pattern
					.compile("<a href=\"(.+?)\".+?title=\"(.+?)\"><img src=\"(.+?)\"");
			while (m.find()) {
				Matcher m2 = rUrlAndTitle.matcher(m.group());
				m2.find();
				String url = WEBSITEURL + m2.group(1);
				String title = m2.group(2);
				String imageUrl = m2.group(3);
				topMangaList.add(new TitleAndUrl(title, url, imageUrl));

			}
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
			Log.e(LOG_TAG, "GetTopMangaList");
		}
		return topMangaList;

	}
}
