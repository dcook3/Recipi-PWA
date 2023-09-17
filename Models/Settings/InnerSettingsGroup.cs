namespace Recipi_PWA.Models.Settings
{
    public class InnerSettingsGroup
    {
        public string name { get; set; }
        public List<BoolSetting>? boolSettings { get; set; }
        public List<StringSetting>? stringSettings { get; set; }
        public List<NumberSetting>? numberSettings { get; set; }
        public List<OptionSetting>? optionSettings { get; set; }
        public InnerSettingsGroup? inner { get; set; }
        public InnerSettingsGroup() 
        {
            boolSettings = new List<BoolSetting>();
            stringSettings = new List<StringSetting>();
            numberSettings = new List<NumberSetting>();
            optionSettings = new List<OptionSetting>();
        }
    }
}
