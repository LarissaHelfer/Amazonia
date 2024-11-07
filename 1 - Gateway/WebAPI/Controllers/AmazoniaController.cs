using API.Application.Dto;
using API.Application.Dto.Request;
using API.Application.Dto.Response;
using API.Application.Users;
using API.Domain.Notifications;
using API.WebApi.Controllers.Base;
using Application.AmazoniaHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace API.WebApi.Controllers
{
    public class AmazoniaController : BaseController
    {
        private readonly AmazoniaHandler _handler;
        private readonly IConfiguration _configuration;

        public AmazoniaController(AmazoniaHandler handler, IConfiguration configuration, UserHandler userHandler, INotificationHandler notificationHandler)
            : base(userHandler, notificationHandler)
            {
                _handler = handler ?? throw new ArgumentNullException(nameof(handler));
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            }

        [HttpGet]
        [Route("amazonia/gellAllArvores")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _handler.GetAll();

            if (result == null)
            {
                return BadRequest("Erro ao retornar os dados.");
            }

            return Ok(new OkDto<List<ArvoreDto>>(result));
        }

        [HttpGet]
        [Route("amazonia/getArvoreById")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _handler.GetById(id);

            if (result == null)
            {
                return BadRequest("Erro ao retornar os dados.");
            }

            return Ok(new OkDto<ArvoreDto>(result));
        }

        [HttpPost]
        [Route("amazonia/updateArvore")]
        public async Task<ActionResult> Update(ArvoreDto dto)
        {
            var result = await _handler.Update(dto);

            if (result == null)
            {
                return BadRequest("Erro ao retornar os dados.");
            }

            return Ok(new OkDto<bool>(result));
        }

        [HttpPost]
        [Route("amazonia/removerArvore")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _handler.Delete(id);

            if (result == null)
            {
                return BadRequest("Erro ao retornar os dados.");
            }

            return Ok(new OkDto<bool>(result));
        }

    }
}
