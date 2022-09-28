using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back.Models
{
    public class VM_Usuario
    {
        public int IdUsuario { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}