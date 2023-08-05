using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Foodi_Application_NET_7.Models
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
