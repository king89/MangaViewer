using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace MangaViewer.Foundation.Helper
{
    public static class AnimationHelper
    {
        public static DoubleAnimation CreateDoubleAnimation(double? from, double? to, Duration duration, string targetProperty, DependencyObject target, EasingFunctionBase easing = null)
        {
            var db = new DoubleAnimation();
            db.To = to;
            db.From = from;
            db.EasingFunction = easing;
            db.Duration = duration;
            Storyboard.SetTarget(db, target);
            Storyboard.SetTargetProperty(db, targetProperty);
            return db;
        }
    }
}
