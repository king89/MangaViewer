using System;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace MangaViewer.Foundation.Interactive
{
    public class EventToCommand : FrameworkElement
    {
        #region RelayCommand

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommand),
                                        new PropertyMetadata(DependencyProperty.UnsetValue));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        #endregion

        #region RelayCommandParameter

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventToCommand),
                                        new PropertyMetadata(DependencyProperty.UnsetValue));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        #endregion

        #region RelayCommandEvent

        public static readonly DependencyProperty EventProperty =
            DependencyProperty.Register("Event", typeof(string), typeof(EventToCommand),
                                        new PropertyMetadata(DependencyProperty.UnsetValue));

        private FrameworkElement _element;

        public string Event
        {
            get { return (string)GetValue(EventProperty); }
            set { SetValue(EventProperty, value); }
        }

        #endregion

        public FrameworkElement Element
        {
            get { return _element; }
            set
            {
                _element = value;

                var eventinfo = _element.GetType().GetRuntimeEvent(Event);

                // if event not found, throw exception
                if (eventinfo == null)
                    throw new ArgumentException(string.Format("Event {0} not found on element {1}.", Event, Element.GetType().Name));

                //Get method to call when event raised
                var executemethodinfo = GetType().GetTypeInfo().GetDeclaredMethod("ExecuteCommand").CreateDelegate(eventinfo.EventHandlerType, this);

                // Register event
                WindowsRuntimeMarshal.AddEventHandler(
                    del => (EventRegistrationToken)eventinfo.AddMethod.Invoke(_element, new object[] { del }),
                    token => eventinfo.RemoveMethod.Invoke(_element, new object[] { token }), executemethodinfo);

            }
        }


        public void ExecuteCommand(object sender, object eventhandler)
        {
            // Set the DataContext to the element DataContext (Otherwise the binding returns NULL
            DataContext = Element.DataContext;

            //Command is null, so don't do anything
            if (Command == null)
                return;

            // Get the parameter
            var exParameter = new ExCommandParameter();
            exParameter.EventArgs = eventhandler;
            exParameter.Sender = sender as DependencyObject;
            exParameter.Parameter = CommandParameter;

            Command.Execute(exParameter);
        }
    }
}
