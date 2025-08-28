using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapReader.Models;
using MapReader.Interfaces;

namespace MapReader.Services
{
    public class FunctoidHandlerFactory
    {
        private readonly Dictionary<FunctoidType, IFunctoidHandler> _handlers;
        public FunctoidHandlerFactory(IEnumerable<IFunctoidHandler> handlers) =>
            _handlers = handlers.ToDictionary(h => h.Type);

        public IFunctoidHandler GetHandler(FunctoidType type) =>
            _handlers.TryGetValue(type, out var h)
              ? h
              : throw new NotSupportedException($"Unsupported: {type}");
    }

}
