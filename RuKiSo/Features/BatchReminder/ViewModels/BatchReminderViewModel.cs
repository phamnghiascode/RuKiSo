using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.UI.Views;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class BatchReminderViewModel : BaseViewModel
    {
        private readonly IReminderService _reminderService;
        private readonly IDispatcherTimer _timer;
        private Popup? _currentPopup;
        private bool _hasNotifications;
        public bool HasNotifications
        {
            get => _hasNotifications;
            set => SetProperty(ref _hasNotifications, value);
        }

        public ObservableCollection<BatchResponse> Batches { get; }
        public ICommand CloseCommand { get; }
        public ICommand OpenCommand { get; }

        public BatchReminderViewModel(IReminderService reminderService,
                                    IErrorHandlingService errorHandlingService) : base(errorHandlingService)
        {
            _reminderService = reminderService;
            Batches = new ObservableCollection<BatchResponse>();
            CloseCommand = new RelayCommand(ClosePopupAsync);
            OpenCommand = new RelayCommand(LoadAndShowPopupAsync);

            // Update timer to check more frequently (e.g., every 15 minutes)
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMinutes(15);
            _timer.Tick += async (s, e) => await CheckBatches();
            _timer.Start();

            // Initial check
            MainThread.BeginInvokeOnMainThread(async () => await CheckBatches());
        }

        private async Task CheckBatches()
        {
            try
            {
                var dueBatches = await _reminderService.GetDueBatchesAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Batches.Clear();
                    foreach (var batch in dueBatches)
                    {
                        Batches.Add(batch);
                    }

                    // Update HasNotifications based on batches
                    HasNotifications = Batches.Any();

                    // Force UI update
                    OnPropertyChanged(nameof(HasNotifications));
                });
            }
            catch (Exception ex)
            {
                HandleException("Error checking batches", ex);
            }
        }

        private async void LoadAndShowPopupAsync()
        {
            try
            {
                await LoadAllBatches();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _currentPopup = new BatchReminderPopup(this);
                    _reminderService.ShowPopup(_currentPopup);
                });
            }
            catch (Exception ex)
            {
                HandleException("Error loading popup", ex);
            }
        }

        private void ClosePopupAsync()
        {
            try
            {
                if (_currentPopup != null)
                {
                    _reminderService.ClosePopup(_currentPopup);
                    _currentPopup = null;
                }
            }
            catch (Exception ex)
            {
                HandleException("Error closing popup", ex);
            }
        }

        private async Task LoadAllBatches()
        {
            try
            {
                var dueBatches = await _reminderService.GetDueBatchesAsync();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Batches.Clear();
                    foreach (var batch in dueBatches)
                    {
                        Batches.Add(batch);
                    }

                    // Update HasNotifications whenever batches are loaded
                    HasNotifications = Batches.Any();
                    OnPropertyChanged(nameof(HasNotifications));
                });
            }
            catch (Exception ex)
            {
                HandleException("Error loading batches", ex);
            }
        }
    }
}
