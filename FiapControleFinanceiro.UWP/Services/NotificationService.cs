using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace FiapControleFinanceiro.UWP.Services
{
    public static class NotificationService
    {
        public static void AgendarToastNotification(TimeSpan timeSpan)
        {
            XmlDocument toastXml = new XmlDocument();

            string toastXmlString =
                $@"<toast scenario='alarm'>
                <visual>
                    <binding template='ToastGeneric'>
                    <text>A hora do timer chegou!!!</text>
                    </binding>
                </visual>
            </toast>";

            toastXml.LoadXml(toastXmlString);

            var toastTime = DateTime.Now.Add(timeSpan);

            var toast = new ScheduledToastNotification(toastXml, toastTime);

            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
    }
}
