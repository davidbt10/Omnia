using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleItemDto, SaleItem>()
                .ConstructUsing(src => new SaleItem(
                    new ExternalIdentity(src.ProductId, src.ProductName),
                    src.Quantity,
                    src.UnitPrice));

            CreateMap<CreateSaleCommand, Sale>()
                .ConstructUsing((src, ctx) =>
                {
                    var mapper = ctx.Mapper;
                    var items = src.Items
                                   .Select(i => mapper.Map<SaleItem>(i))
                                   .ToList();
                    return new Sale(
                        src.SaleNumber,
                        src.Date,
                        new ExternalIdentity(src.CustomerId, src.CustomerName),
                        new ExternalIdentity(src.BranchId, src.BranchName),
                        items);
                });

            CreateMap<Sale, CreateSaleResult>();
        }
    }
}
