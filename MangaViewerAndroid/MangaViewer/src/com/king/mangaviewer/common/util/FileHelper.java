package com.king.mangaviewer.common.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;

import com.king.mangaviewer.common.Constants;

import android.content.Context;
import android.os.Environment;
import android.util.Log;

public class FileHelper {
	public static String saveFile(String folderPath, String fileName,
			InputStream data) {

		try {
			byte[] tmp = new byte[data.read()];
			data.read(tmp);
			return saveFile(fileName, fileName, tmp);

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}

	public static String saveFile(String folderPath, String fileName,
			byte[] data) {
		try {

			File dir = new File(folderPath);
			if (!dir.exists()) {
				dir.mkdir();
			}
			File file = new File(dir.getAbsolutePath() + File.separator
					+ fileName);
			file.createNewFile();
			if (file.exists() && file.canWrite()) {
				FileOutputStream fos = null;
				try {
					fos = new FileOutputStream(file);
					fos.write(data);
				} catch (Exception e) {
					Log.e(Constants.LOGTAG, "ERROR", e);
				} finally {
					if (fos != null) {
						try {
							fos.flush();
							fos.close();
						} catch (IOException e) {
							// swallow
						}
					}
				}
			}

		} catch (Exception e) {
			// TODO: handle exception
			Log.e(Constants.LOGTAG, "error saveFile", e);

		}
		return null;
	}

	public static byte[] loadFile(String folderPath, String fileName) {
		String filePath = folderPath + File.separator + fileName;
		return loadFile(filePath);
	}

	public static byte[] loadFile(String filePath) {
		File file = new File(filePath);
		if (file.isFile() && file.canRead()) {
			FileInputStream fis = null;
			try {
				fis = new FileInputStream(file);
				byte[] buffer = null;
				buffer = new byte[fis.read()];
				fis.read(buffer);
				return buffer;
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				return null;
			} finally {
				if (fis != null) {
					try {
						fis.close();
					} catch (IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			}
		} else {
			return null;
		}

	}

	public static String getFileName(String str) {
		return str.substring(str.lastIndexOf(File.separator) + 1);
	}

	public static String concatPath(String path1, String path2) {
		String result = path1;
		if (path1.lastIndexOf(File.separator) > 0) {
			result = path1 + path2;
		} else {
			result = result + File.separator + path2;
		}

		return result;
	}
	public static void serializeObject(String filePath, Object item) {
		
	}
	public static Object deserializeObject(String filePath) {
		return null;
			
	}
}
