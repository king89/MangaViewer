package com.king.mangaviewer.actviity;

import com.king.mangaviewer.common.Constants.MSGType;

import android.app.Activity;
import android.os.Handler;


public class BaseActivity extends Activity {

	public Handler handler = new Handler(){
		public void handleMessage(android.os.Message msg) {
			update();
		};
	};
	protected void onCreate(android.os.Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		initControl();
	};
	protected void initControl() {
		// TODO Auto-generated method stub
		
	}
	protected void update() {
		
	}
}
