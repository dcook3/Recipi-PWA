using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Recipi_API.Models;
using Recipi_PWA.Models.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Recipi_PWA.Models
{
    public class StateContainer
    {
        private readonly IJSRuntime jsr;

        public StateContainer(IJSRuntime _jsr)
        {
            jsr = _jsr;
        }

        public bool Loaded = false;
        
        public async Task LoadLocalStorage()
        {
            _token = (await jsr.InvokeAsync<string?>("localStorage.getItem", "token").ConfigureAwait(false)) ?? String.Empty;
            _you = JsonSerializer.Deserialize<You>(await jsr.InvokeAsync<string?>("localStorage.getItem", "you").ConfigureAwait(false) ?? "{}");
            _settings = JsonSerializer.Deserialize<Setting>(await jsr.InvokeAsync<string?>("localStorage.getItem", "settings").ConfigureAwait(false) ?? "{}");
            Loaded = true;
        }

        public async Task<string> GetToken()
        {
            return await jsr.InvokeAsync<string>("localStorage.getItem", "token").ConfigureAwait(false);
        }

        

        public async Task SetToken(string token)
        {
            await jsr.InvokeVoidAsync("localStorage.setItem", "token", token).ConfigureAwait(false);
            _token = token;
            NotifyStateChanged();
        }

        public async Task Logout()
        {
            await jsr.InvokeVoidAsync("localStorage.removeItem", "token");
            await jsr.InvokeVoidAsync("localStorage.removeItem", "you");
            _token = "";
            _you = null;
        }

        private string _token;
        public string Token
        {
            get => _token ?? string.Empty;
        }

        public bool LoggedIn => !String.IsNullOrEmpty(_token);


        public async Task<You?> GetYou()
        {
            return JsonSerializer.Deserialize<You>(await jsr.InvokeAsync<string?>("localStorage.getItem", "you").ConfigureAwait(false) ?? String.Empty);
        }
        public async Task SetYou(You? you)
        {
            
            await jsr.InvokeVoidAsync("localStorage.setItem", "you", JsonSerializer.Serialize(you)).ConfigureAwait(false);
            _you = you;
            NotifyStateChanged();
        }

        private You? _you;
        public You? You
        {
            get => _you ?? null;
        }

        private const string KeyName = "state";
        private Setting _settings;
        private bool _initialized;

        public event EventHandler Changed;

        public bool AutoSave { get; set; } = true;

        public async ValueTask<Setting> GetSetting()
        {
            if (_settings != null)
                return _settings;

            if (!_initialized)
            {
                // Create a reference to the current object, so the JS function can call the public method "OnStorageUpdated"
                var reference = DotNetObjectReference.Create(this);
                await jsr.InvokeVoidAsync("BlazorRegisterStorageEvent", reference);
                _initialized = true;
            }

            // Read the JSON string that contains the data from the local storage
            Setting result;
            var str = await jsr.InvokeAsync<string>("BlazorGetLocalStorage", KeyName);
            if (str != null)
            {
                result = System.Text.Json.JsonSerializer.Deserialize<Setting>(str) ?? new Setting();
            }
            else
            {
                result = new Setting();
            }

            // Register the OnPropertyChanged event, so it automatically persists the settings as soon as a value is changed
            result.PropertyChanged += OnPropertyChanged;
            _settings = result;
            return result;
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AutoSave)
            {
                await SaveSetting();
            }
        }

        // This method is called from BlazorRegisterStorageEvent when the storage changed
        [JSInvokable]
        public void OnStorageUpdated(string key)
        {
            if (key == KeyName)
            {
                // Reset the settings. The next call to Get will reload the data
                _settings = null;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task SaveSetting()
        {
            var json = JsonSerializer.Serialize(_settings);
            await jsr.InvokeVoidAsync("localStorage.setItem", "settings", JsonSerializer.Serialize(_settings)).ConfigureAwait(false);
            NotifyStateChanged();
        }
                
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
