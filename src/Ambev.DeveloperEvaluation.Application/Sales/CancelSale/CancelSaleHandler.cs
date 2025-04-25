using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<CancelSaleCommand> _validator;
        private readonly IMapper _mapper;

        public CancelSaleHandler(ISaleRepository repository, 
            IValidator<CancelSaleCommand> validator, 
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _repository.GetByIdAsync(command.SaleId);
            if (sale == null)
                throw new Exception("Sale not found.");

            sale.CancelSale();
            await _repository.UpdateAsync(sale);

            return _mapper.Map<CancelSaleResult>(sale);
        }
    }
}
