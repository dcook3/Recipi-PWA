
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Recipi_PWA.Models.Settings
{
    [Serializable]
    public class Setting : INotifyPropertyChanged
    {
        public Setting(StringSetting changeProfile, StringSetting changePassword, BoolSetting anonymousUsername, List<SettingOption> notifications)
        {
            this.changeProfile = changeProfile;
            this.changePassword = changePassword;
            this.anonymousUsername = anonymousUsername;
            this.notifications = notifications;
        }

        public StringSetting changeProfile { get => changeProfile; set
            {
                changeProfile = value;
                RaisePropertyChanged();
            }
        }
        public StringSetting changePassword {
            get => changePassword; 
            set
            {
                changePassword = value;
                RaisePropertyChanged();
            }
        }

        public BoolSetting anonymousUsername { get; set; } 

        public List<SettingOption> notifications { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}