using System.Collections.Generic;
using MapReader.Models;
namespace MapReader.Interfaces
{
    public interface IFunctoidHandler
    {
        FunctoidType Type { get; }
        object Execute(object[] inputs, IDictionary<string, object> parameters);
    }
}
