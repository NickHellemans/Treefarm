using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Exceptions;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Zones
{
    public class CreateZoneCommand : IRequest<Tuple<CreateZoneDTO, List<ValidationFailure>>>
    {
        public string Name { get; set; }
        public float SurfaceArea { get; set; }
        public int SiteId { get; set; }
        public int TreeId { get; set; }
    }

    public class CreateZoneCommandHandler : IRequestHandler<CreateZoneCommand, Tuple<CreateZoneDTO, List<ValidationFailure>>>
    {
        private IValidator<CreateZoneDTO> validator;
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        
        public CreateZoneCommandHandler(IUnitofWork uow, IMapper mapper, IValidator<CreateZoneDTO> validator)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<Tuple<CreateZoneDTO, List<ValidationFailure>>> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
        {
            var site = await uow.SitesRepository.GetById(request.SiteId);
            if (site == null)
                throw new RelationNotFoundException("The specified site does not exist");
            
            var tree = await uow.TreeRepository.GetById(request.TreeId);
            if (tree == null)
                throw new RelationNotFoundException("The specified tree does not exist");
            


            var zone = new Zone()
            {
                Name = request.Name,
                SurfaceArea = request.SurfaceArea,
                SiteId = request.SiteId,
                TreeId = request.TreeId
            };

            var result = await validator.ValidateAsync(mapper.Map<CreateZoneDTO>(zone), cancellationToken);
            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<CreateZoneDTO>(zone), result.Errors);

            }
            uow.ZonesRepository.Create(zone);
            await uow.Commit();

            return Tuple.Create(mapper.Map<CreateZoneDTO>(zone), result.Errors);
        }
    }
}