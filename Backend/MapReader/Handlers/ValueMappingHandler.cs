using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapReader.Models;
using MapReader.Interfaces;

namespace MapReader.Handlers
{
    public class ValueMappingHandler : IFunctoidHandler
    {
        public FunctoidType Type => FunctoidType.ValueMapping;

        public object Execute(object[] inputs, IDictionary<string, object> parameters)
        {
            if (inputs == null || inputs.Length == 0)
                return null;

            var inputValue = inputs[0]?.ToString();
            var condition = parameters.ContainsKey("Condition") ? parameters["Condition"]?.ToString() : null;
            var trueValue = parameters.ContainsKey("TrueValue") ? parameters["TrueValue"] : null;
            var falseValue = parameters.ContainsKey("FalseValue") ? parameters["FalseValue"] : null;

            return inputValue == condition ? trueValue : falseValue;
        }
    }

}
