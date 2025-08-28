using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapReader.Models
{
    public class FunctoidRequest
    {
        public FunctoidType FunctoidType { get; set; }
        public object[] Inputs { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
