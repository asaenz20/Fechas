using Fechas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace Fechas.Datos
{
    public class Festivo
    {
        public static String Fest(Fecha f)
        {
            String mensaje = "";
            int dia;
            int mes;
            int año = f.fecha.Year;
            String motivo = "";
            int tipo;
            int diasPascua;
          
            //Calculo de cuando es domingo de ramos dependiendo del año
            int a = f.fecha.Year % 19;
            int b = f.fecha.Year % 4;
            int c = f.fecha.Year % 7;
            int d = ((19 * a) + 24) % 30;
            int Dias = d + (2 * b + 4 * c + 6 * d + 5) % 7;
            DateTime QuinceMarzo = new DateTime(f.fecha.Year, 03, 15);
            DateTime DomingoRamos = QuinceMarzo.AddDays(Dias);
            DateTime Pascua = DomingoRamos.AddDays(7);

            //Carga del archivo XML
            String archivoDatos = HttpContext.Current.Server.MapPath("~/Datos/Festivos.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(archivoDatos);
            //Trae todos los nodos con dias festivos registrados
            XmlNodeList nodos = doc.SelectNodes("Festivos/Festivo");

            //Estructura de la clase List donde se guardaran dinamicamente los datos de la tabla
            List<Festivos> party = new List<Festivos>();

            //Se pregunta si la consulta recupero registros de dias festivos
            if (nodos!=null && nodos.Count > 0)
            {
                //Bucle que itera sobre todos los dias festivos
                for (int i = 0; i < nodos.Count; i++)
                {
                    //Bucle que itera sobre el dia,mes,motivo,tipo y dias de pascua del archivo XML
                    for (int j = 0; j < nodos[i].ChildNodes.Count; j++)
                    {
                        if (nodos[i].ChildNodes[0].InnerText.Equals(""))
                        {
                            dia = 0;
                        }
                        else
                        {
                           dia = Int32.Parse(nodos[i].ChildNodes[0].InnerText);
                        }
                        if (nodos[i].ChildNodes[0].InnerText.Equals(""))
                        {
                            mes = 0;
                        }
                        else
                        {
                            mes = Int32.Parse(nodos[i].ChildNodes[1].InnerText);
                        }
                        motivo = nodos[i].ChildNodes[2].InnerText;
                        tipo = Int32.Parse(nodos[i].ChildNodes[3].InnerText);
                        if (nodos[i].ChildNodes[4].InnerText.Equals(""))
                        {
                            diasPascua = 0;
                        }
                        else
                        {
                            diasPascua = Int32.Parse(nodos[i].ChildNodes[4].InnerText);
                        }
                            party.Add(new Festivos(dia, mes, motivo, diasPascua, tipo));
                    }
                }
                //Ya termino de cargar todos los elementos de la tabla en una lista con los dias festivos como vinieron

                //Se itera sobre los elementos de la lista con los festivos para las respectivas modificaciones
                foreach (Festivos fe in party)
                {

                    if (fe.Tipo == 1)
                    { 
                       fe.Fecha= new DateTime(año, fe.Mes, fe.Dia);
                    }
                    if(fe.Tipo == 2)
                    {
                        fe.Fecha= new DateTime(año, fe.Mes, fe.Dia);
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Sunday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(1);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Saturday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(2);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Friday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(3);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Thursday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(4);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Wednesday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(5);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Tuesday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(6);
                        }
                        fe.Dia = fe.Fecha.Day;
                        fe.Mes = fe.Fecha.Month;
                    }
                    if (fe.Tipo == 3)
                    {
                        if (fe.DiasPascua == 0)
                        {
                            fe.Fecha = Pascua;
                        }
                        else
                        {
                            fe.Fecha = Pascua.AddDays(fe.DiasPascua);
                        }
                        fe.Dia = fe.Fecha.Day;
                        fe.Mes = fe.Fecha.Month;
                    }
                    if(fe.Tipo == 4)
                    {
                        fe.Fecha = Pascua.AddDays(fe.DiasPascua);
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Sunday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(1);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Saturday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(2);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Friday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(3);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Thursday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(4);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Wednesday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(5);
                        }
                        if (fe.Fecha.DayOfWeek.ToString().Equals("Tuesday"))
                        {
                            fe.Fecha = fe.Fecha.AddDays(6);
                        }
                        fe.Dia = fe.Fecha.Day;
                        fe.Mes = fe.Fecha.Month;
                    }  
                }
                //Como ya se tienen los respectivos dias festivos, se mira si la fecha administrada es festivo o no
                foreach (Festivos fe in party)
                {
                    if (fe.Fecha == f.fecha)
                     {
                        mensaje = "Si es festivo!!!";
                    }
                 }
                 if (mensaje.Equals(""))
                 {
                     return "No es festivo :(";
                 }
                 else
                 {
                     return mensaje;
                 }
            }
            else
            {
                return "La consulta no recupero registros";
            }         
        }
    }
   
}