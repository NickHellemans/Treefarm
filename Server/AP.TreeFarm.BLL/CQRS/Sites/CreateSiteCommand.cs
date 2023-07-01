using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using FluentValidation.Results;
using AutoMapper;
using MediatR;
using FluentValidation;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AP.MyTreeFarm.Application.CQRS.Sites
{
    public class CreateSiteCommand : IRequest<Tuple<CreateSiteDTO, List<ValidationFailure>>>
    {
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        
        public string MapPicturePath { get; set; }
    }

    public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, Tuple<CreateSiteDTO, List<ValidationFailure>>>
    {
        private IValidator<CreateSiteDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public CreateSiteCommandHandler(IUnitofWork uow, IValidator<CreateSiteDTO> validator, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
            this._validator = validator;

        }

        public async Task<Tuple<CreateSiteDTO, List<ValidationFailure>>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            var site = new Site()
            {
                Name = request.Name,
                PostalCode = request.PostalCode,
                Street = request.Street,
                StreetNumber = request.StreetNumber,
                MapPicturePath = request.MapPicturePath
            };

            ValidationResult result = await _validator.ValidateAsync(mapper.Map<CreateSiteDTO>(site));
            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<CreateSiteDTO>(site), result.Errors);
            }
            
            uow.SitesRepository.Create(site);
            await uow.Commit();
            return Tuple.Create(mapper.Map<CreateSiteDTO>(site), result.Errors);
           // return mapper.Map<CreateSiteDTO>(site);
        }
    }
}
