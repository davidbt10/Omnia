using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<GetSaleQuery> _validator;
        private readonly IMapper _mapper;

        public GetSaleHandler(ISaleRepository repository, 
            IValidator<GetSaleQuery> validator, 
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GetSaleResult?> Handle(GetSaleQuery query, CancellationToken cancellationToken)
        {
            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            var sale = await _repository.GetByIdAsync(query.Id);

            return sale != null ?
                _mapper.Map<GetSaleResult>(sale) : null;
        }
    }
}
