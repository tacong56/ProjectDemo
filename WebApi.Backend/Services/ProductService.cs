using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi.Backend.Common;
using WebApi.Backend.Interfaces;
using WebApi.Backend.Models;
using WebApi.Backend.Objects;
using WebApi.Data.EF;
using WebApi.Data.Entities;
using static WebApi.Backend.Common.Utilities;

namespace WebApi.Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly WebApiDbContext _context;
        private readonly IStorageService _storageService;

        public ProductService(WebApiDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<ResponseData<NonResponseData>> Create(CreateProductRequest request)
        {
            var languages = _context.Languages;
            var translations = new List<ProductTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.LanguageId)
                {
                    translations.Add(new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    });
                }
                else
                {
                    translations.Add(new ProductTranslation()
                    {
                        Name = ProductConstants.NA,
                        Description = ProductConstants.NA,
                        SeoAlias = ProductConstants.NA,
                        LanguageId = language.Id
                    });
                }
            }
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = translations
            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new SuccessResponseData<NonResponseData>("Create success.");
        }

        public async Task<PaginationResult<ProductViewModel>> Get(GetProductRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        //join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        where pt.LanguageId == (string.IsNullOrEmpty(request.LanguageId) ? "vi" : request.LanguageId)
                        select new { p, pt, pi };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            //if (request.CategoryIds != null && request.CategoryIds.Count > 0)
            //    query = query.Where(x => request.CategoryIds.Contains(x.pic.CategoryId));

            var entriesCount = query.Count();

            var data = new List<ProductViewModel>();
            try
            {
                data = query.Skip((request.Page - 1) * request.Limit)
                            .Take(request.Limit)
                            .Select(x => new ProductViewModel()
                            {
                                Id = x.p.Id,
                                Price = x.p.Price,
                                OriginalPrice = x.p.OriginalPrice,
                                DateCreated = x.p.DateCreated,
                                Stock = x.p.Stock,

                                Name = x.pt.Name,

                                ThumbnailPath = x.pi.ImagePath
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }


            var result = new PaginationResult<ProductViewModel>()
            {
                Limit = request.Limit,
                Page = request.Page,
                items = data
            };

            return result;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"Thumbnail_{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + SystemConstans.USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}
