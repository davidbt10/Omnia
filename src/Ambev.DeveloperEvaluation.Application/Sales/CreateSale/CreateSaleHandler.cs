using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<CreateSaleCommand> _validator;
        private readonly IMapper _mapper;

        public CreateSaleHandler(
            ISaleRepository repository,
            IValidator<CreateSaleCommand> validator,
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var sale = _mapper.Map<Sale>(command);

            await _repository.AddAsync(sale);
            return _mapper.Map<CreateSaleResult>(sale);
        }
    }
}
