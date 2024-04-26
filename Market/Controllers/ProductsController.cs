using Market.DAL;
using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Enums;
using Market.Middleware;
using Market.Misc;
using Market.Models;
using Market.Services.Comments;
using Market.UseCases.Products;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("v1/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductsRepository _productsRepository;
    private readonly ICommentsService _commentService;

    public ProductsController(IProductsRepository repository, ICommentsService commentsService)
    {
        _productsRepository = repository;
        _commentService = commentsService;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid productId)
    {
        var productResult = await _productsRepository.GetProductAsync(productId);
        return DbResultHelper.DbResultIsSuccessful(productResult, out var error)
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

        var result = await _productsRepository.GetProductsAsync(null,
                                                                productName: searchProductDTO.ProductName,
                                                                searchProductDTO.Category,
                                                                skip: searchProductDTO.Skip,
                                                                take: searchProductDTO.Take);
        if (!DbResultHelper.DbResultIsSuccessful(result, out var error))
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
    [ServiceFilter(typeof(CheckAuthFilter))]
    public async Task<IActionResult> GetSellerProductsAsync(
        [FromQuery] Guid sellerId,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        var productsResult = await _productsRepository.GetProductsAsync(sellerId: sellerId, skip: skip, take: take);
        if (!DbResultHelper.DbResultIsSuccessful(productsResult, out var error))
            return error;

        var productDtos = productsResult.Result.Select(ProductDto.FromModel);
        return new JsonResult(productDtos);
    }

    [HttpPost]
    [ServiceFilter(typeof(CheckAuthFilter))]
    public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
    {
        var createResult = await _productsRepository.CreateProductAsync(product);

        return DbResultHelper.DbResultIsSuccessful(createResult, out var error)
            ? new CreatedResult("/{productId}", product.Id)
            : error;
    }

    [HttpPut("{productId:Guid}")]
    [ServiceFilter(typeof(CheckAuthFilter))]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid productId, [FromBody] UpdateProductRequestDto requestInfo)
    {
        var updateResult = await _productsRepository.UpdateProductAsync(productId, new ProductUpdateInfo
        {
            Name = requestInfo.Name,
            Description = requestInfo.Description,
            Category = requestInfo.Category,
            PriceInRubles = requestInfo.PriceInRubles
        });

        return DbResultHelper.DbResultIsSuccessful(updateResult, out var error)
            ? new NoContentResult()
            : error;
    }

    [HttpDelete("{productId:Guid}")]
    [ServiceFilter(typeof(CheckAuthFilter))]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId)
    {
        var deleteResult = await _productsRepository.DeleteProductAsync(productId);

        return DbResultHelper.DbResultIsSuccessful(deleteResult, out var error)
            ? new NoContentResult()
            : error;
    }

    [HttpGet("{productId:Guid}/comments")]
    public async Task<IActionResult> GetCommentsByProductId([FromRoute] Guid productId)
    {
        var resultFindProduct = await _productsRepository.GetProductAsync(productId);

        if (!DbResultHelper.DbResultIsSuccessful(resultFindProduct, out var error))
        {
            return error;
        }

        var result = await _commentService.GetCommentsByProductId(productId);

        var resultDto = result.Select(GetCommentDto.FromModel);

        return new JsonResult(resultDto);
    }

    [HttpPost("{productId:Guid}/comments")]
    public async Task<IActionResult> AddCommentToProduct([FromRoute] Guid productId,
                                                        [FromBody] CreateCommentDto createCommentDto)
    {
        var resultFindProduct = await _productsRepository.GetProductAsync(productId);

        if (!DbResultHelper.DbResultIsSuccessful(resultFindProduct, out var error))
        {
            return error;
        }

        await _commentService.AddAnonymComment(productId, createCommentDto);

        return Ok();
    }

    //[HttpDelete("{productId:Guid}/comments")]
    //public async Task<IActionResult> RemoveComment()
    //{

    //}
}