using CBA_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CBA_app.Request
{
    internal interface IRequest
    {
        Task<JsonNode> requestWithPromise(string jsonData, string urlApi);
    }
}
