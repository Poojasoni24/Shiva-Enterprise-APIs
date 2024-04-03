using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model;
using Shiva_Enterprise_APIs.Model.Purchase;

namespace Shiva_Enterprise_APIs.Helper
{
    public class SalesOrderDetailMappingHelper
    {
        public static SalesOrderDetail MapToEntity(SalesOrderDetailModel item)
        {
            return new SalesOrderDetail()
            {
                SalesOrderId = item.SalesOrderId,
                ProductId = item.ProductId,
                BrandId = item.BrandId,
                Quantity = item.Quantity,
                Discount = item.Discount,
                UnitPrice = item.UnitPrice,
                NetTotal = item.NetTotal,
                Tax_Percentage = item.Tax_Percentage,
                IsActive = item.IsActive,
                CreatedBy = item.CreatedBy,
                CreatedDateTime = item.CreatedDateTime,
                ModifiedBy = item.ModifiedBy,
                ModifiedDateTime = item.ModifiedDateTime
            };
        }
    }
}
