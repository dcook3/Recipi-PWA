using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Recipi_API.Models;
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
            _setting = JsonSerializer.Deserialize<Setting>(await jsr.InvokeAsync<string?>("localStorage.getItem", "settings").ConfigureAwait(false) ?? "{}");
            Loaded = true;
        }

        public async Task<string> GetToken()
        {
            return await jsr.InvokeAsync<string>("localStorage.getItem", "token").ConfigureAwait(false);
        }

        public async Task<string> GetSetting()
        {
            return await jsr.InvokeAsync<string>("localStorage.getItem", "settings").ConfigureAwait(false);
        }

        public async Task SetSetting(Setting setting)
        {
            await jsr.InvokeVoidAsync("localStorage.setItem", "settings", JsonSerializer.Serialize(setting)).ConfigureAwait(false);
            _setting = setting;
            NotifyStateChanged();
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

        private Setting _setting;
        


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

        


                
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
