﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PersonaDireccion:BEEntidad
    {
        public decimal PersonaId { get; set; }
        public decimal PersonaDireccionId { get; set; }
        public decimal TipoDireccionId { get; set; }
        public string PersonaDireccionTitulo { get; set; }
        public string PersonaDireccionGeo { get; set; }
        public string PersonaDireccionDescripcion { get; set; }
        public decimal CiudadDireccionId { get; set; }
        public System.DateTime PersonaDireccionFHMod { get; set; }
        public string PersonaDireccionUsuarioMod { get; set; }
        public decimal EstadoDireccionId { get; set; }
        public string PersonaDireccionDireccion { get; set; }
    }
}
