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
        static StorageFolder TempFolder
        {
            get { return Windows.Storage.ApplicationData.Current.TemporaryFolder; }
        }

        static StorageFolder LocalFolder
        {
            get { return Windows.Storage.ApplicationData.Current.LocalFolder;}
        }
        
        static CreationCollisionOption CreateOptionReplace
        {
            get { return Windows.Storage.CreationCollisionOption.ReplaceExisting; }
        }
       
        static CreationCollisionOption CreateOptionOpen 
        {
            get { return Windows.Storage.CreationCollisionOption.OpenIfExists; }
        }

        public static async Task<string> SaveFileInTemp(string folderPath, string fileName, Stream stream)
        {

            StorageFolder saveFolder = await TempFolder.CreateFolderAsync(folderPath, CreateOptionOpen);

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

            StorageFolder saveFolder = await LocalFolder.CreateFolderAsync(folderPath, CreateOptionOpen);

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
            StorageFolder saveFolder = await LocalFolder.CreateFolderAsync(folderPath, CreateOptionOpen);
            var file = await saveFolder.CreateFileAsync(fileName, CreateOptionReplace);
#if Win8
            await FileIO.WriteTextAsync(file, content);
#elif WP
            using (IRandomAccessStream output = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                Stream outputstream = WindowsRuntimeStreamExtensions.AsStreamForWrite(output.GetOutputStreamAt(0));
                using (var writer = new StreamWriter(outputstream))
                {
                    writer.Write(content);
                }
                outputstream.Dispose();
            }
#endif

        }
        public static async Task<string> LoadFileInLocalByText(string folderPath, string fileName)
        {

            StorageFolder saveFolder = await LocalFolder.CreateFolderAsync(folderPath, CreateOptionOpen);
            StorageFile file = await saveFolder.CreateFileAsync(fileName, CreateOptionOpen);
#if Win8

            string result = await FileIO.ReadTextAsync(file);

#elif WP

            string result = "";
            using (IRandomAccessStream input = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                Stream readstream = WindowsRuntimeStreamExtensions.AsStreamForRead(input.GetInputStreamAt(0));
                using (var reader = new StreamReader(readstream))
                {
                    result = reader.ReadToEnd();
                } 
            }
#endif

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
