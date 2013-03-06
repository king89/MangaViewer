using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Service
{
    public static class CommonService
    {
        public static T Clone<T>(object obj)
        {
            string xml = MySerialize.JsonSerialize(obj);

            object newObj = MySerialize.JsonDeserialize<T>(xml);

            return (T)newObj;
        }
    }
}
