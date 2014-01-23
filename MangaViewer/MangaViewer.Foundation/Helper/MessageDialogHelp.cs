using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.UI.Popups
{
    public class MessageDialogHelp
    {
        public async static Task<MessageDialogResult> Show(string content, string title)
        {
            var dia = new MessageDialog(content, title);
            dia.Commands.Add(new UICommand("确定", null));
            dia.Commands.Add(new UICommand("取消", null));
            dia.DefaultCommandIndex = 1;
            var result = await dia.ShowAsync();
            if (result == dia.Commands[0])
            {
                return MessageDialogResult.OK;
            }
            return MessageDialogResult.Cancel;
        }
    }

    public enum MessageDialogResult
    {
        OK,
        Cancel
    }
}
