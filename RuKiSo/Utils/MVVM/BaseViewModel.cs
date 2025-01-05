using RuKiSo.Features.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RuKiSo.Utils.MVVM
{
    public abstract partial class BaseViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        protected readonly IErrorHandlingService _errorHandlingService;

        protected BaseViewModel(IErrorHandlingService errorHandlingService)
        {
            _errorHandlingService = errorHandlingService;
        }

        public virtual async Task OnAppearingAsync()
        {
            await LoadDataAsync();
        }

        protected virtual Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearingAsync()
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name}.{nameof(OnDisappearingAsync)}");
            return Task.CompletedTask;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void HandleException(string message, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"{message}: {ex}");
            _errorHandlingService.ShowError($"{message}: {ex.Message}");
        }
    }
}
