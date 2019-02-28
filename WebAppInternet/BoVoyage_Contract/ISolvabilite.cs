using System.ServiceModel;

namespace BoVoyage_Contract
{
    [ServiceContract]
    public interface ISolvabilite
    {
        [OperationContract]
        bool isCBSolvable(string cb);
    }
}
