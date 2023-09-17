namespace Recipi_PWA.Models.Settings
{
    public class OptionSetting : SettingOption<string>
    {
        public string value { get; set; }
        public string label { get; set; }
        public List<string>? options { get; set; } = new();

        public OptionSetting(string label, List<string> options)
        {
            this.label = label;
            this.options = options;
            this.value = options[0];
        }
    }
}
