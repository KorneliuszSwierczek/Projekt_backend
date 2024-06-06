using SoapService.DataContract;
using System.ServiceModel;

namespace ProjektAPI1.ServiceContract
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string RegisterUser(User user);
    }
}
