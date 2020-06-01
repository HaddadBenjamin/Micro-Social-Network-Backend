using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Services.Hals;
using DiabloII.Domain.Readers.Bases;
using Halcyon.HAL;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers.Bases
{
    public class BaseController<DataModel, ResponseDto> : ControllerBase
        where DataModel : class
        where ResponseDto : class
    {
        protected readonly IMediator _mediator;

        protected readonly IMapper _mapper;

        public BaseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Read
        protected ActionResult<ApiResponses<ResponseDto>> GetAll(IReaderGetAll<DataModel> readerGetAll)
        {
            var responseDtos = readerGetAll
                .GetAll()
                .Select(_mapper.Map<ResponseDto>)
                .ToList();
            var responseDto = new ApiResponses<ResponseDto>
            {
                Elements = responseDtos
            };

            return Ok(responseDto);
        }

        protected ActionResult<HALResponse> GetAll(IReaderGetAll<DataModel> readerGetAll, IHalService<ResponseDto> halService)
        {
            var halResponses = readerGetAll
                .GetAll()
                .Select(dataModel =>
                {
                    var responseDto = _mapper.Map<ResponseDto>(dataModel);

                    return halService.AddLinks(responseDto);
                })
                .ToList();
            var halResponse = halService.AddLinks(halResponses);

            return Ok(halResponse);
        }

        protected ActionResult<ApiResponses<ResponseDto>> Search<RequestDto, Query>(RequestDto searchDto, IReaderSearch<Query, DataModel> readerSearch)
        {
            var responseDtos = readerSearch
                .Search(_mapper.Map<Query>(searchDto))
                .Select(_mapper.Map<ResponseDto>)
                .ToList();
            var responseDto = new ApiResponses<ResponseDto>
            {
                Elements = responseDtos
            };

            return Ok(responseDto);
        }

        protected ActionResult<HALResponse> Get<RequestDto, Query>(RequestDto requestDto, IReaderGet<DataModel, Query> readerGet, IHalService<ResponseDto> halService)
        {
            var query = _mapper.Map<Query>(requestDto);
            var dataModel = readerGet.Get(query);
            var responseDto = _mapper.Map<ResponseDto>(dataModel);
            var halResponse = halService.AddLinks(responseDto);

            return Ok(halResponse);
        }

        protected ActionResult<ResponseDto> Get<RequestDto, Query>(RequestDto requestDto, IReaderGet<DataModel, Query> readerGet)
        {
            var query = _mapper.Map<Query>(requestDto);
            var dataModel = readerGet.Get(query);
            var responseDto = _mapper.Map<ResponseDto>(dataModel);

            return Ok(responseDto);
        }
        #endregion

        #region Write
        protected async Task<ActionResult<Guid>> Create<CreateDto, CreateCommand>(CreateDto requestDto)
        {
            var command = _mapper.Map<CreateCommand>(requestDto);
            var createdResourceId = await _mediator.Send(command);

            return this.CreatedByUsingTheRequestRoute(createdResourceId);
        }

        protected async Task<ActionResult<Guid>> Update<UpdateDto, UpdateCommand>(UpdateDto dto)
        {
            var command = _mapper.Map<UpdateCommand>(dto);
            var updatedResourceId = await _mediator.Send(command);

            return Ok(updatedResourceId);
        }

        protected async Task<ActionResult<Guid>> Delete<DeleteDto, DeleteCommand>(DeleteDto dto)
        {
            var command = _mapper.Map<DeleteCommand>(dto);
            var deletedResourceId = await _mediator.Send(command);

            return Ok(deletedResourceId);
        }
        #endregion
    }
}