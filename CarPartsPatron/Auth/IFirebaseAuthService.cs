using System.Threading.Tasks;
using CarPartsPatron.Auth.Models;

namespace CarPartsPatron.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}