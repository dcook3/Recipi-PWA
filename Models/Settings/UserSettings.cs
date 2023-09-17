
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Recipi_PWA.Models.Settings
{
    public class UserSettings : INotifyPropertyChanged
    {
        private StringSetting changeProfile;
        private StringSetting changePassword;
        private InnerSettingsGroup measurementUnits;
        private InnerSettingsGroup notifications;

        public int UserId { get; set; }


        public StringSetting ChangeProfile 
        {
            get { return changeProfile; } 
            set
            {
                changeProfile = value;
                RaisePropertyChanged();
            }
        }
        public StringSetting ChangePassword 
        {
            get { return changePassword; } 
            set
            {
                changePassword = value;
                RaisePropertyChanged();
            }
        }

        public InnerSettingsGroup MeasurementUnits 
        { 
            get { return measurementUnits; }
            set
            {
                measurementUnits = value;
            }
        }

        public InnerSettingsGroup Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}