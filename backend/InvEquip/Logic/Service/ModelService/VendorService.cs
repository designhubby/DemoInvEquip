using AutoMapper;
using InvEquip.Data.Repository;
using InvEquip.Dto;
using InvEquip.Logic.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public class VendorService : ServiceBase
    {
        public VendorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task<IEnumerable<VendorDataDto>> GetAllVendorDataDtos()
        {
            IEnumerable<VendorModel> vendorModels = await _unitofwork.VendorRepository.GetAllDomainModelsAsync();
            IEnumerable<VendorDataDto> vendorDataDtos = vendorModels.Select((indiv) => _mapper.Map<VendorModel, VendorDataDto>(indiv));
            return vendorDataDtos;
        }
        public async Task<IEnumerable<ContractDataDto>> GetContractDataDtoByVendorByVendorId(int vendorId)
        {
            VendorModel vendorModel = await _unitofwork.VendorRepository.GetDomainModelByIdAsync(vendorId);

            IEnumerable<ContractModel> ContractModels = vendorModel._entity.Contracts.ToList().Select((indiv) => new ContractModel(indiv));
            IEnumerable<ContractDataDto> contractDataDtos = ContractModels.Select((indiv) => _mapper.Map<ContractModel, ContractDataDto>(indiv));
            return contractDataDtos;

        }

        public async Task<VendorDataDto> GetVendorDataDtoByVendorId(int id)
        {
            VendorDataDto vendorDataDto = new();
            VendorModel vendorModel = await _unitofwork.VendorRepository.GetDomainModelByIdAsync(id);
            return (vendorModel != null ? _mapper.Map<VendorModel, VendorDataDto>(vendorModel) : null);

        }

        public async Task<VendorDataDto> CreateVendorDataByDtoAsync(VendorDataDto vendorDataDto)
        {
            try
            {
                VendorModel vendorModel = new VendorModel(vendorDataDto.VendorName);
                vendorModel = await _unitofwork.VendorRepository.AddDomainAsync(vendorModel);
                VendorDataDto result = _mapper.Map<VendorModel, VendorDataDto>(vendorModel);
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        public async Task<IEnumerable<ContractDataDto>> GetContractDataDtoByVendorByContractIdAsync(int contractId)
        {
            ContractModel contractModel = await _unitofwork.ContractRepository.GetDomainModelByIdAsync(contractId);
            int vendorId = contractModel.VendorId;
            IEnumerable<ContractDataDto> contractDataDtos = await GetContractDataDtoByVendorByVendorId(vendorId);
            return contractDataDtos;
        }
        public async Task<bool> UpdateVendorDataByDtoAsync(VendorDataDto vendorDataDto)
        {
            
            try
            {
                VendorModel vendorModel = await _unitofwork.VendorRepository.GetDomainModelByIdAsync(vendorDataDto.VendorId);
                vendorModel.ChangeVendorNameTo(vendorDataDto.VendorName);
                _unitofwork.SaveAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
