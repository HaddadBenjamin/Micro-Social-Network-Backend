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
            IGetAllReader<DataModel> getAllReader,
            IMapper mapper)
        {
            var response = getAllReader
                .GetAll()
                .Select(mapper.Map<ResponseDto>)
                .ToList();

            return Ok(response);
        }

        protected ActionResult<IReadOnlyCollection<ResponseDto>> Search<RequestDto, Query>(
            RequestDto searchDto,
            ISearchReader<Query, DataModel> searchReader,
            IMapper mapper)
        {
            var response = searchReader
                .Search(mapper.Map<Query>(searchDto))
                .Select(mapper.Map<ResponseDto>)
                .ToList();

            return Ok(response);
        }

        protected ActionResult<ResponseDto> Create<CreateDto, CreateCommand>(
            CreateDto dto,
            ICreateCommandHandler<CreateCommand, DataModel> createCommandHandler,
            IMapper mapper)
        {
            var command = mapper.Map<CreateCommand>(dto);
            var model = createCommandHandler.Create(command);
            var response = mapper.Map<ResponseDto>(model);

            return this.CreatedByUsingTheRequestRoute(response);
        }

        protected ActionResult<ResponseDto> Update<UpdateDto, UpdateCommand>(
            UpdateDto dto,
            IUpdateCommandHandler<UpdateCommand, DataModel> updateCommandHandler,
            IMapper mapper)
        {
            var command = mapper.Map<UpdateCommand>(dto);
            var model = updateCommandHandler.Update(command);
            var response = mapper.Map<ResponseDto>(model);

            return Ok(response);
        }

        protected ActionResult<Guid> Delete<DeleteDto, DeleteCommand>(
            DeleteDto dto,
            IDeleteCommandHandler<DeleteCommand, Guid> deleteCommandHandler,
            IMapper mapper)
        {
            var command = mapper.Map<DeleteCommand>(dto);
            var response = deleteCommandHandler.Delete(command);

            return Ok(response);
        }
        protected ActionResult<Response> DeleteWithMap<DeleteDto, DeleteCommand, CommandResponse, Response>(
            DeleteDto dto,
            IDeleteCommandHandler<DeleteCommand, CommandResponse> deleteCommandHandler,
            IMapper mapper)
        {
            var command = mapper.Map<DeleteCommand>(dto);
            var model = deleteCommandHandler.Delete(command);
            var response = mapper.Map<ResponseDto>(model);
            
            return Ok(response);
        }
    }
}