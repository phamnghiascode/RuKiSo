using CommunityToolkit.Maui.Views;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IGenericService<BatchResponse, BatchRequest> batchService;
        private Popup? currentPopup;

        public ReminderService(IGenericService<BatchResponse, BatchRequest> batchService)
        {
            this.batchService = batchService;
        }

        public async Task<List<BatchResponse>> GetDueBatchesAsync() 
        {
            var allBatches = await batchService.GetAllAsync();
            var today = DateTime.Today;
            var threeDaysAgo = today.AddDays(-365);
            var result = allBatches
            .Where(b => b.Yield == 0)
            .Where(b =>
                b.EstimateEndDate.Date >= threeDaysAgo &&
                b.EstimateEndDate.Date <= today
            )
            .OrderBy(b => b.EstimateEndDate)
            .ToList() ?? new List<BatchResponse>();

            return result;
        }

        public void ShowPopup(Popup popup)
        {
            if (Application.Current?.MainPage == null) return;

            currentPopup = popup;
            Application.Current.MainPage.ShowPopup(popup);
        }

        public void ClosePopup(Popup popup)
        {
            if (currentPopup == popup)
            {
                popup.Close();
                currentPopup = null;
            }
        }
    }
}
