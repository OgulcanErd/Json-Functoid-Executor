using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapReader.Models;
using MapReader.Interfaces;

namespace MapReader.Handlers
{
    public class LogicalNotHandler : IFunctoidHandler
    {
        public FunctoidType Type => FunctoidType.LogicalNot;

        public object Execute(object[] inputs, IDictionary<string, object> parameters)
        {
            if (inputs.Length == 0) return false;

            if (bool.TryParse(inputs[0]?.ToString(), out var val))
                return !val;

            return false;
        }
    }

}
