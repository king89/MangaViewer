package com.king.mangaviewer.common;

import com.king.mangaviewer.common.MangaPattern.WebHHComic;
import com.king.mangaviewer.common.MangaPattern.WebIManhua;

public class Constants {
	public static enum MSGType
	{
		Menu,
		Chapter,
		Page;
	}
	
	public enum WebSiteEnum
    {

        IManhua(WebIManhua.class.getName(),0),
        HHComic(WebHHComic.class.getName(),1);
//        Local();
        
        private String clsName;
        private int index;
        private WebSiteEnum(String cls, int index)
        {
        	this.clsName = cls;
        	this.index = index;
        }
        
        public String getClsName() {
			return this.clsName;
		}

		public int getIndex() {
			return index;
		}

    }
	
	public enum SaveType
    {
        Temp,
        Local
    }

	public static String MANGAFOLDER = "Manga";
	public static String LOGTAG = "MangaViewer";
}
