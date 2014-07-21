package com.king.mangaviewer.actviity;

import com.king.mangaviewer.viewmodel.AppViewModel;

import android.app.Application;

public class MyApplication extends Application {
	AppViewModel appViewModel = new AppViewModel();

	public AppViewModel getAppViewModel() {
		return appViewModel;
	}
}
