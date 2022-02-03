using SyncApp.Models.ViewModels;
using System.Threading.Tasks;

namespace SyncApp.Services
{
    public interface ILoginService
    {
        Task<string> Login(LoginViewModel model);
    }
}
