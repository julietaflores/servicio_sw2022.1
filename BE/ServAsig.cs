using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace BE
{
    public class ServAsig : BEEntidad
    {
        public string ServAsigId { get; set; }
        public decimal ProveedorId { get; set; }
        public System.DateTime ServAsigFHUbicacion { get; set; }
        public System.DateTime ServAsigFHEstimadaLlegada { get; set; }
        public Nullable<System.DateTime> ServAsigFHInicio { get; set; }
        public Nullable<System.DateTime> ServAsigFHFin { get; set; }
        public Nullable<System.DateTime> ServAsigFHPago { get; set; }
        public Nullable<decimal> ServAsigCostoTotal { get; set; }
        public Nullable<decimal> StatusServAsigId { get; set; }
        public string RequiereServicioId { get; set; }
        public Nullable<bool> ServAsigPagaCliente { get; set; }
        public Nullable<bool> servAsigCalificado { get; set; }
        public StatusServAsig statusServAsig { get; set; }
        public List<ServAsigCosto> servAsigCosto { get; set; }
        public Post post { get; set; }
        public List<Post> post1 { get; set; }
        public RequiereServicio requiereServicio { get; set; }
        public Persona proveedor { get; set; }


    }
    public enum relServAsig
    {
        requiereServicio, statusServAsig, servAsigCosto, post,proveedor
    }
 
}
