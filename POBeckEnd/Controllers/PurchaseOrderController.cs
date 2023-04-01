using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POBeckEnd.Models;
using POBeckEnd.Repository;
using POBeckEnd.ViewModels;

namespace POBeckEnd.Controllers
{
    [Route("api/po/")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly DbPOContext _dbPOContext;
        private readonly PurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderController(DbPOContext dbPOContext)
        {
            _dbPOContext = dbPOContext;
            _purchaseOrderRepository = new PurchaseOrderRepository(_dbPOContext);
        }

        [HttpPost("insert/po")]
        public async Task<IActionResult> InsertPO([FromBody] TblPoHeader poModels)
        {
            var d = await _purchaseOrderRepository.InsertPurchaseOrder(poModels);
            return Ok(d);
        }
    }
}
