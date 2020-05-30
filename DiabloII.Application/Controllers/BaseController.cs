using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Services.Hals;
using DiabloII.Domain.Handlers.Bases;
using DiabloII.Domain.Readers.Bases;
using Halcyon.HAL;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    public class BaseController<DataModel, ResponseDto> : ControllerBase
        where DataModel : class
        where ResponseDto : class
    {
        protected ActionResult<IReadOnlyCollection<ResponseDto>> GetAll(
            IReaderGetAll<DataModel> readerGetAll,
            IMapper mapper)
        {
            var response = readerGetAll
                .GetAll()
                .Select(mapper.Map<ResponseDto>)
                .ToList();

            return Ok(response);
        }

        protected ActionResult<HALResponse> GetAll(
            IReaderGetAll<DataModel> readerGetAll,
            IMapper mapper,
            IHalService<ResponseDto> halService)
        {
            var halResponseList = readerGetAll
                .GetAll()
                .Select(dataModel =>
                {
                    var dto = mapper.Map<ResponseDto>(dataModel);

                    return halService.AddLinks(dto);
                })
                .ToList();
            var halResponse = halService.AddLinks(halResponseList);

            return Ok(halResponse);
        }

        protected ActionResult<IReadOnlyCollection<ResponseDto>> Search<RequestDto, Query>(
            RequestDto searchDto,
            IReaderSearch<Query, DataModel> readerSearch,
            IMapper mapper)
        {
            var response = readerSearch
                .Search(mapper.Map<Query>(searchDto))
                .Select(mapper.Map<ResponseDto>)
                .ToList();

            return Ok(response);
        }

        protected async Task<ActionResult<ResponseDto>> CreateWithMediator<CreateDto, CreateCommand>(CreateDto dto, IMapper mapper, IMediator mediator)
        {
            var command = mapper.Map<CreateCommand>(dto);
            var model = await mediator.Send(command);
            var response = mapper.Map<ResponseDto>(model);

            return this.CreatedByUsingTheRequestRoute(response);
        }

        protected ActionResult<HALResponse> Create<CreateDto, CreateCommand>(
            CreateDto requestDto,
            ICommandHandlerCreate<CreateCommand, DataModel> handlerCreate,
            IMapper mapper,
            IHalService<ResponseDto> halService)
        {
            var command = mapper.Map<CreateCommand>(requestDto);
            var model = handlerCreate.Create(command);
            var responseDto = mapper.Map<ResponseDto>(model);
            var halResponse = halService.AddLinks(responseDto);

            return this.CreatedByUsingTheRequestRoute(halResponse);
        }

        protected ActionResult<ResponseDto> Update<UpdateDto, UpdateCommand>(
            UpdateDto dto,
            ICommandHandlerUpdate<UpdateCommand, DataModel> handlerUpdate,
            IMapper mapper)
        {
            var command = mapper.Map<UpdateCommand>(dto);
            var model = handlerUpdate.Update(command);
            var response = mapper.Map<ResponseDto>(model);

            return Ok(response);
        }

        protected ActionResult<Guid> Delete<DeleteDto, DeleteCommand>(
            DeleteDto dto,
            ICommandHandlerDelete<DeleteCommand, Guid> handlerDelete,
            IMapper mapper)
        {
            var command = mapper.Map<DeleteCommand>(dto);
            var response = handlerDelete.Delete(command);

            return Ok(response);
        }
        protected ActionResult<HALResponse> DeleteWithMap<DeleteDto, DeleteCommand, CommandResponse, ResponseDto>(
            DeleteDto requestDto,
            ICommandHandlerDelete<DeleteCommand, CommandResponse> handlerDelete,
            IMapper mapper,
            IHalService<ResponseDto> halService)
        {
            var command = mapper.Map<DeleteCommand>(requestDto);
            var model = handlerDelete.Delete(command);
            var responseDto = mapper.Map<ResponseDto>(model);
            var halResponse = halService.AddLinks(responseDto);

            return Ok(halResponse);
        }
    }
}