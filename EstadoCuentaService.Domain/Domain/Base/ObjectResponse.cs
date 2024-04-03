using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Domain.Domain.Base
{
    public class ObjectResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T item { get; set; }
    }
}
