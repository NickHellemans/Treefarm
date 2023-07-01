using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Trees
{
    public class UpdateTreeCommand : IRequest<Tuple<UpdateTreeDTO, List<ValidationFailure>>>
    {
        public int Id;
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string InstructionsUrl { get; set; }
        public string QrCodeUrl { get; set; }
    }

    public class UpdateTreeCommandHandler : IRequestHandler<UpdateTreeCommand, Tuple<UpdateTreeDTO, List<ValidationFailure>>>
    {
        private IValidator<UpdateTreeDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public UpdateTreeCommandHandler(IUnitofWork uow, IValidator<UpdateTreeDTO> validator, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
            this._validator = validator;
        }

        public async Task<Tuple<UpdateTreeDTO, List<ValidationFailure>>>  Handle(UpdateTreeCommand request, CancellationToken cancellationToken)
        {
            var tree = await uow.TreeRepository.GetById(request.Id);
            if (tree == null)
                throw new KeyNotFoundException("The tree was not found");
            
            tree.Name = request.Name;
            tree.PictureUrl = request.PictureUrl;
            tree.InstructionsUrl = request.InstructionsUrl;
            tree.QrCodeUrl = request.QrCodeUrl;
            
            ValidationResult result = await _validator.ValidateAsync(mapper.Map<UpdateTreeDTO>(tree));
            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<UpdateTreeDTO>(tree), result.Errors);
            }
            
            uow.TreeRepository.Update(tree);
            await uow.Commit();
            return Tuple.Create(mapper.Map<UpdateTreeDTO>(tree), result.Errors);
        }
    }
}