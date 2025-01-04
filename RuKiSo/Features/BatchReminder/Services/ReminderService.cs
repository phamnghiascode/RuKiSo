using CommunityToolkit.Maui.Views;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IGenericService<BatchResponse, BatchRequest> batchService;
        public ReminderService(IGenericService<BatchResponse, BatchRequest> batchService)
        {
            this.batchService = batchService;
        }
        public async Task<List<BatchResponse>> GetDueBatchesAsync()
        {
            var allBatches = await batchService.GetAllAsync();
            return allBatches?
                .Where(b => b.EstimateEndDate.Date <= DateTime.Today)
                .ToList() ?? new List<BatchResponse>();
        }
        public void ShowPopup(Popup popup)
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            page.ShowPopup(popup);
        }
    }
}
