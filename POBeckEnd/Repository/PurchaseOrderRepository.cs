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

        public async Task<APIResponse> InsertPurchaseOrder(TblPoHeader poItems)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    var itemPoHeader = new TblPoHeader
                    {
                        NoPo = Commons.GeneratePO(),
                        AlamatPerusahaan = poItems.AlamatPerusahaan,
                        CatatanTambahan = poItems.CatatanTambahan,
                        CreatedBy = poItems.CreatedBy,
                        CreatedDate = poItems.CreatedDate,
                        Diskon = poItems.Diskon,
                        Email = poItems.Email,
                        Nama = poItems.Nama,
                        Jabatan = poItems.Jabatan,
                        NoTelp = poItems.NoTelp,
                        PerusahaanTujuan = poItems.PerusahaanTujuan,
                        Website = poItems.Website,
                        Ppn = poItems.Ppn,
                        Tanggal = poItems.Tanggal,
                        Total = poItems.Total,
                        Subtotal = poItems.Subtotal,
                        UpdatedBy = poItems.UpdatedBy,
                        UpdatedDate = poItems.UpdatedDate,
                        IsDeleted = false
                    };                    

                    await _dbContext.TblPoHeader.AddAsync(poItems);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new APIResponse(200, "Success", $"PO Created - {poItems.NoPo}", poItems);
                }
            }
            catch (Exception ex)
            {
                return new APIResponse(500, "Failed", ex.Message, null);
            }
        }

    }
}
