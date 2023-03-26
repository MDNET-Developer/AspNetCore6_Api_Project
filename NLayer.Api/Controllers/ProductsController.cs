using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Service;

namespace NLayer.Api.Controllers
{

    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _productService;
        private readonly IProductService _proService;

        public ProductsController(IMapper mapper, IService<Product> productService, IProductService proService)
        {
            _mapper = mapper;
            _productService = productService;
            _proService = proService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            var data = await _proService.GetProductWithCategories();
            return CreateActionResult(data);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produts = await _productService.GetAllAsync();
            var mappedData = _mapper.Map<List<ProductDto>>(produts.ToList());
            return CreateActionResult/*<List<ProductDto>>*/(CustomResponseDto<List<ProductDto>>.Succsess(200, mappedData));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produts = await _productService.GetByIdAsync(id);
            var mappedData = _mapper.Map<ProductDto>(produts);
            return CreateActionResult/*<List<ProductDto>>*/(CustomResponseDto<ProductDto>.Succsess(200, mappedData));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var mappedData = _mapper.Map<Product>(productDto);
            var produts = await _productService.AddAsync(mappedData);
            var responsemappedData = _mapper.Map<ProductDto>(produts);
            return CreateActionResult/*<List<ProductDto>>*/(CustomResponseDto<ProductDto>.Succsess(201, responsemappedData));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            var mappedData = _mapper.Map<Product>(productDto);
            await _productService.UpdateAsync(mappedData);
            return CreateActionResult/*<List<ProductDto>>*/(CustomResponseDto<NoContentResponseDto>.Succsess(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productId = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(productId);
            return CreateActionResult/*<List<ProductDto>>*/(CustomResponseDto<NoContentResponseDto>.Succsess(204));
        }
    }
}
