using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MangaViewer.Foundation.Interactive
{
    /// <summary>  
    /// 扩展CommandParameter，使CommandParameter可以带事件参数  
    /// </summary>  
    public class ExCommandParameter
    {
        /// <summary>  
        /// 事件触发源  
        /// </summary>  
        public DependencyObject Sender { get; set; }
        /// <summary>  
        /// 事件参数  
        /// </summary>  
        public object EventArgs { get; set; }
        /// <summary>  
        /// 额外参数  
        /// </summary>  
        public object Parameter { get; set; }
    }
}
