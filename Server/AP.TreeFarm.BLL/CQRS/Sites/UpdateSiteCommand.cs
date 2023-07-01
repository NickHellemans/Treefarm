using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Sites
{
    public class UpdateSiteCommand : IRequest<Tuple<UpdateSiteDTO, List<ValidationFailure>>>
    {
        public int Id;
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string MapPicturePath { get; set; }
    }

    public class UpdateSiteCommandHandler : IRequestHandler<UpdateSiteCommand, Tuple<UpdateSiteDTO, List<ValidationFailure>>>
    {
        private IValidator<UpdateSiteDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        public UpdateSiteCommandHandler(IUnitofWork uow, IValidator<UpdateSiteDTO> validator, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
            this._validator = validator;
        }

        public async Task<Tuple<UpdateSiteDTO, List<ValidationFailure>>> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
        {
            var site = await uow.SitesRepository.GetById(request.Id);
            if (site == null)
                throw new KeyNotFoundException("The site was not found");
            
            site.Name = request.Name;
            site.PostalCode = request.PostalCode;
            site.Street = request.Street;
            site.StreetNumber = request.StreetNumber;
            site.MapPicturePath = request.MapPicturePath;
            
            ValidationResult result = await _validator.ValidateAsync(mapper.Map<UpdateSiteDTO>(site));
            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<UpdateSiteDTO>(site), result.Errors);
            }

            
            uow.SitesRepository.Update(site);
            await uow.Commit();
            return Tuple.Create(mapper.Map<UpdateSiteDTO>(site), result.Errors);
        }
    }
}