using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Trees
{
    public class CreateTreeCommand : IRequest<Tuple<CreateTreeDTO, List<ValidationFailure>>>
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string InstructionsUrl { get; set; }
        public string QrCodeUrl { get; set; }
    }

    public class CreateTreeCommandHandler : IRequestHandler<CreateTreeCommand, Tuple<CreateTreeDTO, List<ValidationFailure>>>
    {
        private IValidator<CreateTreeDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public CreateTreeCommandHandler(IUnitofWork uow, IMapper mapper, IValidator<CreateTreeDTO> validator)
        {
            this.uow = uow;
            this.mapper = mapper;
            this._validator = validator;
        }

        public async Task<Tuple<CreateTreeDTO, List<ValidationFailure>>> Handle(CreateTreeCommand request, CancellationToken cancellationToken)
        {
            var tree = new Tree()
            {
                Name = request.Name,
                PictureUrl = request.PictureUrl,
                InstructionsUrl = request.InstructionsUrl,
                QrCodeUrl = request.QrCodeUrl,
            };

            ValidationResult result = await _validator.ValidateAsync(mapper.Map<CreateTreeDTO>(tree));
            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<CreateTreeDTO>(tree), result.Errors);
            }

            uow.TreeRepository.Create(tree);
            await uow.Commit();
            return Tuple.Create(mapper.Map<CreateTreeDTO>(tree), result.Errors);
        }
    }

  
}
