using CommunityToolkit.Maui.Views;
using RuKiSo.Features.Models;

namespace RuKiSo.Features.Services
{
    public interface IReminderService
    {
        Task<List<BatchResponse>> GetDueBatchesAsync();
        void ShowPopup(Popup popup);
        void ClosePopup(Popup popup);
    }
}
