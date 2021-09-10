using BaseFacware.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseFacware.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private List<Items> GetItems() 
        {
            var list = new List<Items>();
            list.Add(new Items() { Id = 1, Name = "value1" });
            list.Add(new Items() { Id = 2, Name = "value2" });
            return list;
        }

        /// <summary>
        /// Get items
        /// </summary>
        /// <returns>list of items</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await Task.Run(() => GetItems());
            return Ok(list);
            
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <returns>list of items</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var item = await Task.Run(() => GetItems().Where(x => x.Id.Equals(id)).FirstOrDefault());
            return Ok(item);
        }


        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Items value)
        {
            return RedirectToAction(nameof(GetById), new { id = value.Id });
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Items value)
        {
            return RedirectToAction(nameof(GetById), new { Id = id });
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
