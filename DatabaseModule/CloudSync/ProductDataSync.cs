using DatabaseModule.Master.ProductSetup;
using System.Collections.Generic;

namespace DatabaseModule.CloudSync;

public class ProductDataSync : BaseSyncData
{
    public IList<Product> Products { get; set; }
    public IList<ProductUnit> Units { get; set; }
    public IList<ProductGroup> ProductGroups { get; set; }
    public IList<ProductSubGroup> SubGroups { get; set; }
}