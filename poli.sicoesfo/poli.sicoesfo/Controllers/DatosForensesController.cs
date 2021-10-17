using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poli.sicoesfo.Domain.Entities;
using poli.sicoesfo.Domain.Filters;
using poli.sicoesfo.Infrastructure;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<object> Get(int? page, int? rowsPerPage)
        {
            page = page ?? 1;
            rowsPerPage = rowsPerPage ?? 20;
            try
            {
                var filter = new DatoForenseFilter() { ItemsPerpage = rowsPerPage.Value, Page = page.Value };
                var result = new List<DatoForenseModel>();
                var dbResult = await _unitOfWork.DatosForenses().GetAllAsync(filter);
                foreach (var item in dbResult.Data)
                {
                    result.Add(new DatoForenseModel()
                    {
                        Id = item.Id,
                        FechaMuerte = item.FechaMuerte.HasValue ? string.Format("{0:yyyy-MM-dd}", item.FechaMuerte.Value) : "",
                        HoraMuerte = item.HoraMuerte.HasValue ? new DateTime(item.HoraMuerte.Value.Ticks).ToString("HH:mm:ss"): "",
                        TipoMuerte = item.TipoMuerte,
                        Edad = item.Edad,
                        EstadoCivil = item.EstadoCivil,
                        Escolaridad = item.Escolaridad,
                        FactorVulnerabilidad = item.FactorVulnerabilidad,
                        TipoDeZona = item.TipoDeZona,
                        Escenario = item.Escenario,
                        ActividadDuranteHecho = item.ActividadDuranteHecho,
                        Circunstancia = item.Circunstancia,
                        Mecanismo = item.Mecanismo

                    });
                }

                return new { page = dbResult.Page, rowsPerPage = dbResult.RowsPerPage, totalRows = dbResult.TotalRows, data = result };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(DatoForensePostModel model)
        {
            try
            {
                DatoForense entity = new DatoForense()
                {
                    Id = Guid.NewGuid(),
                    TipoMuerte = model.TipoMuerte,
                    Edad = model.Edad,
                    EstadoCivil = model.EstadoCivil,
                    Escolaridad = model.Escolaridad,
                    FactorVulnerabilidad = model.FactorVulnerabilidad,
                    TipoDeZona = model.TipoDeZona,
                    Escenario = model.Escenario,
                    ActividadDuranteHecho = model.ActividadDuranteHecho,
                    Circunstancia = model.Circunstancia,
                    Mecanismo = model.Mecanismo
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
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}
