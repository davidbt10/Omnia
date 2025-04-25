using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<UpdateSaleCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository repository, IValidator<UpdateSaleCommand> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var sale = await _repository.GetByIdAsync(command.Id);
            if (sale == null)
                throw new Exception("Sale not found.");

            sale.UpdateInfo(command.CustomerId, command.CustomerName, command.BranchId, command.BranchName);
            command.Items.ForEach(i => sale.UpdateItem(_mapper.Map<SaleItem>(i)));

            await _repository.UpdateAsync(sale);
            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
}
