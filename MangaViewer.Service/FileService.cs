using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MangaViewer.Service
{
    public static class FileService
    {
        static StorageFolder tempFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
        static StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        static CreationCollisionOption CreateOptionReplace = Windows.Storage.CreationCollisionOption.ReplaceExisting;
        static CreationCollisionOption CreateOptionOpen = Windows.Storage.CreationCollisionOption.OpenIfExists;

        public static async Task<string> SaveFileInTemp(string folderPath, string fileName, byte[] buffer)
        {

            StorageFolder saveFolder = await tempFolder.CreateFolderAsync(folderPath, CreateOptionOpen);

            var file = await saveFolder.CreateFileAsync(fileName, CreateOptionReplace);

            await Windows.Storage.FileIO.WriteBytesAsync(file, buffer);

            string resultPath = file.Path;

            return resultPath;
        }

        public static async Task<string> SaveFile(Stream stream,int count, string folder,string fileName,SaveType saveType)
        {

            byte[] buffer = new byte[count];
            int c = stream.Read(buffer, 0, (int)count);
            switch (saveType)
            {
                case SaveType.Temp:
                    {
                       return await SaveFileInTemp(folder, fileName, buffer);
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
