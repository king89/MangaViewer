package com.king.mangaviewer.actviity;

import com.king.mangaviewer.R;
import com.king.mangaviewer.common.Constants.MSGType;
import com.king.mangaviewer.common.util.MangaHelper;
import com.king.mangaviewer.common.util.SettingHelper;
import com.king.mangaviewer.viewmodel.AppViewModel;
import com.king.mangaviewer.viewmodel.MangaViewModel;





import android.app.ActionBar;
import android.app.Activity;
import android.app.ProgressDialog;
import android.os.Handler;
import android.os.Message;
import android.view.MenuItem;


public class BaseActivity extends Activity {
	

	
	public Handler handler = new Handler(){
		public void handleMessage(android.os.Message msg) {
			update(msg);
		};
	};
	protected void onCreate(android.os.Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		initControl();
		initActionBar();
	};
	protected void initActionBar() {
		// TODO Auto-generated method stub
		ActionBar actionBar = this.getActionBar();
		actionBar.setDisplayHomeAsUpEnabled(IsCanBack());
		actionBar.setTitle(getActionBarTitle());
	}
	protected void initControl() {
		// TODO Auto-generated method stub
		
	}
	protected String getActionBarTitle() {
		return (String) this.getTitle();
		
	}
	protected boolean IsCanBack() {
		return false;
	}
	protected void update(Message msg) {
		
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// TODO Auto-generated method stub
		int itemId = item.getItemId();
		if (itemId == android.R.id.home) {
			finish();
			overridePendingTransition(R.anim.in_leftright, R.anim.out_leftright);
		}
		return super.onOptionsItemSelected(item);
	}
	
	public AppViewModel getAppViewModel() {
		return ((MyApplication)this.getApplication()).AppViewModel;
	}
	
	public MangaHelper getMangaHelper() {
		return ((MyApplication)this.getApplication()).MangaHelper;
	}
	
	public SettingHelper getSettingHelper() {
		return ((MyApplication)this.getApplication()).SettingHelper;
	}
}
