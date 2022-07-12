using BE;
using System;




public class BEEntidad
{
    private TipoEstado enuTipoEstado = BE.TipoEstado.SinAccion;


    public TipoEstado TipoEstado
    {
        get
        {
            return enuTipoEstado;
        }
        set
        {
            enuTipoEstado = value;
        }
    }



    public BEEntidad Clonar()
    {
        return (BEEntidad)this.MemberwiseClone();
    }
}