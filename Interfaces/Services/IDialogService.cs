using Core;
using Interfaces.Models;

namespace Interfaces.Services
{
    public interface IDialogService
    {
        void ShowError(string message);
        void OpenWindow(IEntity entity, WindowType windowsType, IBLC blc, IDialogService dialogService);
        void CloseWindow(string quid);
    }
}
