package com.king.mangaviewer.common.MangaPattern;

import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.List;

import com.king.mangaviewer.common.util.StringUtils;
import com.king.mangaviewer.model.MangaPageItem;

public class WebSiteBasePattern {
	protected int startNum = 1;
	protected int totalNum = 1;
	protected String firstPageHtml = null;
	public String WEBSITEURL = "";
	public String WEBSEARCHURL = "";
	public String CHARSET = "utf8";

	public List<String> GetPageList(String firstPageUrl) {
		return null;
	}

	public String GetImageUrl(String pageUrl) {
		return null;
	}

	public String GetImageUrl(String pageUrl, int nowPage) {
		return null;
	}

	// public void GetImageByImageUrl(MangaPageItem page,SaveType saveType) {
	// return ; }
	public int InitSomeArgs(String firstPageUrl) {
		return 0;
	}

	// public void DownloadOnePage(String pageUrl,String folder,int nowPageNum)
	// { return; }
	public String GetHtml(String Url) {
		URL url;
		try {
			url = new URL(Url);
			HttpURLConnection conn = (HttpURLConnection) url.openConnection();
			conn.setDoInput(true);
			conn.connect();
			InputStream inputStream = conn.getInputStream();
			String html = StringUtils.inputStreamToString(inputStream);
			return html;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return "";
		}

	}
}
