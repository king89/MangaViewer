package com.king.mangaviewer.adapter;

import java.util.List;







import com.king.mangaviewer.actviity.MangaChapterActivity;
import com.king.mangaviewer.common.AsyncImageLoader;
import com.king.mangaviewer.common.AsyncImageLoader.ImageCallback;
import com.king.mangaviewer.R;
import com.king.mangaviewer.model.MangaMenuItem;
import com.king.mangaviewer.viewmodel.MangaViewModel;

import android.R.integer;
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

public class MangaMenuItemAdapter extends BaseAdapter {
	private Context context;
	private LayoutInflater mInflater = null;
	private MangaViewModel viewModel;
	private AsyncImageLoader asyncImageLoader = null;
	private List<MangaMenuItem> menu;

	class ViewHolder
	{
		public ImageView imageView;
		public TextView textView;
	}
	public MangaMenuItemAdapter(Context context, MangaViewModel viewModel,
			List<MangaMenuItem> menu) {
		super();
		this.mInflater = LayoutInflater.from(context);
		this.viewModel = viewModel;
		this.context = context;
		this.menu = menu;
		
		asyncImageLoader = new AsyncImageLoader();
	}

	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return menu.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		Object result = null;
		try {
			result = menu.get(position);
		} catch (Exception e) {
			// TODO: handle exception
			result = null;
		}
		return result;
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		ViewHolder holder = null;
      
        if(convertView == null)
        {
            holder = new ViewHolder();
         
            convertView = mInflater.inflate(R.layout.list_manga_menu_item ,null);
            holder.imageView = (ImageView)convertView.findViewById(R.id.imageView);
            holder.textView = (TextView)convertView.findViewById(R.id.textView);
            convertView.setTag(holder);
        }else
        {
            holder = (ViewHolder)convertView.getTag();
        }
        String imagePath = this.menu.get(position).getImagePath();
    	Drawable cachedImage = asyncImageLoader.loadDrawable(imagePath,
				holder.imageView, new ImageCallback() {

					public void imageLoaded(Drawable imageDrawable,
							ImageView imageView, String imageUrl) {
						// TODO Auto-generated method stub
						if (imageDrawable != null) {
							imageView.setImageDrawable(imageDrawable);
						}
						
						
					}
				});
    if (cachedImage != null) {
    	holder.imageView.setImageDrawable(cachedImage);
	}
    String title = this.menu.get(position).getTitle();
	holder.textView.setText(title);
	final int menuPos = position;
	convertView.setOnClickListener(new OnClickListener() {
		
		@Override
		public void onClick(View arg0) {
			// TODO Auto-generated method stub
			//Toast.makeText(context, "Item", Toast.LENGTH_SHORT).show();
			viewModel.selectedMangaMenuItem = menu.get(menuPos);
			context.startActivity(new Intent(context,MangaChapterActivity.class));
			((Activity)context).overridePendingTransition(R.anim.in_from_right, R.anim.out_from_left);
		}
	});
        return convertView;
	
	}

}
