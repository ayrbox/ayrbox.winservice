using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ayrbox.winservice.Core {
    public abstract class BaseDataContext {

        private readonly string _connectionString = "";

        public BaseDataContext(string connectionString) {
            _connectionString = connectionString;
        }

        public void Execute(Action<IDbConnection> action) {
            using (var _conn = new SqlConnection(_connectionString)) {
                action.Invoke(_conn);
            }
        }

        public T Get<T>(Func<IDbConnection, T> func) where T : class {
            using (var _conn = new SqlConnection(_connectionString)) {
                return func.Invoke(_conn);
            }
        }
    }
}
