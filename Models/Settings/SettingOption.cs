namespace Recipi_PWA.Models.Settings
{
    public interface SettingOption<T>
    {
        public string label { get; set; }
        public T value { get; set; }
    }
}
