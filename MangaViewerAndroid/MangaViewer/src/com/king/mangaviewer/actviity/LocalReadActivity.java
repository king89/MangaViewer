package com.king.mangaviewer.actviity;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.animation.AnimationUtils;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;

import com.king.mangaviewer.R;
import com.king.mangaviewer.common.util.MangaImageSwitcher;

import java.util.ArrayList;
import java.util.Enumeration;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

/**
 * Created by KinG on 7/15/2015.
 */
public class LocalReadActivity extends BaseActivity {

    private String filePath;
    private MangaImageSwitcher mis;
    ArrayList<String> fl = new ArrayList<String>();

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.actitvity_local_read);

        mis = (MangaImageSwitcher)this.findViewById(R.id.mangaImageSwitcher);
        filePath = getAppViewModel().LoacalManga.getSelectedFilePath();
        mis.Initial(filePath);
        mis.setInAnimation(AnimationUtils.loadAnimation(this,
                android.R.anim.fade_in));
        mis.setOutAnimation(AnimationUtils.loadAnimation(this,
                android.R.anim.fade_out));
    }

    @Override
    protected boolean IsCanBack() {
        return true;
    }

}