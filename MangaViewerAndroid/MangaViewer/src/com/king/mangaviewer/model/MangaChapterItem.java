package com.king.mangaviewer.model;

public class MangaChapterItem extends BaseItem {
	MangaMenuItem menu = null;

	/**
	 * @param id
	 * @param title
	 * @param description
	 * @param imagePath
	 * @param menu
	 */


	public MangaMenuItem getMenu() {
		return menu;
	}

	public MangaChapterItem(String id, String title, String description,
			String imagePath, String url) {
		super(id, title, description, imagePath, url);
		// TODO Auto-generated constructor stub
	}

	public void setMenu(MangaMenuItem menu) {
		this.menu = menu;
	}
}
