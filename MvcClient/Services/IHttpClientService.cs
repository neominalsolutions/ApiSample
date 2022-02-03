using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Services
{
    public interface IHttpClientService
    {
        Task<TResponse> GetAsync<TResponse>(string endpoint);
        Task<TResponse> PostAsync<TResponse,TRequest>(string endpoint, TRequest request);
    }
}
