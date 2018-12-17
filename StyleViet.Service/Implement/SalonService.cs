using StyleViet.Repository.Interface;
using StyleViet.Service.Interface;
using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleViet.Service.Implement
{
    public class SalonService : ISalonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISalonRepository _salonRepository;

        public SalonService(IUnitOfWork unitOfWork, ISalonRepository salonRepo)
        {
            _unitOfWork = unitOfWork;
            _salonRepository = salonRepo;
        }

        public async Task<IEnumerable<SalonViewModel>> GetAllSalon()
        {
            var salons = _salonRepository.GetAll().Select(s => new SalonViewModel
            {
                Id = s.AccountId,
                Name = s.Name,
                Address = s.Address,
                Phone = s.Phone,
                SalonId = s.Id,
                Email = s.Email
            });
            return salons;
        }
    }
}
