﻿using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts
            ([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

            return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var procuct = await repo.GetById(id);
            if (procuct == null)
            {

                return NotFound();
            }
            return procuct;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);

            if(await repo.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new {id = product.Id}, product);
            }

            return BadRequest("Opps! there is problem to add product!");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id == id || !productExists(id)) return BadRequest("Product not found!");

            repo.Update(product);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Opps! there is problem to update product!");

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetById(id);

            if (product == null) return NotFound();

            repo.Remove(product);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Opps! there is problem to delete product!");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return Ok(await repo.ListAsync(spec));                
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet]
        private bool productExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
