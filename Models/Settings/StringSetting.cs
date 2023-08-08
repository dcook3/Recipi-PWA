namespace Recipi_PWA.Models.Settings
{
    [Serializable]
    public class StringSetting : SettingOption<string>
    {
        public string value { get; set; }
        public string label { get; set; }
        public List<SettingOption<string>>? children { get; set; } = new();

        public StringSetting(string label, string value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
