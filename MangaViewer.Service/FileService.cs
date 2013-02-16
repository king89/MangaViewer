using System;
using System.Collections.Generic;
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

        public static async Task<string> SaveMangaInTemp(string folderPath, string fileName, byte[] buffer)
        {

            StorageFolder saveFolder = await tempFolder.CreateFolderAsync(folderPath, CreateOptionOpen);

            var file = await saveFolder.CreateFileAsync(fileName, CreateOptionReplace);

            await Windows.Storage.FileIO.WriteBytesAsync(file, buffer);

           string resultPath = file.Path;

            return resultPath;
        }
    }
}
