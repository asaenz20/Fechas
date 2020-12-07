using Fechas.Datos;
using Fechas.Models;
using System;
using Fechas.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fechas.Controllers
{
    public class FechaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Festivo(int año,int mes,int dia) 
        {
            string fecha = año + "/" + mes + "/" + dia;
            bool b=IsDateTime(fecha);
            if (b) 
            {
                Fecha f = new Fecha(año, mes, dia);
                f.festivo= Datos.Festivo.Fest(f);
                return Ok(f.festivo);
            }
            else 
            {
                return Ok("No es una fecha valida :(");
            }
        }
        public static bool IsDateTime(String txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate);
        }

    }
}
