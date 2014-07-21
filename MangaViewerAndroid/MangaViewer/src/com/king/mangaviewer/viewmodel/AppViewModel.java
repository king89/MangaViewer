package com.king.mangaviewer.viewmodel;

public class AppViewModel extends ViewModelBase {
	MangaViewModel mangaViewModel = new MangaViewModel();
	SettingViewModel settingViewModel = new SettingViewModel();
	
	public MangaViewModel getMangaViewModel() {
		return mangaViewModel;
	}
	public SettingViewModel getSettingViewModel() {
		return settingViewModel;
	}
}
