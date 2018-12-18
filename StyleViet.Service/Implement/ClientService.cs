using StyleViet.Repository.Interface;
using StyleViet.Repository.Repository;
using StyleViet.Service.Interface;
using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StyleViet.Service.Implement
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientRepository _clientRepository;

        public ClientService(IUnitOfWork unitOfWork, IClientRepository clientRepository)
        {
            _unitOfWork = unitOfWork;
            _clientRepository = clientRepository;
        }

        public IEnumerable<MemberViewModel> GetAll()
        {
            return _clientRepository.GetAll().Select(m => new MemberViewModel
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                MemberId = m.Id,
                Phone = m.Phone, 
                Email = m.Email
            });
        }
    }
}
