using AutoMapper;
using POBeckEnd.Helpers;
using POBeckEnd.Models;
using POBeckEnd.ViewModels;

namespace POBeckEnd.Repository
{
    public class PurchaseOrderRepository
    {
        private DbPOContext _dbContext;

        public PurchaseOrderRepository(DbPOContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<APIResponse> InsertPurchaseOrder(PurchaseOrderViewModels poItems)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    var itemPoHeader = new TblPoHeader
                    {
                        Id = GenerateId(),
                        NoPo = Commons.GeneratePO(),
                        AlamatPerusahaan = poItems.POHeaders.AlamatPerusahaan,
                        CatatanTambahan = poItems.POHeaders.CatatanTambahan,
                        CreatedBy = poItems.POHeaders.CreatedBy,
                        CreatedDate = poItems.POHeaders.CreatedDate,
                        Diskon = poItems.POHeaders.Diskon,
                        Email = poItems.POHeaders.Email,
                        Nama = poItems.POHeaders.Nama,
                        Jabatan = poItems.POHeaders.Jabatan,
                        NoTelp = poItems.POHeaders.NoTelp,
                        PerusahaanTujuan = poItems.POHeaders.PerusahaanTujuan,
                        Website = poItems.POHeaders.Website,
                        Ppn = 11000,
                        Tanggal = poItems.POHeaders.Tanggal,
                        Total = poItems.PODetails.Total * poItems.PODetails.Qty,
                        Subtotal = poItems.POHeaders.Total * poItems.POHeaders.Ppn,
                        UpdatedBy = poItems.POHeaders.UpdatedBy,
                        UpdatedDate = poItems.POHeaders.UpdatedDate,
                        IsDeleted = false
                    };
                    poItems.PODetails.PoHeaderId = itemPoHeader.Id;
                    poItems.POVendor.PoHeaderId = itemPoHeader.Id;
                    poItems.POShipments.PoHeaderId = itemPoHeader.Id;
                    poItems.POVendor.PoHeaderId = itemPoHeader.Id;

                    poItems.PODetails.IsDeleted = false;
                    poItems.POVendor.IsDeleted = false;
                    poItems.POShipments.IsDeleted = false;
                    poItems.PelangganModels.IsDeleted = false;

                    await _dbContext.TblPoHeader.AddAsync(poItems.POHeaders);
                    await _dbContext.TblPoDetail.AddAsync(poItems.PODetails);
                    await _dbContext.TblPoVendor.AddAsync(poItems.POVendor);
                    await _dbContext.TblPoCustomer.AddAsync(poItems.PelangganModels);
                    await _dbContext.TblPoShipment.AddAsync(poItems.POShipments);

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new APIResponse(200, "Success", $"PO Created - {poItems.POHeaders.NoPo}", poItems);
                }
            }
            catch (Exception ex)
            {
                return new APIResponse(500, "Failed", ex.Message, null);
            }
        }

        public int GenerateId()
        {
            var id = 0;
            var dataExisting = _dbContext.TblPoHeader.ToList();
            if (dataExisting.Count > 0 || dataExisting is not null)
            {
                var findId = dataExisting.Max(i => i.Id);
                id = Convert.ToInt32(findId) + 1;
            }
            return id;
        }
    }
}
