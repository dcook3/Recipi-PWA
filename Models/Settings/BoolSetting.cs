namespace Recipi_PWA.Models.Settings
{
    public class BoolSetting : SettingOption<bool>
    {
        public bool value { get; set; }
        public string label { get; set; }
        public List<SettingOption<bool>>? children { get; set; } = new();

        public BoolSetting(string label, bool value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
