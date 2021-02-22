using NcrTestSub.Bo.Bo;
using NcrTestSub.Bo.IBo;

namespace NcrTestSub.Bo.Factory
{
    /// <summary>
    /// Factory class that create all business class objects
    /// </summary>
    internal sealed class FactoryBoProduction : FactoryBo
    {
        #region Interface Methods

        public override ICommandBo CreateInstanceCommandBo => CommandBo.GetInstance;


        #endregion
    }
}
