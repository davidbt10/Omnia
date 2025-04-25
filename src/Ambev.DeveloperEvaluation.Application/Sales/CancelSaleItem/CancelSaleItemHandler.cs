using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<CancelSaleItemCommand> _validator;
        private readonly IMapper _mapper;

        public CancelSaleItemHandler(ISaleRepository repository, 
            IValidator<CancelSaleItemCommand> validator, 
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleItemValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _repository.GetByIdAsync(command.SaleId);
            if (sale == null)
                throw new Exception("Sale not found.");

            sale.CancelItem(command.ItemId);
            await _repository.UpdateAsync(sale);

            return _mapper.Map<CancelSaleItemResult>(sale);
        }
    }
}
