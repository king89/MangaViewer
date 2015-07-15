package com.king.mangaviewer.actviity;

import java.util.List;

import android.app.ProgressDialog;
import android.os.Bundle;
import android.os.Message;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.king.mangaviewer.R;
import com.king.mangaviewer.adapter.MangaChapterItemAdapter;
import com.king.mangaviewer.model.MangaChapterItem;
import com.king.mangaviewer.model.MangaMenuItem;
import com.king.mangaviewer.viewmodel.MangaViewModel;

public class MangaChapterActivity extends BaseActivity {

	protected ProgressDialog progressDialog;

	ListView listView = null;
	ImageView imageView = null;
	TextView textView = null;

	@Override
	protected void initControl() {
		// TODO Auto-generated method stub
		setContentView(R.layout.activity_manga_chapter);

		listView = (ListView) this.findViewById(R.id.listView);
		imageView = (ImageView) this.findViewById(R.id.imageView);
		textView = (TextView) this.findViewById(R.id.textView);

		new Thread() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				MangaViewModel mangaViewModel = MangaChapterActivity.this
						.getAppViewModel().Manga;
				
				List<MangaChapterItem> mList = MangaChapterActivity.this
						.getMangaHelper().getChapterList(
								mangaViewModel.getSelectedMangaMenuItem());
				mangaViewModel.setMangaChapterList(mList);

				handler.sendEmptyMessage(0);

			}
		}.start();
	}

	@Override
	protected void update(Message msg) {
		// TODO Auto-generated method stub
		if (progressDialog != null) {
			progressDialog.dismiss();
		}
	
		ListAdapter adapter = new MangaChapterItemAdapter(this,
				this.getAppViewModel().Manga,
				this.getAppViewModel().Manga.mangaChapterList);

		listView.setAdapter(adapter);
	}

	@Override
	protected boolean IsCanBack() {
		// TODO Auto-generated method stub
		return true;
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
