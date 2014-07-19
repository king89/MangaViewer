package com.king.mangaviewer.common.MangaPattern;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.List;

import android.content.Context;
import android.os.Environment;

import com.king.mangaviewer.common.Constants;
import com.king.mangaviewer.common.Constants.SaveType;
import com.king.mangaviewer.common.util.FileHelper;
import com.king.mangaviewer.common.util.StringUtils;
import com.king.mangaviewer.model.MangaPageItem;

public class WebSiteBasePattern {
	private Context context;
	protected int startNum = 1;
	protected int totalNum = 1;
	protected String firstPageHtml = null;
	public String WEBSITEURL = "";
	public String WEBSEARCHURL = "";
	public String CHARSET = "utf8";

	public WebSiteBasePattern(Context context) {
		this.context = context;
	}

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

	public String getMangaFolder() {
		// Check if have external storage
		if (Environment.getExternalStorageState() == Environment.MEDIA_MOUNTED) {
			return context.getExternalFilesDir(null) + File.separator
					+ Constants.MANGAFOLDER + File.separator
					+ this.getClass().getSimpleName();
		} else {
			return context.getFilesDir() + File.separator
					+ Constants.MANGAFOLDER + File.separator
					+ this.getClass().getSimpleName();
		}

	}

	public String DownloadImgPage(String imgUrl, MangaPageItem pageItem,
			SaveType saveType, String refer) {
		String folderName = getMangaFolder() + File.separator
				+ pageItem.getFolderPath();
		String fileName = FileHelper.getFileName(imgUrl);
		try {
			URL url = new URL(imgUrl);
			HttpURLConnection conn = (HttpURLConnection) url.openConnection();
			conn.setDoInput(true);
			conn.connect();
			InputStream inputStream = conn.getInputStream();
			// TODO Save File

			FileHelper.saveFile(folderName, fileName, inputStream);

		} catch (MalformedURLException e1) {
			e1.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return null;
	}
}
