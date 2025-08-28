using System;
using System.Collections.Generic;
using MapReader.Interfaces;
using MapReader.Models;
using System.Text.Json;

namespace MapReader.Handlers
{
    public class StringLeftOrRight : IFunctoidHandler
    {
        public FunctoidType Type => FunctoidType.StringLeftOrRight;

        public object Execute(object[] inputs, IDictionary<string, object> parameters)
        {
            if (inputs == null || inputs.Length == 0 || inputs[0] == null)
                return null;

            var str = inputs[0].ToString();

            if (parameters == null || !parameters.ContainsKey("Count"))
                return null;

            int length = 0;
            object countObj = parameters["Count"];

            if (countObj is JsonElement jeCount && jeCount.ValueKind == JsonValueKind.Number)
            {
                length = jeCount.GetInt32();
            }
            else if (int.TryParse(countObj.ToString(), out int parsedLength))
            {
                length = parsedLength;
            }
            else
            {
                return null;
            }

            string direction = "right";
            if (parameters.ContainsKey("Direction"))
            {
                var dirObj = parameters["Direction"];
                if (dirObj is JsonElement jeDir && jeDir.ValueKind == JsonValueKind.String)
                    direction = jeDir.GetString()?.ToLower() ?? "right";
                else
                    direction = dirObj?.ToString()?.ToLower() ?? "right";
            }

            if (length > str.Length)
                length = str.Length;

            switch (direction)
            {
                case "left":
                    return str.Substring(0, length);
                case "right":
                default:
                    return str.Substring(str.Length - length);
            }
        }
    }
}
