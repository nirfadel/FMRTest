using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fmr.Core.Model;
using Fmr.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FmrExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksRepository _stocksRepository;
        public StocksController(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        [HttpGet("{id}")]
        public async Task<StockItem> Get(int id)
        {
            return await _stocksRepository.GetStockById(id);
        }

        [HttpGet("/search/{term}")]
        public async Task<List<StockItem>> Get([FromQuery]string term, [FromBody] object param)
        {
            FilterStock filterStock = new FilterStock() { Name = term, param = param };
            return await _stocksRepository.GetStockByNameFilter(filterStock);
        }

        [HttpGet("/search/{range}")]
        public async Task<List<StockItem>> Get([FromQuery] string range)
        {
            string[] ranges = range.Split(',');
            return await _stocksRepository.GetStockByPriceRange(Convert.ToDouble(ranges[0]), Convert.ToDouble(ranges[1]));
        }


    }
}
