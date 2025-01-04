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

        public ObservableCollection<BatchResponse> Batches { get; }
        public ICommand CloseCommand { get; }
        public ICommand OpenCommand { get; }

        public BatchReminderViewModel(IReminderService reminderService)
        {
            _reminderService = reminderService;
            Batches = new ObservableCollection<BatchResponse>();

            CloseCommand = new RelayCommand(ClosePopupAsync);
            OpenCommand = new RelayCommand(LoadAndShowPopupAsync);

            // Set up timer to check batches every hour
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromHours(1);
            _timer.Tick += async (s, e) => await CheckBatches();
            _timer.Start();

            // Initial check
            CheckBatches().ConfigureAwait(false);
        }

        private async void LoadAndShowPopupAsync()
        {
            await LoadAllBatches();
            var popup = new BatchReminder(this);
            _reminderService.ShowPopup(popup);
        }

        private void ClosePopupAsync()
        {
            if (Application.Current?.MainPage?.Handler?.MauiContext != null)
            {
                return;
            }
        }

        private async Task CheckBatches()
        {
            await LoadAllBatches();

            foreach (var batch in Batches)
            {
                var daysRemaining = (batch.EstimateEndDate - DateTime.Now).Days;
                if (daysRemaining is 3 or 2 or 1)
                {
                    var reminderPopup = new BatchReminder(this);
                    _reminderService.ShowPopup(reminderPopup);
                    break;
                }
            }
        }

        private async Task LoadAllBatches()
        {
            try
            {
                var dueBatches = await _reminderService.GetDueBatchesAsync();
                Batches.Clear();
                foreach (var batch in dueBatches)
                {
                    Batches.Add(batch);
                }
            }
            catch (Exception ex)
            {
                HandleException("Error loading batches", ex);
            }
        }
    }
}
