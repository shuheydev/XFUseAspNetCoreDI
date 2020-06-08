using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using XFUseAspNetCoreDI.Services;

namespace XFUseAspNetCoreDI.iOS.Services
{
    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            DependencyService.Get<INotificationService>().ReceiveNotification(notification.Request.Content.Title, notification.Request.Content.Body);

            completionHandler(UNNotificationPresentationOptions.Alert);
        }
    }
}