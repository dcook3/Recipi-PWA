using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Recipi_API.Models;
using Recipi_PWA.Models.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Recipi_PWA.Models;

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
            //_settings = JsonSerializer.Deserialize<UserSettings>(await jsr.InvokeAsync<string?>("localStorage.getItem", "settings").ConfigureAwait(false) ?? "{}");
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

        private UserSettings _settings;
        private bool _initialized;

        public event EventHandler Changed;

        public bool AutoSave { get; set; } = true;

        public async ValueTask<UserSettings> GetSetting()
        {
            Console.WriteLine("Getting settings");
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
            UserSettings result;

            UserSettings defaultSettings = new();
            defaultSettings = new UserSettings();
            defaultSettings.AnonymousUsername = new("Use Anonymous Username", false);
            defaultSettings.ChangeProfile = new("Change Profile", "https://recipiapp.com/profile/edit");
            defaultSettings.ChangePassword = new("Change Password", "https://recipiapp.com/login/change-password");
            defaultSettings.Notifications = new();
            defaultSettings.Notifications.boolSettings = new()
            {
                new BoolSetting("Offers Notifications", true),
                new BoolSetting("Messages Notifications", true),
                new BoolSetting("Friend Request Notifications", true)
            };

            var str = await jsr.InvokeAsync<string>("localStorage.getItem", "settings");
            if (str != null)
            {
                result = JsonSerializer.Deserialize<UserSettings>(str) ?? defaultSettings;
            }
            else
            {
                result = defaultSettings;
            }

            // Register the OnPropertyChanged event, so it automatically persists the settings as soon as a value is changed
            result.PropertyChanged += OnPropertyChanged;
            _settings = result;
            return result;
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("On property changed");
            if (AutoSave)
            {
                await SaveSetting();
            }
        }

        // This method is called from BlazorRegisterStorageEvent when the storage changed
        [JSInvokable]
        public void OnStorageUpdated(string key)
        {
            if (key == "settings")
            {
                // Reset the settings. The next call to Get will reload the data
                _settings = null;
                Changed?.Invoke(this, EventArgs.Empty);
            }
            else if(key == "recipe")
            {
                currentRecipe = null;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task SaveSetting()
        {
            Console.WriteLine("Saving settings");
            var json = JsonSerializer.Serialize(_settings);
            await jsr.InvokeVoidAsync("localStorage.setItem", "settings", json).ConfigureAwait(false);
            NotifyStateChanged();
        }

        public Recipe currentRecipe;

        public async Task<Recipe> GetRecipe()
        {
            if (currentRecipe != null)
                return currentRecipe;

            if (!_initialized)
            {
                // Create a reference to the current object, so the JS function can call the public method "OnStorageUpdated"
                var reference = DotNetObjectReference.Create(this);
                await jsr.InvokeVoidAsync("BlazorRegisterStorageEvent", reference);
                _initialized = true;
            }

            // Read the JSON string that contains the data from the local storage
            Recipe result;

            Recipe placeholderRecipe = new Recipe();
            placeholderRecipe.recipeTitle = "Recipe was not loaded properly.";

            var str = await jsr.InvokeAsync<string>("sessionStorage.getItem", "recipe");
            if (str != null)
            {
                result = JsonSerializer.Deserialize<Recipe>(str) ?? placeholderRecipe;
            }
            else
            {
                result = placeholderRecipe;
            }

            result.PropertyChanged += OnPropertyChanged;
            currentRecipe = result;
            return result;
        }

        public async Task SaveRecipe()
        {
            Console.WriteLine("Saving recipe");
            var json = JsonSerializer.Serialize(currentRecipe);
            await jsr.InvokeVoidAsync("sessionStorage.setItem", "recipe", json).ConfigureAwait(false);
            NotifyStateChanged();
        }

        public async Task SaveNewRecipe(Recipe recipe)
        {
            if (!recipe.IsStateless)
            {
                Console.WriteLine("Saving a new recipe");
                var json = JsonSerializer.Serialize(recipe);
                await jsr.InvokeVoidAsync("sessionStorage.setItem", "recipe", json).ConfigureAwait(false);
                NotifyStateChanged();
            }
        }
                
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
