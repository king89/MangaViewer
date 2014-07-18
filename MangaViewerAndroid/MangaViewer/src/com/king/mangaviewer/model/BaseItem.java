package com.king.mangaviewer.model;

public class BaseItem {
	String id = "";
	String title = "";
	String description = "";
	String imagePath = "";
	
	/**
	 * @param id
	 * @param title
	 * @param description
	 * @param imagePath
	 */
	public BaseItem(String id, String title, String description,
			String imagePath) {
		super();
		this.id = id;
		this.title = title;
		this.description = description;
		this.imagePath = imagePath;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public String getImagePath() {
		return imagePath;
	}

	public void setImagePath(String imagePath) {
		this.imagePath = imagePath;
	}



	
}
