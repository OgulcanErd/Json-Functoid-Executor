using System.Collections.Generic;
using MapReader.Interfaces;
using MapReader.Models;

namespace MapReader.Handlers
{
    public class StringTrim : IFunctoidHandler
    {
        public FunctoidType Type => FunctoidType.StringTrim;

        public object Execute(object[] inputs, IDictionary<string, object> parameters)
        {
            if (inputs == null || inputs.Length == 0 || inputs[0] == null)
                return null;

            var input = inputs[0].ToString();

            string direction = null;

            if (parameters != null && parameters.TryGetValue("Direction", out var dirValue))
                direction = dirValue?.ToString()?.ToLower();

            switch (direction)
            {
                case "left":
                    return input.TrimStart();
                case "right":
                    return input.TrimEnd();
                case "both":
                case null:
                default:
                    return input.Trim();
            }
        }
    }
}
