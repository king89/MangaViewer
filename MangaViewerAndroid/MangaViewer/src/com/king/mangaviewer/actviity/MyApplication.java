package com.king.mangaviewer.actviity;

import com.king.mangaviewer.common.util.MangaHelper;
import com.king.mangaviewer.common.util.SettingHelper;
import com.king.mangaviewer.viewmodel.AppViewModel;

import android.app.Application;

public class MyApplication extends Application {
	public AppViewModel AppViewModel;

	public SettingHelper SettingHelper;
	public MangaHelper MangaHelper;

	public MyApplication() {
		super();
		// TODO Auto-generated constructor stub
		AppViewModel = new AppViewModel();
		SettingHelper = new SettingHelper();
		MangaHelper = new MangaHelper(this, SettingHelper);
	}
	
}
