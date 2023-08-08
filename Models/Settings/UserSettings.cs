
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Recipi_PWA.Models.Settings
{
    public class UserSettings : INotifyPropertyChanged
    {
        private StringSetting changeProfile;
        private StringSetting changePassword;
        private BoolSetting anonymousUsername;
        private InnerSettingsGroup notifications;


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

        public BoolSetting AnonymousUsername 
        { 
            get { return anonymousUsername; } 
            set
            {
                anonymousUsername = value;
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