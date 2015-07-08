package com.king.mangaviewer.actviity;

import java.util.List;

import com.king.mangaviewer.R;
import com.king.mangaviewer.R.layout;
import com.king.mangaviewer.adapter.MangaMenuItemAdapter;
import com.king.mangaviewer.common.Constants.MSGType;
import com.king.mangaviewer.common.Constants.WebSiteEnum;
import com.king.mangaviewer.common.MangaPattern.PatternFactory;
import com.king.mangaviewer.common.MangaPattern.WebSiteBasePattern;
import com.king.mangaviewer.common.util.MangaHelper;
import com.king.mangaviewer.model.MangaMenuItem;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.Menu;
import android.view.MenuItem;
import android.view.TextureView;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.GridView;
import android.widget.TextView;

public class MainActivity extends BaseActivity {

	Button bt;
	TextView tv;
	GridView gv;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

	}

	@Override
	public void update(Message msg) {
		// TODO Auto-generated method stub
		if(progressDialog.isShowing())
		{
			progressDialog.dismiss();
		}
		MangaMenuItemAdapter adapter = new MangaMenuItemAdapter(this,
				this.getAppViewModel().Manga,
				this.getAppViewModel().Manga.getNewMangaMenuList());
		gv.setAdapter(adapter);
	}

	String html;

	@Override
	protected void initControl() {
		// TODO Auto-generated method stub
		setContentView(R.layout.activity_main);

		progressDialog = ProgressDialog.show(MainActivity.this, "Loading",
				"Loading");

		new Thread() {
			@Override
			public void run() {
				// TODO Auto-generated method stub

				List<MangaMenuItem> mList = MainActivity.this.getMangaHelper()
						.GetNewMangeList();
				MainActivity.this.getAppViewModel().Manga
						.setNewMangaMenuList(mList);

				handler.sendEmptyMessage(0);
			}
		}.start();

		gv = (GridView) this.findViewById(R.id.gridView);

	}
}
