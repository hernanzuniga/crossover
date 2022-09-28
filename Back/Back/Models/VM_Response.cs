using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back.Models
{
    public class VM_Response
    {
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public object Resultado { get; set; }
    }
}