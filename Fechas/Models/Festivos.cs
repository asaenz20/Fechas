using System;
public class Festivos
{
    public int Dia { get; set; }
    public int Mes { get; set; }
    public String Nombre { get; set; }
    public int DiasPascua { get; set; }
    public int Tipo { get; set; }
    public DateTime Fecha { get; set; }

    public Festivos(int Dia, int Mes, String Nombre, int DiasPascua, int Tipo)
    {
        this.Dia = Dia;
        this.Mes = Mes;
        this.Nombre = Nombre;
        this.DiasPascua = DiasPascua;
        this.Tipo = Tipo;
    }

}