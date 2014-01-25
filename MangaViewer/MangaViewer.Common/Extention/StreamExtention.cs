using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

#if WIn8

using Windows.Security.Cryptography;
#elif WP

#endif

namespace System.IO
{
    public static class StreamExtention
    {
        /// <summary>
        /// 把流中所有的内容都以字符串的形式独取出来
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadToEnd(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        #if WIn8


        /// <summary>
        /// 将Stream转换成IRandomAccessStream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<IRandomAccessStream> ToRandomAccessStream(this Stream stream)
        {
            var buffer = CryptographicBuffer.CreateFromByteArray(stream.GetBytes());
            InMemoryRandomAccessStream inme = new InMemoryRandomAccessStream();
            await inme.WriteAsync(buffer);
            inme.Seek(0);
            return inme;
        }


        #endif

        /// <summary>
        /// 获取流中的字节数组
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        /// <summary>
        /// 将字节数组写入流（从流的起始位置写）
        /// </summary>
        /// <param name="strem"></param>
        /// <param name="buffer"></param>
        internal static void Write(this Stream strem, byte[] buffer)
        {

            strem.Write(buffer, 0, buffer.Length);
        }
    }
}
