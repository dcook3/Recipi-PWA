using Foodi_Application_NET_7._0.Pages;

namespace Foodi_Application_NET_7.Models
{
    [Serializable]
    public class Setting
    {
        public SettingOption changeProfile { get; set; } = new SettingOption("Change Profile","https://recipiapp.com/profile/edit", "string");
        public SettingOption changePassword { get; set; } = new SettingOption("Change Password", "https://recipiapp.com/auth/changePassword", "string");
        public SettingOption anonymousUsername { get; set; } = new SettingOption("Show Anonymous Username", "false", "boolean");

        public List<SettingOption> notifications { get; set; } = new List<SettingOption> {
            new SettingOption("Push Notifications", "true", "boolean"),
            new SettingOption("New Offer Notifications", "true", "boolean")
        };
    }
}