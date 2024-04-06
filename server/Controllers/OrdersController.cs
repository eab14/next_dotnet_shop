using server.Models;
using server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrdersService _ordersService;
    private readonly ProductsService _productsService;

    public OrdersController(OrdersService ordersService, ProductsService productsService)
    {
        _ordersService = ordersService;
        _productsService = productsService;
    }
        


    [HttpGet]
    public async Task<List<Order>> Get() =>
        await _ordersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Order>> Get(string id)
    {
        var order = await _ordersService.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        else
        {

            var products = new List<Product>();

            if (order.ProductsArray != null)
            {

                foreach (var productId in order.ProductsArray)
                {

                    var product = await _productsService.GetAsync(productId);
                    if (product != null)
                    {
                        products.Add(product);
                    }

                }

                order.Products = products;

            }

        }

        return order;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Order newOrder)
    {
        await _ordersService.CreateAsync(newOrder);

        return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Order updatedOrder)
    {
        var order = await _ordersService.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        updatedOrder.Id = order.Id;

        await _ordersService.UpdateAsync(id, updatedOrder);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var order = await _ordersService.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        await _ordersService.RemoveAsync(id);

        return NoContent();
    }

}
