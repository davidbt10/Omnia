﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<Sale, UpdateSaleCommand>();
            CreateMap<SaleItem, UpdateSaleItemDto>();
        }
    }
}
