using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtention
    {
        /// <summary>
        /// 获取字符串的字节数组（以UTF8编码）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string str)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(str);
        }
    }

}
