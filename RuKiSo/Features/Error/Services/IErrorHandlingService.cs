namespace RuKiSo.Features.Services
{
    public interface IErrorHandlingService
    {
        void ShowError(string message);
        void CloseError();
    }
}
