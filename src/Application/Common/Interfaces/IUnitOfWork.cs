using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopupPortal.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
    }
}
