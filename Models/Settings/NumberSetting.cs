namespace Recipi_PWA.Models.Settings
{
    public class NumberSetting : SettingOption<int>
    {
        public int value { get; set; }
        public string label { get; set; }
        public List<SettingOption<int>>? children { get; set; } = new();

        public NumberSetting(string label, int value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
