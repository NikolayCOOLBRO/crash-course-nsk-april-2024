using Market.DAL;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Enums;
using Market.Models;
using Market.UseCases.Products;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("v1/products")]
public sealed class ProductsController : ControllerBase
{
    public ProductsController()
    {
        ProductsRepository = new ProductsRepository();
    }

    private ProductsRepository ProductsRepository { get; }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid productId)
    {
        var productResult = await ProductsRepository.GetProductAsync(productId);
        return DbResultIsSuccessful(productResult, out var error)
            ? new JsonResult(productResult.Result)
            : error;
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchProductsAsync(
        [FromBody] SearchProductDto searchProductDTO)
    {
        if (searchProductDTO == null)
        {
            return BadRequest();
        }

        var result = await ProductsRepository.GetProductsAsync(null,
                                                                productName: searchProductDTO.ProductName,
                                                                searchProductDTO.Category,
                                                                skip: searchProductDTO.Skip,
                                                                take: searchProductDTO.Take);
        if (!DbResultIsSuccessful(result, out var error))
            return error;

        IEnumerable<Product> productResult = result.Result;

        if (searchProductDTO.SortType != null)
        {
            var sorter = new ProductSorter();
            productResult = sorter.Sort(productResult, searchProductDTO.SortType, searchProductDTO.Ascending);
        }
        
        return new JsonResult(productResult.Select(ProductDto.FromModel));
    }

    [HttpGet]
    public async Task<IActionResult> GetSellerProductsAsync(
        [FromQuery] Guid sellerId,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        var productsResult = await ProductsRepository.GetProductsAsync(sellerId: sellerId, skip: skip, take: take);
        if (!DbResultIsSuccessful(productsResult, out var error))
            return error;

        var productDtos = productsResult.Result.Select(ProductDto.FromModel);
        return new JsonResult(productDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
    {
        var createResult = await ProductsRepository.CreateProductAsync(product);

        return DbResultIsSuccessful(createResult, out var error)
            ? new StatusCodeResult(StatusCodes.Status201Created)
            : error;
    }

    [HttpPut("{productId:Guid}")]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid productId, [FromBody] UpdateProductRequestDto requestInfo)
    {
        var updateResult = await ProductsRepository.UpdateProductAsync(productId, new ProductUpdateInfo
        {
            Name = requestInfo.Name,
            Description = requestInfo.Description,
            Category = requestInfo.Category,
            PriceInRubles = requestInfo.PriceInRubles
        });

        return DbResultIsSuccessful(updateResult, out var error)
            ? new StatusCodeResult(StatusCodes.Status204NoContent)
            : error;
    }

    [HttpDelete("{productId:Guid}")]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId)
    {
        var deleteResult = await ProductsRepository.DeleteProductAsync(productId);

        return DbResultIsSuccessful(deleteResult, out var error)
            ? new StatusCodeResult(StatusCodes.Status204NoContent)
            : error;
    }

    private static bool DbResultIsSuccessful(DbResult dbResult, out IActionResult error) =>
        DbResultStatusIsSuccessful(dbResult.Status, out error);

    private static bool DbResultIsSuccessful<T>(DbResult<T> dbResult, out IActionResult error) =>
        DbResultStatusIsSuccessful(dbResult.Status, out error);

    private static bool DbResultStatusIsSuccessful(DbResultStatus status, out IActionResult error)
    {
        error = null!;
        switch (status)
        {
            case DbResultStatus.Ok:
                return true;
            case DbResultStatus.NotFound:
                error = new StatusCodeResult(StatusCodes.Status404NotFound);
                return false;
            default:
                error = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return false;
        }
    }
}