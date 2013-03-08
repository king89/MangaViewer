using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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

        public static async Task<string> SaveFileInLocal(string folderPath, string fileName, Stream stream)
        {

            StorageFolder saveFolder = await localFolder.CreateFolderAsync(folderPath, CreateOptionOpen);

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
        public static async void SaveFileInLocalByText(string folderPath, string fileName, string content)
        {
            StorageFolder saveFolder = await localFolder.CreateFolderAsync(folderPath, CreateOptionOpen);

            var file = await saveFolder.CreateFileAsync(fileName, CreateOptionReplace);

            await FileIO.WriteTextAsync(file, content);
        }
        public static async Task<string> LoadFileInLocalByText(string folderPath, string fileName)
        {

            StorageFolder saveFolder = await localFolder.CreateFolderAsync(folderPath, CreateOptionOpen);
            StorageFile file = await saveFolder.CreateFileAsync(fileName, CreateOptionOpen);
            string result = await FileIO.ReadTextAsync(file);
            return result;
        }
        public static string SaveFile(Stream stream, string folder, string fileName, SaveType saveType)
        {

            switch (saveType)
            {
                case SaveType.Temp:
                    {

                        return SaveFileInTemp(folder, fileName, stream).Result;

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

    public static class MySerialize
    {
        public static string JsonSerialize(object o)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(o.GetType()).WriteObject(ms, o);
                return Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Count());
            }
        }

        public static T JsonDeserialize<T>(string s)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(s)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }
    }
}
