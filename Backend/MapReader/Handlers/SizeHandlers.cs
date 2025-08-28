using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MapReader.Models;
using MapReader.Interfaces;

namespace MapReader.Handlers
{
    public class SizeHandler : IFunctoidHandler
    {
        public FunctoidType Type => FunctoidType.Size;

        public object Execute(object[] inputs, IDictionary<string, object> parameters)
        {
            if (inputs == null || inputs.Length == 0 || inputs[0] == null)
                return 0;

            var input = inputs[0];

            if (input is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.String)
                {
                    var str = jsonElement.GetString();
                    return str?.Length ?? 0;
                }

                if (jsonElement.ValueKind == JsonValueKind.Array)
                {
                    return jsonElement.EnumerateArray().Count();
                }
            }

            if (input is string strInput)
            {
                return strInput.Length;
            }

            if (input is Array arr)
            {
                return arr.Length;
            }

            if (input is IEnumerable<object> list)
            {
                return list.Count();
            }

            return 0;
        }
    }
}
