using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Hals.Bases;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Domain.Commands.Bases;
using DiabloII.Domain.Exceptions;
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

        protected ActionResult<HALResponse> GetAll(IReaderGetAll<DataModel> readerGetAll, IHalDecorator<ResponseDto> halDecorator)
        {
            var halResponses = readerGetAll
                .GetAll()
                .Select(dataModel =>
                {
                    var responseDto = _mapper.Map<ResponseDto>(dataModel);

                    return halDecorator.AddLinks(responseDto);
                })
                .ToList();
            var halResponse = halDecorator.AddLinks(halResponses);

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

        protected ActionResult<HALResponse> Get<RequestDto, Query>(RequestDto requestDto, IReaderGet<DataModel, Query> readerGet, IHalDecorator<ResponseDto> halDecorator)
        {
            var query = _mapper.Map<Query>(requestDto);
            var dataModel = readerGet.Get(query);

            if (dataModel is null)
                throw new NotFoundException("suggestion");

            var responseDto = _mapper.Map<ResponseDto>(dataModel);
            var halResponse = halDecorator.AddLinks(responseDto);

            return Ok(halResponse);
        }

        protected ActionResult<ResponseDto> Get<RequestDto, Query>(RequestDto requestDto, IReaderGet<DataModel, Query> readerGet)
        {
            var query = _mapper.Map<Query>(requestDto);
            var dataModel = readerGet.Get(query);

            if (dataModel is null)
                throw new NotFoundException("suggestion");

            var responseDto = _mapper.Map<ResponseDto>(dataModel);

            return Ok(responseDto);
        }
        #endregion

        #region Write
        protected async Task<ActionResult<Guid>> Create<CreateDto, CreateCommand>(CreateDto requestDto)
            where  CreateCommand : ICreateCommand<Guid> =>
            await Create<CreateDto, CreateCommand, Guid>(requestDto);
       
        protected async Task<ActionResult<CreatedtResourceId>> Create<CreateDto, CreateCommand, CreatedtResourceId>(CreateDto requestDto)
            where  CreateCommand : ICreateCommand<CreatedtResourceId>
        {
            var command = _mapper.Map<CreateCommand>(requestDto);
            
            await _mediator.Send(command);

            return this.CreatedByUsingTheRequestRoute(command.Id);
        }

        protected async Task<ActionResult<Guid>> Update<UpdateDto, UpdateCommand>(UpdateDto dto)
            where UpdateCommand : IUpdateCommand =>
            await Update<UpdateDto, UpdateCommand, Guid>(dto);

        protected async Task<ActionResult<UpdatedResourceId>> Update<UpdateDto, UpdateCommand, UpdatedResourceId>(UpdateDto dto)
            where UpdateCommand : IUpdateCommand<UpdatedResourceId>
        {
            var command = _mapper.Map<UpdateCommand>(dto);
            
            await _mediator.Send(command);

            return Ok(command.Id);
        }

        protected async Task<ActionResult<Guid>> Delete<DeleteDto, DeleteCommand>(DeleteDto dto)
            where DeleteCommand : IDeleteCommand<Guid> =>
            await Delete<DeleteDto, DeleteCommand, Guid>(dto);

        protected async Task<ActionResult<DeletedResourceId>> Delete<DeleteDto, DeleteCommand, DeletedResourceId>(DeleteDto dto)
            where DeleteCommand : IDeleteCommand<DeletedResourceId>
        {
            var command = _mapper.Map<DeleteCommand>(dto);
            
            await _mediator.Send(command);

            return Ok(command.Id);
        }
        #endregion
    }
}