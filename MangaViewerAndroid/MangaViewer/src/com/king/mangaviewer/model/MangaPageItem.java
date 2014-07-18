package com.king.mangaviewer.model;

import android.R.bool;

public class MangaPageItem extends BaseItem {
	MangaChapterItem chapter = null;
	String webImageUrl = "";
	double imageHeight = 0;
	double imageWidth = 0;
	Boolean isLoaded = false;
	
	/**
	 * @param id
	 * @param title
	 * @param description
	 * @param imagePath
	 * @param chapter
	 */
	public MangaPageItem(String id, String title, String description,
			String imagePath, MangaChapterItem chapter) {
		super(id, title, description, imagePath);
		this.chapter = chapter;
	}

	public MangaChapterItem getChapter() {
		return chapter;
	}

	public void setChapter(MangaChapterItem chapter) {
		this.chapter = chapter;
	}
	
}
