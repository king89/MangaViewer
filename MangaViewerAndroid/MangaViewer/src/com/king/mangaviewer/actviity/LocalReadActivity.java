package com.king.mangaviewer.actviity;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;

import com.king.mangaviewer.R;

import java.util.ArrayList;
import java.util.Enumeration;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

/**
 * Created by KinG on 7/15/2015.
 */
public class LocalReadActivity extends BaseActivity {

    private String filePath;
    ImageView im;
    ListView lv;
    ArrayList<String> fl = new ArrayList<String>();

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.actitvity_local_read);
        im = (ImageView)this.findViewById(R.id.imageView);
        lv = (ListView)this.findViewById(R.id.listView);

        filePath = getAppViewModel().LoacalManga.getSelectedFilePath();

        LoadManga();
    }

    @Override
    protected boolean IsCanBack() {
        return true;
    }

    private void LoadManga() {
        try {
            ZipEntry ze = null;
            ZipFile zp = new ZipFile(filePath);
            Enumeration<? extends ZipEntry> it = zp.entries();
            while (it.hasMoreElements()) {
                ze = it.nextElement();
                fl.add(ze.getName());
                Log.v("loadManga", "" + ze.getSize());
            }

        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        lv.setAdapter(new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,fl));
        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                try {

                    ZipFile zf = new ZipFile(filePath);
                    ZipEntry ze = zf.getEntry(fl.get(position));

                    Bitmap mBackground = BitmapFactory.decodeStream(zf.getInputStream(ze));
                    im.setImageBitmap(mBackground);

                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        });
    }
}