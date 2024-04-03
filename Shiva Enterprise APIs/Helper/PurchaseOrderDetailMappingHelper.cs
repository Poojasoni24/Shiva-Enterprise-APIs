using Microsoft.EntityFrameworkCore.Storage;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model.Purchase;

namespace Shiva_Enterprise_APIs.Helper
{

    public static class PurchaseOrderDetailMappingHelper
    {
        public static PurchaseOrderDetail MapToEntity(PurchaseOrderDetailModel item)
        {
            return new PurchaseOrderDetail()
            {
                PurchaseOrderId = item.PurchaseOrderId,
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

