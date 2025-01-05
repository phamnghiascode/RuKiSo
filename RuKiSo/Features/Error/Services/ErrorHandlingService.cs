using CommunityToolkit.Maui.Views;
using RuKiSo.UI.Views;
using RuKiSo.ViewModels;

namespace RuKiSo.Features.Services
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private Popup? currentErrorPopup;

        public void ShowError(string message)
        {
            if (Application.Current?.MainPage == null) return;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                var errorPopup = new ErrorPopup
                {
                    BindingContext = new ErrorPopupViewModel { ErrorMessage = message }
                };
                currentErrorPopup = errorPopup;
                Application.Current.MainPage.ShowPopup(errorPopup);
            });
        }

        public void CloseError()
        {
            if (currentErrorPopup != null)
            {
                currentErrorPopup.Close();
                currentErrorPopup = null;
            }
        }
    }
}
