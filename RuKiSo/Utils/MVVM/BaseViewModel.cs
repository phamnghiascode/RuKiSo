using CommunityToolkit.Maui.Core;
using RuKiSo.UI.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RuKiSo.Utils.MVVM
{
    public abstract partial class BaseViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        private readonly IPopupService popupService;
        private int notiCount;

        public int NotiCount
        {
            get { return notiCount; }
            set { notiCount = value;
                OnPropertyChanged(nameof(NotiCount));
            }
        }

        public ICommand OpenNotiPopup { get; set; }
        public BaseViewModel(IPopupService popupService)
        {
            this.popupService = popupService;
            OpenNotiPopup = new Command(OpenNoti);
        }
        private void OpenNoti(object obj)
        {
            this.popupService.ShowPopup<NotiPopup>();
        }

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
    }
}
