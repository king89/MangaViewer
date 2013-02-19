using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MangaViewer.Service
{
    public static class FileService
    {
        static object syncWrite = new object();
        static StorageFolder tempFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
        static StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        static CreationCollisionOption CreateOptionReplace = Windows.Storage.CreationCollisionOption.ReplaceExisting;
        static CreationCollisionOption CreateOptionOpen = Windows.Storage.CreationCollisionOption.OpenIfExists;

        public static async Task<string> SaveFileInTemp(string folderPath, string fileName, Stream stream)
        {

            StorageFolder saveFolder = await tempFolder.CreateFolderAsync(folderPath, CreateOptionOpen);

            var file = await saveFolder.CreateFileAsync(fileName, CreateOptionReplace);

            using (IRandomAccessStream output = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                Stream outputstream = WindowsRuntimeStreamExtensions.AsStreamForWrite(output.GetOutputStreamAt(0));
                await stream.CopyToAsync(outputstream);
                outputstream.Dispose();
                stream.Dispose();
            }


            string resultPath = file.Path;

            return resultPath;
        }

        public static string SaveFile(Stream stream,int count, string folder,string fileName,SaveType saveType)
        {

            byte[] buffer = new byte[count];
            int c = stream.Read(buffer, 0, (int)count);
            switch (saveType)
            {
                case SaveType.Temp:
                    {
                        lock (syncWrite)
                        {
                            return SaveFileInTemp(folder, fileName, stream).Result;
                        }

                    }
                case SaveType.Local:
                    {
                        //
                        return null;
                    }
                default: return null;
            }

        }
    }
}
