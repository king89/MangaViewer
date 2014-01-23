using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;

namespace MangaViewer.Foundation.Interactive
{
    public class EventTrigger : DependencyObject, IAttachedObject
    {
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register("EventName", typeof(string), typeof(EventTrigger), null);

        public string EventName
        {
            get
            {
                return this.GetValue(EventNameProperty) as string;
            }
            set
            {
                this.SetValue(EventNameProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(EventTrigger), null);

        public ICommand Command
        {
            get
            {
                return this.GetValue(CommandProperty) as ICommand;
            }
            set
            {
                this.SetValue(CommandProperty, value);
            }
        }


        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventTrigger), null);

        public object CommandParameter
        {
            get
            {
                return this.GetValue(CommandParameterProperty);
            }
            set
            {
                this.SetValue(CommandParameterProperty, value);
            }
        }




        public void Attach(DependencyObject dependencyObject)
        {
            Type t = dependencyObject.GetType();
            string name = this.EventName;
            var eventinfo = t.GetRuntimeEvent(name);
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("请指定事件名称");
            if (eventinfo == null)
                throw new ArgumentException("指定需要动态添加的事件不存在");
            var executemethodinfo = this.GetType().GetTypeInfo().GetDeclaredMethod("Invok").CreateDelegate(eventinfo.EventHandlerType, this);
            WindowsRuntimeMarshal.AddEventHandler(
                 del => (EventRegistrationToken)eventinfo.AddMethod.Invoke(dependencyObject, new object[] { del }),
                token => eventinfo.RemoveMethod.Invoke(dependencyObject, new object[] { token }), executemethodinfo);
            this.associatedObject = dependencyObject;
        }
        /// <summary>
        /// 当事件被触发的时候会调用这个事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventhandler"></param>
        private void Invok(object sender, object eventhandler)
        {
            ICommand cmd = this.Command;
            if (cmd != null)
            {
                if (cmd.CanExecute(this.CommandParameter))
                {
                    cmd.Execute(this.CommandParameter);
                }
            }
        }

        public void Detach()
        {
            Type t = associatedObject.GetType();
            string name = this.EventName;
            var eventinfo = t.GetRuntimeEvent(name);
            if (eventinfo == null)
                throw new ArgumentException("指定需要动态添加的事件不存在");
            var executemethodinfo = this.GetType().GetTypeInfo().GetDeclaredMethod("Invok").CreateDelegate(eventinfo.EventHandlerType, this);
            WindowsRuntimeMarshal.RemoveEventHandler((c) =>
            {
                eventinfo.RemoveMethod.Invoke(associatedObject, new object[] { c });
            }, executemethodinfo);
            this.associatedObject = null;
        }

        private DependencyObject associatedObject;

        public DependencyObject AssociatedObject
        {
            get { return associatedObject; }
        }
    }
}
