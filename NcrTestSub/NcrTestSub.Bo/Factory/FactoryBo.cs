using NcrTestSub.Bo.IBo;
using System;

namespace NcrTestSub.Bo.Factory
{
    /// <summary>
    /// Factory class that create all business class objects
    /// </summary>
    public abstract class FactoryBo
    {
        #region Singleton

        private static readonly Lazy<FactoryBo> instance = new Lazy<FactoryBo>(() => new FactoryBoProduction());

        public static FactoryBo GetInstance => instance.Value;

        #endregion

        #region abstract methods

        public abstract ICommandBo CreateInstanceCommandBo { get; }

        #endregion
    }
}
