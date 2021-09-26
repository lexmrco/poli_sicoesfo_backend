﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poli.sicoesfo.Domain.Entities;
using poli.sicoesfo.Domain.Filters;
using poli.sicoesfo.Infrastructure;
using poli.sicoesfo.Infrastructure.Domain.Repositories;
using poli.sicoesfo.Models;

namespace poli.sicoesfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosForensesController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;
        public DatosForensesController(ILogger<DatosForensesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IEnumerable<DatoForenseModel>> Get()
        {
            try
            {
                var filter = new DatoForenseFilter();
                var result = new List<DatoForenseModel>();
                var data = await _unitOfWork.DatosForenses().GetAllAsync(filter);
                foreach (var item in data)
                {
                    result.Add(new DatoForenseModel()
                    {
                        Id = item.Id,
                        FechaMuerte = item.FechaMuerte.HasValue ? string.Format("{0:yyyy-MM-dd}", item.FechaMuerte.Value) : "",
                        HoraMuerte = item.HoraMuerte.HasValue ? new DateTime(item.HoraMuerte.Value.Ticks).ToString("HH:mm:ss"): ""
                    });
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DatoForensePostModel model)
        {
            try
            {
                DatoForense entity = new DatoForense()
                {
                    Id = Guid.NewGuid(),
                    FechaMuerte = DateTime.Parse(model.FechaMuerte),
                };
                if (!string.IsNullOrEmpty(model.FechaMuerte))
                {
                    entity.FechaMuerte = DateTime.Parse(model.FechaMuerte);
                }
                if (!string.IsNullOrEmpty(model.HoraMuerte))
                {
                    entity.HoraMuerte = TimeSpan.Parse(model.HoraMuerte);
                }
                await _unitOfWork.DatosForenses().CreateAsync(entity);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}