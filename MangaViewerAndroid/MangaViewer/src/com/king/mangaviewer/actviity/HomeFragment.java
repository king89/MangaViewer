package com.king.mangaviewer.actviity;


import android.app.Fragment;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.os.Handler;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.GridView;

import com.king.mangaviewer.R;
import com.king.mangaviewer.adapter.MangaMenuItemAdapter;
import com.king.mangaviewer.model.MangaMenuItem;

import java.util.List;


public class HomeFragment extends Fragment {

    public HomeFragment(){}
    ProgressDialog progressDialog;
    public GridView gv;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View rootView = inflater.inflate(R.layout.fragment_home, container, false);

        gv = (GridView) rootView.findViewById(R.id.gridView);
        progressDialog = ProgressDialog.show(this.getActivity(), "Loading",
                "Loading");

		new Thread() {
			@Override
			public void run() {
				// TODO Auto-generated method stub
                MainActivity copy = (MainActivity)getActivity();
				List<MangaMenuItem> mList = copy.getMangaHelper()
						.GetNewMangeList();
                copy.getAppViewModel().Manga
						.setNewMangaMenuList(mList);

				handler.sendEmptyMessage(0);
			}
		}.start();

        return rootView;
    }

    public Handler handler = new Handler()
    {
        public void handleMessage(android.os.Message msg) {
            if(progressDialog.isShowing())
            {
                progressDialog.dismiss();
            }
            MainActivity copy = (MainActivity)getActivity();
            MangaMenuItemAdapter adapter = new MangaMenuItemAdapter(copy,
                    copy.getAppViewModel().Manga,
                    copy.getAppViewModel().Manga.getNewMangaMenuList());
            gv.setAdapter(adapter);
        };

    };
}