using Microsoft.Data.SqlClient;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using RealEstateAgency.Service.Mappers;
using System.Linq.Expressions;

namespace RealEstateAgency.Service.EstateService
{
    public class EstateService : IEstateService
    {
        private readonly IGenericRepository<Estate> _estateRepository;
        private readonly IGenericRepository<EstateOption> _estateOptionRepository;
        private readonly IGenericRepository<Photo> _imageRepository;
        public EstateService(IGenericRepository<Estate> estateRepository, IGenericRepository<EstateOption> estateOptionRepository, IGenericRepository<Photo> imageRepository)
        {
            _estateRepository = estateRepository;
            _estateOptionRepository = estateOptionRepository;
            _imageRepository = imageRepository;
        }
        public async Task<IEnumerable<Estate>> GetAllAsync(string sortOption, SortOrder sortOrder)
        {
            var estates = await _estateRepository.GetAllAsync(
                e => e.AgentUser,
                e => e.BuildingPlan,
                e => e.BuildingType,
                e => e.Category,
                e => e.EstateCondition,
                e => e.EstateOptions,
                e => e.Photos,
                e => e.Zone);

            if (sortOption.ToLower() == "estatename")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    estates = estates.OrderBy(e => e.EstateName);
                }
                else
                {
                    estates = estates.OrderByDescending(e => e.EstateName);
                }
            }
            else if (sortOption.ToLower() == "address")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    estates = estates.OrderBy(e => e.Address);
                }
                else
                {
                    estates = estates.OrderByDescending(e => e.Address);
                }
            }
            else if (sortOption.ToLower() == "agent")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    estates = estates.OrderBy(e => e.AgentUser?.UserName);
                }
                else
                {
                    estates = estates.OrderByDescending(e => e.AgentUser?.UserName);
                }
            }

            return estates;
        }
        public async Task<IEnumerable<Estate>> GetAllAsync(Expression<Func<Estate, bool>> filter)
        {
            return await _estateRepository.GetAllAsync(filter,
                e => e.AgentUser,
                e => e.BuildingPlan,
                e => e.BuildingType,
                e => e.Category,
                e => e.EstateCondition,
                e => e.EstateOptions,
                e => e.Photos,
                e => e.Zone);
        }

        public async Task<Estate> GetByIdAsync(Guid id)
        {
            return await _estateRepository.GetByIdAsync(id,
                e => e.AgentUser,
                e => e.BuildingPlan,
                e => e.BuildingType,
                e => e.Category,
                e => e.EstateCondition,
                e => e.EstateOptions,
                e => e.Photos,
                e => e.Zone);
        }

        public async Task<bool> AddAsync(Estate estate)
        {
            return await _estateRepository.AddAsync(estate);
        }

        public async Task<bool> UpdateAsync(EditEstateViewModel editEstateViewModel)
        {
            var setIds = editEstateViewModel.EstateOptionViewModels.Where(eo => eo.IsSet == true).Select(eo => eo.Id);

            var estate = await GetByIdAsync(editEstateViewModel.Id);
            estate.EstateOptions = (List<EstateOption>)await _estateOptionRepository.GetAllAsync(o => setIds.Contains(o.Id));
            estate.Photos = (List<Photo>)await _imageRepository.GetAllAsync(i => editEstateViewModel.PhotoNames.Contains(i.FileTitle));
            estate.SetValues(editEstateViewModel);

            return await _estateRepository.UpdateAsync(estate);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _estateRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
