using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopupPortal.Infrastructure
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// The connection string to use for connecting to Microsoft SQL Server.
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Creates a new instance of the underlying <see cref="IDbConnection"/>.
        /// </summary>
        IDbConnection Create();
    }
}
