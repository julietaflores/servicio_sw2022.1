using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Siniestro:BEEntidad
    {
        public decimal SiniestroId { get; set; }
        public string SiniestroCodigo { get; set; }
        public DateTime SiniestroFechaHoraReg { get; set; }
        public string SiniestroDescripcion { get; set; }
        public string SiniestroFoto1 { get; set; }
       public string SiniestroFoto2 { get; set; }
        public string SiniestroFoto3 { get; set; }
        public string SiniestroFoto4 { get; set; }
        public string SiniestroVideo { get; set; }
        public Boolean SiniestroCreadoPor { get; set; }
        public string ServAsigId { get; set; }

        public string SiniestroUID { get; set; }

        public List<Siniestro_Estado> Siniestro_Estado { get; set; }

        public string SiniestroDescripcion2 { get; set; }
    }
    public enum relSiniestro
    {
        siniestro_estado
    }

}
