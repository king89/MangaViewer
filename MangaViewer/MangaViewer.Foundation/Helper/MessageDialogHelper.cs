using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MangaViewer.Foundation.Helper
{
    public class MessageDialogHelper
    {
        public static async Task<MessageDialogResult> ShowMessageBoxYesNo(string message, string title)
        {
            MessageDialog dlg = new MessageDialog(message, title);

            // Add commands and set their command ids
            dlg.Commands.Add(new UICommand("Yes", null, 0));
            dlg.Commands.Add(new UICommand("No", null, 1));

            // Set the command that will be invoked by default
            dlg.DefaultCommandIndex = 1;

            // Show the message dialog and get the event that was invoked via the async operator
            var result = await dlg.ShowAsync();

            if (result == dlg.Commands[0])
            {
                return MessageDialogResult.Yes;
            }
            return MessageDialogResult.No;
        }

        public async static Task<MessageDialogResult> Show(string content, string title)
        {
            var dlg = new MessageDialog(content, title);
            dlg.Commands.Add(new UICommand("OK", null));
            dlg.Commands.Add(new UICommand("Cancel", null));
            dlg.DefaultCommandIndex = 1;

            var result = await dlg.ShowAsync();
            if (result == dlg.Commands[0])
            {
                return MessageDialogResult.OK;
            }
            return MessageDialogResult.Cancel;
        }
    }

    public enum MessageDialogResult
    {
        OK,
        Cancel,
        Yes,
        No
    }
}
