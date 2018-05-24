using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ayrbox.winservice.Core {
    public interface IDataContext {
        void Execute(Action<IDbConnection> action);
        T Get<T>(Func<IDbConnection, T> func) where T : class;
    }
}
