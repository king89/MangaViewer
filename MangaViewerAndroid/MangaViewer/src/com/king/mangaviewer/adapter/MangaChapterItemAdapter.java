package com.king.mangaviewer.adapter;

import java.util.List;

import com.king.mangaviewer.R;
import com.king.mangaviewer.actviity.MangaChapterActivity;
import com.king.mangaviewer.actviity.MangaPageActivity;
import com.king.mangaviewer.adapter.MangaMenuItemAdapter.ViewHolder;
import com.king.mangaviewer.common.AsyncImageLoader;
import com.king.mangaviewer.common.AsyncImageLoader.ImageCallback;
import com.king.mangaviewer.model.MangaChapterItem;
import com.king.mangaviewer.model.MangaMenuItem;
import com.king.mangaviewer.viewmodel.MangaViewModel;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.drawable.Drawable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class MangaChapterItemAdapter extends BaseAdapter {

	private Context context;
	private LayoutInflater mInflater = null;
	private MangaViewModel viewModel;
	private AsyncImageLoader asyncImageLoader = null;
	private List<MangaChapterItem> chapter;

	public MangaChapterItemAdapter(Context context, MangaViewModel viewModel,
			List<MangaChapterItem> chapter) {
		super();
		this.mInflater = LayoutInflater.from(context);
		this.viewModel = viewModel;
		this.context = context;
		this.chapter = chapter;

		asyncImageLoader = new AsyncImageLoader();
	}

	class ViewHolder {
		public ImageView imageView;
		public TextView textView;
	}

	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return chapter.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return chapter.get(position);
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	@Override
	public View getView(final int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		ViewHolder holder = null;

		if (convertView == null) {
			holder = new ViewHolder();

			convertView = mInflater
					.inflate(R.layout.list_manga_chapter_item, null);
			holder.imageView = (ImageView) convertView
					.findViewById(R.id.imageView);
			holder.textView = (TextView) convertView
					.findViewById(R.id.textView);
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		String chapterTitle = chapter.get(position).getTitle();
		holder.textView.setText(chapterTitle);
		convertView.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// TODO Auto-generated method stub
				viewModel.selectedMangaChapterItem = chapter.get(position);
				context.startActivity(new Intent(context, MangaPageActivity.class));
				((Activity)context).overridePendingTransition(R.anim.in_from_right, R.anim.out_from_left);
				
			}
		});
		return convertView;

	}
	

}
