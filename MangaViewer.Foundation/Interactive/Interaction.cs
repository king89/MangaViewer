using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MangaViewer.Foundation.Interactive
{
    /// <summary>
    /// 用于给DependencyObject添加Behavior或者EventTrigger
    /// </summary>
    public static class Interaction
    {
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached("Behaviors", typeof(BehaviorCollection), typeof(Interaction), new PropertyMetadata(new PropertyChangedCallback(Interaction.OnBehaviorsChanged)));
        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached("Triggers", typeof(EventTriggerCollection), typeof(Interaction), new PropertyMetadata(null, new PropertyChangedCallback(Interaction.OnTriggersChanged)));


        public static EventTriggerCollection GetTriggers(DependencyObject obj)
        {
            EventTriggerCollection Triggers = (EventTriggerCollection)obj.GetValue(TriggersProperty);
            if (Triggers == null)
            {
                Triggers = new EventTriggerCollection();
                obj.SetValue(TriggersProperty, Triggers);
            }
            return Triggers;
        }


        private static void OnTriggersChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            EventTriggerCollection oldValue = (EventTriggerCollection)args.OldValue;
            EventTriggerCollection newValue = (EventTriggerCollection)args.NewValue;
            if (oldValue != newValue)
            {
                if (oldValue != null && oldValue.AssociatedObject != null) oldValue.Detach();
                if (newValue != null && obj != null)
                {
                    if (newValue.AssociatedObject != null) throw new InvalidOperationException("无法多次附加");
                    newValue.Attach(obj);
                }
            }


        }

        public static BehaviorCollection GetBehaviors(DependencyObject obj)
        {
            BehaviorCollection behaviors = (BehaviorCollection)obj.GetValue(BehaviorsProperty);
            if (behaviors == null)
            {
                behaviors = new BehaviorCollection();
                obj.SetValue(BehaviorsProperty, behaviors);
            }
            return behaviors;
        }

        private static void OnBehaviorsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            BehaviorCollection oldValue = (BehaviorCollection)args.OldValue;
            BehaviorCollection newValue = (BehaviorCollection)args.NewValue;
            if (oldValue != newValue)
            {
                if (oldValue != null && oldValue.AssociatedObject != null) oldValue.Detach();
                if (newValue != null && obj != null)
                {
                    if (newValue.AssociatedObject != null) throw new InvalidOperationException("无法多次附加");
                    newValue.Attach(obj);
                }
            }
        }


        #region EventTrigger
        /// <summary>
        /// 仅仅是为了添加单个的EventTrigger
        /// </summary>
        public static readonly DependencyProperty EventTriggerProperty = DependencyProperty.RegisterAttached("EventTrigger", typeof(EventTrigger), typeof(Interaction), new PropertyMetadata(null, OnAttachObjectChanged));

        public static EventTrigger GetEventTrigger(DependencyObject obj)
        {
            return obj.GetValue(EventTriggerProperty) as EventTrigger;
        }
        public static void SetEventTrigger(DependencyObject obj, EventTrigger value)
        {
            obj.SetValue(EventTriggerProperty, value);
        }
        #endregion


        #region Behavior
        public static readonly DependencyProperty BehaviorProperty = DependencyProperty.RegisterAttached("Behavior", typeof(Behavior), typeof(Interaction), new PropertyMetadata(null, OnAttachObjectChanged));

        public static Behavior GetBehavior(DependencyObject obj)
        {
            return obj.GetValue(BehaviorProperty) as Behavior;
        }
        public static void SetBehavior(DependencyObject obj, Behavior value)
        {
            obj.SetValue(BehaviorProperty, value);
        }
        private static void OnAttachObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            IAttachedObject oldvalue = args.OldValue as IAttachedObject;
            IAttachedObject newvalue = args.NewValue as IAttachedObject;
            if (oldvalue != null)
                oldvalue.Detach();
            if (newvalue != null)
                newvalue.Attach(obj);
        }
        #endregion


    }
}
