using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesHandler : IRequestHandler<ListSalesQuery, ListSalesResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<ListSalesQuery> _validator;
        private readonly IMapper _mapper;

        public ListSalesHandler(ISaleRepository repository,
            IValidator<ListSalesQuery> validator,
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ListSalesResult> Handle(ListSalesQuery query, CancellationToken cancellationToken)
        {
            var validator = new ListSalesValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var (sales, total) = await _repository.ListPagedAsync(query.Page, query.PageSize);
            return new ListSalesResult
            {
                TotalItems = total,
                Sales = _mapper.Map<List<SaleItemDto>>(sales)
            };
        }
    }
}
