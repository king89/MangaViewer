package com.king.mangaviewer.actviity;

import java.util.List;

import android.app.ProgressDialog;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.GridView;
import android.widget.TextView;

import com.king.mangaviewer.R;
import com.king.mangaviewer.model.MangaMenuItem;

public class MangaChapterActivity extends BaseActivity {
	
	@Override
	protected void initControl() {
		// TODO Auto-generated method stub
		this.IsCanBack = true;
		setContentView(R.layout.activity_manga_chapter);
		
		
	}

	@Override
	protected String getActionBarTitle() {
		// TODO Auto-generated method stub
		return this.getAppViewModel().Manga.selectedMangaMenuItem.getTitle();
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
	}
}
