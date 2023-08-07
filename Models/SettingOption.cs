using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Recipi_PWA.Models
{
    public class SettingOption
    {
        public string label { get; set; }
        public string? value { get; set; }
        public string type { get; set; }
        public List<SettingOption>? children { get; set; }
        public SettingOption(string label, string? value, string type)
        {
            this.label = label;
            if(value != null) 
            { 
                this.value = value;
            }
        }
    }
}
