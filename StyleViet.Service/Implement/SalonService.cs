using StyleViet.Repository.Interface;
using StyleViet.Service.Interface;
using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StyleViet.Service.Implement
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _salonRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SalonService(IUnitOfWork unitOfWork, ISalonRepository salonRepository)
        {
            _unitOfWork = unitOfWork;
            _salonRepository = salonRepository;
        }

        public SalonViewModel GetProfile(string username)
        {
            var salon = _salonRepository.Find(x => x.Account.Username.Equals(username)).FirstOrDefault();
            if (salon != null)
            {
                return new SalonViewModel
                {
                    SalonId = salon.Id,
                    Name = salon.Name,
                    Phone = salon.Phone,
                    Address = salon.Address,
                    Email = salon.Email

                };
            }
            else
            {
                return null;
            }
            
        }

        public void UpdateProfile(SalonViewModel model)
        {
            if (model != null)
            {
                var salon = _salonRepository.FindById(model.SalonId);
                if (salon != null)
                {
                    salon.Name = model.Name;
                    salon.Phone = model.Phone;
                    salon.Address = model.Address;
                    _salonRepository.Update(salon);
                    _unitOfWork.Save();
                }
            }
        }
    }
}
