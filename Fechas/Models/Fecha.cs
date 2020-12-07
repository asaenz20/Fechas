using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Fechas.Models
{
    public class Fecha
    {
        [Required]
        public DateTime fecha { get; set; }
        public String festivo { get; set; }
        
        public Fecha(int año,int mes,int dia)
        {
            this.fecha = new DateTime(año, mes, dia);
        }
    }
}