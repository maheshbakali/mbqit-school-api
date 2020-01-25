using AutoMapper;
using School.BLL.Dto;
using School.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Actions
{
    public class SalutationActions : ISalutationActions
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SalutationActions(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SalutationDto>> GetAllAsync()
        {
            var salutations = await Task.Run(() => _unitOfWork.SalutationRepository.GetAll());
            var salutationDtoList = _mapper.Map<IEnumerable<SalutationDto>>(salutations);

            return salutationDtoList;
        }
    }
}
