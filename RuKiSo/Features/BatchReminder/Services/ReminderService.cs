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
            //return allBatches?
            //    .Where(b => {
            //        var daysRemaining = (b.EstimateEndDate.Date - DateTime.Today).Days;
            //        //return daysRemaining is >= 0 and <= 3;
            //        return daysRemaining;
            //    })
            //    .ToList() ?? new List<BatchResponse>();
            return allBatches.ToList() ?? new List<BatchResponse>();
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
