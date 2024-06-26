﻿using server.Models;
using server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductsService _productsService;
    private readonly FeaturedProductsService _featuredProductsService;

    public ProductsController(ProductsService productsService, FeaturedProductsService featuredProductsService)
    {
        _productsService = productsService;
        _featuredProductsService = featuredProductsService;
    }

    [HttpGet]
    public async Task<List<Product>> Get() =>
        await _productsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
        var product = await _productsService.GetAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product newProduct)
    {
        await _productsService.CreateAsync(newProduct);

        return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Product updatedProduct)
    {
        var product = await _productsService.GetAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        updatedProduct.Id = product.Id;

        await _productsService.UpdateAsync(id, updatedProduct);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _productsService.GetAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        await _productsService.RemoveAsync(id);

        return NoContent();
    }

    [HttpGet("featured")]
    public async Task<ActionResult<List<FeaturedProduct>>> GetFeaturedProducts()
    {
        var featuredProducts = await _featuredProductsService.GetAsync();

        if (featuredProducts == null || featuredProducts.Count == 0)
        {
            return NotFound();
        }

        else
        {
            foreach (var featuredProduct in featuredProducts)
            {
                var product = await _productsService.GetAsync(featuredProduct.ProductId);
                featuredProduct.Product = product;
            }
        }

        return featuredProducts;
    }

}