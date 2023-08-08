namespace Recipi_PWA.Models.Settings
{
    public interface SettingOption
    {
        public string label { get; set; }
        public T value { get; set; }
        public List<SettingOption>? children { get; set; }
    }
}
