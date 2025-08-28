using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MapReader.Models;
using MapReader.Services;

namespace MapReader.Executors
{
    public class PipelineExecutor
    {
        private readonly MapExecutor _mapExecutor;

        public PipelineExecutor(MapExecutor mapExecutor)
        {
            _mapExecutor = mapExecutor;
        }

        public object ExecutePipeline(string json)
        {
            var steps = JsonSerializer.Deserialize<List<FunctoidRequest>>(json);
            if (steps == null || steps.Count == 0)
                throw new ArgumentException("No functoid steps provided.");

            object currentOutput = steps[0].Inputs?.FirstOrDefault() ?? "";

            foreach (var step in steps)
            {
                object[] inputs = (step.Inputs == null || step.Inputs.Length == 0)
                    ? new object[] { currentOutput }
                    : step.Inputs;

                var parameters = step.Parameters ?? new Dictionary<string, object>();

                // Debug loglama
                Console.WriteLine($"Executing {step.FunctoidType}:");
                Console.WriteLine($"Inputs: [{string.Join(", ", inputs.Select(i => i?.ToString() ?? "null"))}]");
                Console.WriteLine("Parameters:");
                foreach (var param in parameters)
                {
                    Console.WriteLine($"  {param.Key}: {JsonElementToString(param.Value)}");
                }

                currentOutput = _mapExecutor.ExecuteFunctoid(step.FunctoidType, inputs, parameters);

                Console.WriteLine($"Result: {currentOutput}");
                Console.WriteLine(new string('-', 40));
            }

            return currentOutput;
        }

        private string JsonElementToString(object obj)
        {
            if (obj is JsonElement je)
            {
                switch (je.ValueKind)
                {
                    case JsonValueKind.String:
                        return je.GetString();
                    case JsonValueKind.Number:
                        return je.GetRawText();
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        return je.GetBoolean().ToString();
                    case JsonValueKind.Null:
                        return "null";
                    default:
                        return je.GetRawText();
                }
            }

            return obj?.ToString() ?? "null";
        }
    }
}
