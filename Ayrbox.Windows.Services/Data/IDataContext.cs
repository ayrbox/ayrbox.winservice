using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ayrbox.Windows.Services.Data {
    public interface IDataContext {
        void Execute(Action<IDbConnection> action);
        T Get<T>(Func<IDbConnection, T> func) where T : class;
    }
}
