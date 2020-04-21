using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Domain.Handlers.Bases;
using DiabloII.Domain.Readers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    public class BaseController<DataModel, ResponseDto> : Controller
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

        protected ActionResult<ResponseDto> Create<CreateDto, CreateCommand>(
            CreateDto dto,
            ICommandHandlerCreate<CreateCommand, DataModel> handlerCreate,
            IMapper mapper)
        {
            var command = mapper.Map<CreateCommand>(dto);
            var model = handlerCreate.Create(command);
            var response = mapper.Map<ResponseDto>(model);

            return this.CreatedByUsingTheRequestRoute(response);
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
        protected ActionResult<Response> DeleteWithMap<DeleteDto, DeleteCommand, CommandResponse, Response>(
            DeleteDto dto,
            ICommandHandlerDelete<DeleteCommand, CommandResponse> handlerDelete,
            IMapper mapper)
        {
            var command = mapper.Map<DeleteCommand>(dto);
            var model = handlerDelete.Delete(command);
            var response = mapper.Map<ResponseDto>(model);
            
            return Ok(response);
        }
    }
}