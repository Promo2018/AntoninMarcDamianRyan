using System.ServiceModel;

namespace BoVoyage_Contract
{
    [ServiceContract]
    public interface IBVAuthorization
    {
        [OperationContract]
        int login(string username, string password);
    }
}
