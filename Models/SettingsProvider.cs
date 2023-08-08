using Microsoft.JSInterop;
using Recipi_PWA.Models.Settings;
using System.ComponentModel;

namespace Recipi_PWA.Models
{
    public class SettingsProvider
    {
        private const string KeyName = "state";

        private readonly IJSRuntime _runtime;
        private bool _initialized;
        private Setting _settings;

        public event EventHandler Changed;

        public bool AutoSave { get; set; } = true;
        public SettingsProvider(IJSRuntime runtime)
        {
            _runtime = runtime;
        }

        public async ValueTask<Setting> Get()
        {
            if (_settings != null)
                return _settings;

            if (!_initialized)
            {
                var reference = DotNetObjectReference.Create(this);
                await _runtime.InvokeVoidAsync("BlazorRegisterStorageEvent", reference);
                _initialized = true;
            }

            Setting result;
            var str = await _runtime.InvokeAsync<string>("BlazorGetLocalStorage", KeyName);
            if (str != null)
            {
                result = System.Text.Json.JsonSerializer.Deserialize<Setting>(str) ?? new Setting();
            }
            else
            {
                result = new Setting();
            }

            result.PropertyChanged += OnPropertyChanged;
            _settings = result;
            return result;
        }

        public async Task Save()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(_settings);
            await _runtime.InvokeVoidAsync("BlazorSetLocalStorage", KeyName, json);
        }

        // Automatically persist the settings when a property changed
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AutoSave)
            {
                await Save();
            }
        }
    }
}
