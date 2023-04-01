using POBeckEnd.Models;

namespace POBeckEnd.ViewModels
{
    public class PurchaseOrderViewModels
    {
        public TblPoCustomer PelangganModels { get; set; }
        public TblPoHeader POHeaders { get; set; }
        public TblPoDetail PODetails { get; set; }
        public TblPoShipment POShipments { get; set; }
        public TblPoVendor POVendor { get; set; }
    }
}
