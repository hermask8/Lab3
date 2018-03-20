﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3_Edwin_Ana.Models
{
    public class Partido: IComparable<Partido>
    {
        public int noPartido { get; set; }
        public string FechaPartido { get; set; }
        public string Grupo { get; set; }
        public string Pais1 { get; set; }
        public string Pais2 { get; set; }
        public string Estadio { get; set; }

        public int CompareTo(Partido other)
        {
            return this.noPartido.CompareTo(other.noPartido);
        }
    }
}