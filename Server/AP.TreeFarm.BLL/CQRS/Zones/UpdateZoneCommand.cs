using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Exceptions;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Zones
{
    public class UpdateZoneCommand : IRequest<Tuple<UpdateZoneDTO, List<ValidationFailure>>>
    {
        public int Id;
        public string Name { get; set; }
        public float SurfaceArea { get; set; }
        public int SiteId { get; set; }
        public int TreeId { get; set; }
    }

    public class UpdateZoneCommandHandler : IRequestHandler<UpdateZoneCommand, Tuple<UpdateZoneDTO, List<ValidationFailure>>>
    {
        private IValidator<UpdateZoneDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;

        public UpdateZoneCommandHandler(IUnitofWork uow, IMapper mapper, IValidator<UpdateZoneDTO> validator)
        {
            this._validator = validator;
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<Tuple<UpdateZoneDTO, List<ValidationFailure>>> Handle(UpdateZoneCommand request, CancellationToken cancellationToken)
        {
            //Check if zone, site and tree exist
            var zone = await uow.ZonesRepository.GetById(request.Id);
            if (zone == null)
                throw new KeyNotFoundException("The zone was not found");
            
            var site = await uow.SitesRepository.GetById(request.SiteId);
            if (site == null)
                throw new RelationNotFoundException("The specified site does not exist");
            
            var tree = await uow.TreeRepository.GetById(request.TreeId);
            if (tree == null)
                throw new RelationNotFoundException("The specified tree does not exist");
            
            //Update values
            zone.Name = request.Name;
            zone.SurfaceArea = request.SurfaceArea;
            zone.TreeId = request.TreeId;
            zone.SiteId = request.SiteId;
            
            
            var result = await _validator.ValidateAsync(mapper.Map<UpdateZoneDTO>(zone), cancellationToken);

            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<UpdateZoneDTO>(zone), result.Errors);

            }

            uow.ZonesRepository.Update(zone);
            await uow.Commit();
            return Tuple.Create(mapper.Map<UpdateZoneDTO>(zone), result.Errors);

        }
    }
}