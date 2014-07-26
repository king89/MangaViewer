package com.king.mangaviewer.viewmodel;

import java.util.List;

import com.king.mangaviewer.model.MangaChapterItem;
import com.king.mangaviewer.model.MangaMenuItem;
import com.king.mangaviewer.model.MangaPageItem;

public class MangaViewModel extends ViewModelBase {

	List<MangaMenuItem> newMangaMenuList = null;

	public List<MangaMenuItem> getNewMangaMenuList() {
		return newMangaMenuList;
	}

	public void setNewMangaMenuList(List<MangaMenuItem> newMangaMenuList) {
		this.newMangaMenuList = newMangaMenuList;
	}

	public MangaMenuItem selectedMangaMenuItem = null;

	public List<MangaChapterItem> mangaChapterList = null;

	public MangaChapterItem selectedMangaChapterItem = null;

	public List<MangaPageItem> mangaPageList = null;

	public MangaPageItem selectedMangaPageItem = null;
}
