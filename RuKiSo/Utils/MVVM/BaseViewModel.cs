using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RuKiSo.Utils.MVVM
{
    public abstract partial class BaseViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        public virtual Task OnAppearingAsync()
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name}.{nameof(OnAppearingAsync)}");

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
            Console.WriteLine($"{message}: {ex.Message}");
        }
    }
}
