using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapReader.Interfaces;
using MapReader.Models;

namespace MapReader.Handlers
{
    public class EqualHandler : IFunctoidHandler
    {
        public FunctoidType Type => FunctoidType.Equal;

        public object Execute(object[] inputs, IDictionary<string, object> parameters)
        {
            if (inputs.Length < 2) return false;
            return inputs[0]?.ToString() == inputs[1]?.ToString();
        }
    }

}
