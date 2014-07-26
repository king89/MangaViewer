package com.king.mangaviewer.viewmodel;

public class AppViewModel extends ViewModelBase {
	public MangaViewModel Manga;
	public SettingViewModel Setting;
	public AppViewModel() {
		this.Manga = new MangaViewModel();;
		this.Setting = new SettingViewModel();;
	}
	
	
}
