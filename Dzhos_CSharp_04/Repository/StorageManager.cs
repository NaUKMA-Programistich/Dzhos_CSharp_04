using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dzhos_CSharp_04.Repository
{
    internal class StorageManager
    {
        #region Fields
        private static Storage _storage;
        #endregion

        #region Properties
        internal static Storage Storage
        {
            get { return _storage; }
        }
        #endregion

        #region Constructor;
        internal static void Instance(Storage storage)
        {
            _storage = storage;
        }
        #endregion
    }
}
