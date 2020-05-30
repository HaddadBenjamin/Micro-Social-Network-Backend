using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Services.Hals;
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

        protected async Task<ActionResult<ResponseDto>> Create<CreateDto, CreateCommand>(CreateDto dto, IMediator mediator, IMapper mapper)
        {
            var command = mapper.Map<CreateCommand>(dto);
            var model = await mediator.Send(command);
            var response = mapper.Map<ResponseDto>(model);

            return this.CreatedByUsingTheRequestRoute(response);
        }

        protected async Task<ActionResult<HALResponse>> Create<CreateDto, CreateCommand>(
            CreateDto requestDto,
            IMediator mediator,
            IMapper mapper,
            IHalService<ResponseDto> halService)
        {
            var command = mapper.Map<CreateCommand>(requestDto);
            var model = await mediator.Send(command);
            var responseDto = mapper.Map<ResponseDto>(model);
            var halResponse = halService.AddLinks(responseDto);

            return this.CreatedByUsingTheRequestRoute(halResponse);
        }

        protected async Task<ActionResult<ResponseDto>> Update<UpdateDto, UpdateCommand>(
            UpdateDto dto,
            IMediator mediator,
            IMapper mapper)
        {
            var command = mapper.Map<UpdateCommand>(dto);
            var model = await mediator.Send(command);
            var response = mapper.Map<ResponseDto>(model);

            return Ok(response);
        }

        protected async Task<ActionResult<ResponseDto>> Delete<DeleteDto, DeleteCommand, ResponseDto>(
            DeleteDto dto,
            IMediator mediator,
            IMapper mapper)
        {
            var command = mapper.Map<DeleteCommand>(dto);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        protected async Task<ActionResult<HALResponse>> DeleteWithMap<DeleteDto, DeleteCommand, ResponseDto>(
            DeleteDto requestDto,
            IMediator mediator,
            IMapper mapper,
            IHalService<ResponseDto> halService)
        {
            var command = mapper.Map<DeleteCommand>(requestDto);
            var model = await mediator.Send(command);
            var responseDto = mapper.Map<ResponseDto>(model);
            var halResponse = halService.AddLinks(responseDto);

            return Ok(halResponse);
        }
    }
}