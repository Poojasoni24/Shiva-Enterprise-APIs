using Microsoft.EntityFrameworkCore.Storage;
using Shiva_Enterprise_APIs.Entities;

namespace Shiva_Enterprise_APIs.Helper
{
    public interface ICommonHelper
    {
        void RollbackTransaction(ShivaEnterpriseContext transaction);
    }
}
