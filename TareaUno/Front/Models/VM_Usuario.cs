using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Models
{
    public class VM_Usuario
    {
        public VM_Usuario()
        {
            this.IdUsuario = 0;
            this.Mail = string.Empty;
            this.Password = string.Empty;
        }
        public int IdUsuario { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}