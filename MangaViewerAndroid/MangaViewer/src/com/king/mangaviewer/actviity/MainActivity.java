package com.king.mangaviewer.actviity;

import com.king.mangaviewer.R;
import com.king.mangaviewer.R.layout;
import com.king.mangaviewer.common.Constants.MSGType;
import com.king.mangaviewer.common.Constants.WebSiteEnum;
import com.king.mangaviewer.common.MangaPattern.PatternFactory;
import com.king.mangaviewer.common.MangaPattern.WebSiteBasePattern;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.os.Handler;
import android.view.Menu;
import android.view.MenuItem;
import android.view.TextureView;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

public class MainActivity extends BaseActivity {

	Button bt;
	TextView tv;
	private ProgressDialog progressDialog;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

	}

	@Override
	public void update() {
		// TODO Auto-generated method stub
		progressDialog.dismiss();
		tv.setText(html);
	}

	String html;

	@Override
	protected void initControl() {
		// TODO Auto-generated method stub
		setContentView(R.layout.activity_main);
		bt = (Button) this.findViewById(R.id.button1);
		bt.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View arg0) {
				// TODO Auto-generated method stub
				progressDialog = ProgressDialog.show(MainActivity.this,
						"Loading", "Loading");

				new Thread() {

					@Override
					public void run() {
						// TODO Auto-generated method stub

						WebSiteBasePattern pattern = PatternFactory.getPattern(
								MainActivity.this, WebSiteEnum.IManhua);
						html = pattern.GetHtml("http://www.baidu.com");
						handler.sendEmptyMessage(0);
						// tv.setText(html);
					}
				}.start();
			}
		});

		tv = (TextView) this.findViewById(R.id.textView1);
	}
}
