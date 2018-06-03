using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace FiapControleFinanceiro.UWP.Services
{
    public static class NotificationService
    {
        public static void SendNotification(string title, string content, string tag, string group, int days)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },
                        new AdaptiveText()
                        {
                            Text = content
                        }
                    }
                }
            };

            ToastActionsCustom actions = new ToastActionsCustom();

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions
            };

            var toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddDays(days);
            toast.Tag = tag;
            toast.Group = group;

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
