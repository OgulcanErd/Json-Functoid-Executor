    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapReader.Models;

namespace MapReader.Services
{
    public class MapExecutor
    {
        private readonly FunctoidHandlerFactory _factory;
        public MapExecutor(FunctoidHandlerFactory factory) => _factory = factory;

        public object ExecuteFunctoid(FunctoidType type, object[] inputs, IDictionary<string, object> parameters)
        {
            var handler = _factory.GetHandler(type);
            return handler.Execute(inputs, parameters);
        }
    }
}
