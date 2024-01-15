using AutoMapper;
using System.Threading.Tasks;
using InvEquip.Data.Repository;

namespace InvEquip.Logic.Service
{
    public class ServiceBase
    {
        protected IUnitOfWork _unitofwork;
        protected IMapper _mapper;

        public ServiceBase(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

    }
}