using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        public int IdColonia { get; set; }
        public string Nombre { get; set; }
        public List<Object> Colonias { get; set; }
        public Municipio Municipio { get; set; }

    }
}
