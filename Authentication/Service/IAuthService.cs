using System.Threading.Tasks;

namespace Authentication.Service
{
    public interface IAuthService
    {
        Task<string> Login();
    }
}