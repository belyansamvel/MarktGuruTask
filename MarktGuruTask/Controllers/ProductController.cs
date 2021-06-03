using MarktGuruTask.Entities;
using MarktGuruTask.Models;
using MarktGuruTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarktGuruTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts([FromQuery] PaginationModel model)
        {
            var products = await _productService.GetProducts(model.Offset, model.Count);

            if (products == null || !products.Any())
            {
                return NotFound("There are no products yet");
            }

            var list = products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Available,
                p.Price
            }).ToList();

            return Ok(list);// new JsonResult(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var entity = new Product
            {
                Available = model.Available,
                Description = model.Description,
                Name = model.Name,
                Price = model.Price
            };

            try
            {
                var result = await _productService.Add(entity);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetails(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be less than 1");
            }

            var product = await _productService.GetProductDetails(id);
            if (product == null)
            {
                return NotFound("Product with the given Id does not exist");
            }

            return new JsonResult(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var entity = new Product
            {
                Available = model.Available,
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                DateCreated = model.DateCreated
            };

            try
            {
                var result = await _productService.UpdateProduct(entity);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be less than 1");
            }

            try
            {
                await _productService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}
