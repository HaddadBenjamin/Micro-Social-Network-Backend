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

namespace DiabloII.Application.Controllers
{
    public class BaseController<DataModel, ResponseDto> : ControllerBase
        where DataModel : class
        where ResponseDto : class
    {
        protected ActionResult<ApiResponses<ResponseDto>> GetAll(
            IReaderGetAll<DataModel> readerGetAll,
            IMapper mapper)
        {
            var responseDtos = readerGetAll
                .GetAll()
                .Select(mapper.Map<ResponseDto>)
                .ToList();
            var responseDto = new ApiResponses<ResponseDto>
            {
                Elements = responseDtos
            };

            return Ok(responseDto);
        }

        protected ActionResult<HALResponse> GetAll(
            IReaderGetAll<DataModel> readerGetAll,
            IMapper mapper,
            IHalService<ResponseDto> halService)
        {
            var halResponses = readerGetAll
                .GetAll()
                .Select(dataModel =>
                {
                    var responseDto = mapper.Map<ResponseDto>(dataModel);

                    return halService.AddLinks(responseDto);
                })
                .ToList();
            var halResponse = halService.AddLinks(halResponses);

            return Ok(halResponse);
        }

        protected ActionResult<ApiResponses<ResponseDto>> Search<RequestDto, Query>(
            RequestDto searchDto,
            IReaderSearch<Query, DataModel> readerSearch,
            IMapper mapper)
        {
            var responseDtos = readerSearch
                .Search(mapper.Map<Query>(searchDto))
                .Select(mapper.Map<ResponseDto>)
                .ToList();
            var responseDto = new ApiResponses<ResponseDto>
            {
                Elements = responseDtos
            };

            return Ok(responseDto);
        }

        protected async Task<ActionResult<Guid>> Create<CreateDto, CreateCommand>(
            CreateDto requestDto,
            IMediator mediator,
            IMapper mapper)
        {
            var command = mapper.Map<CreateCommand>(requestDto);
            var createdId = await mediator.Send(command);

            return this.CreatedByUsingTheRequestRoute(createdId);
        }

        protected async Task<ActionResult<ResponseDto>> Update<UpdateDto, UpdateCommand>(
            UpdateDto dto,
            IMediator mediator,
            IMapper mapper)
        {
            var command = mapper.Map<UpdateCommand>(dto);
            var model = await mediator.Send(command);
            var responseDto = mapper.Map<ResponseDto>(model);

            return Ok(responseDto);
        }

        protected async Task<ActionResult<ResponseDto>> Delete<DeleteDto, DeleteCommand, ResponseDto>(
            DeleteDto dto,
            IMediator mediator,
            IMapper mapper)
        {
            var command = mapper.Map<DeleteCommand>(dto);
            var responseDto = await mediator.Send(command);

            return Ok(responseDto);
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