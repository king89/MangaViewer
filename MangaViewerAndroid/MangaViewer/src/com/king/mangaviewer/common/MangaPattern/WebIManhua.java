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

	@Override
	public List<TitleAndUrl> GetChapterList(String chapterUrl) {
		// TODO Auto-generated method stub
		//http://comic.131.com/content/shaonian/2104.html
        String html = GetHtml(chapterUrl);
        //Rex1  = <ul class="mh_fj" .+<li>.+</li></ul>
        Pattern rGetUl = Pattern.compile("id=[\"']subBookList[\"']>.+?</ul>");
        //Rex2 = <li>.*?</li>
        Matcher m = rGetUl.matcher(html);
        m.find();
        Pattern rGetLi = Pattern.compile("<li>.+?</li>");
        html = m.group(0);
        m = rGetLi.matcher(html);
        List<TitleAndUrl> chapterList = new ArrayList<TitleAndUrl>();
        Pattern rUrlAndTitle = Pattern.compile("<a href=\"(.+?)\".+?>(.+?)<");
        m = rUrlAndTitle.matcher(html);
        while (m.find()) {
        	
        	String url = WEBSITEURL + m.group(1);
        	String title = m.group(2);
            chapterList.add(new TitleAndUrl(title,url));

        }


        return chapterList;
	}
}
