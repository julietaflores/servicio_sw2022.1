using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public class Servicio : BEEntidad
    {
        public decimal ServicioId { get; set; }
        public string ServicioNombre { get; set; }
        public string ServicioURLFoto { get; set; }
        public decimal CategoriaServicioId { get; set; }
        public string ServicioUsuario { get; set; }
        public System.DateTime ServicioFechaHoraMod { get; set; }
        public string ServicioKeyWords { get; set; }
        public Nullable<decimal> ServicioPorcentaje { get; set; }
        public Nullable<decimal> ServicioTarifaMinima { get; set; }
        public bool servicioDetalleTipo { get; set; }
        public bool servicioSabado { get; set; }
        public bool servicioDomingo { get; set; }
        public DateTime servicioHorarioRegularIni { get; set; }
        public DateTime servicioHorarioRegularFin { get; set; }
        public DateTime servicioHorarioSabadoIni { get; set; }
        public DateTime servicioHorarioSabadoFin { get; set; }
        public DateTime servicioHorarioDomingoIni { get; set; }
        public DateTime servicioHorarioDomingoFin { get; set; }
        public decimal servicioPersonaEnTurno { get; set; }
        public string servicioDetalleFormulario { get; set; }
        public decimal tipoServicioId { get; set; }
        public decimal servicioTarifaPlana { get; set; }
        public int nroProveedores { get; set; }
        public decimal ServicioHoras { get; set; }

        public string ServicioDescripcion { get; set; }


        public decimal servicioTarifaInsumos_Extras { get; set; }
        public CategoriaServicio categoriaServicio { get; set; }
        public ServicioRequerimiento servicioRequerimiento { get; set; }
        public ServicioTexto servicioTexto { get; set; }
        public List<servicioDetalle> servicioDetalle { get; set; }
        public ServicioDescripcion servicioDescripcion { get; set; }
    }

    public enum relServicio
    {
        categoriaServicio,servicioRequerimiento,servicioDetalle,servicioTexto, servicioDescripcion
    }

}
