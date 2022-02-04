using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MvcClient.Extensions
{
    public class ApiResponse<TResponse>
    {
        public TResponse Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccedeed { get; set; }

        public int HttpStatusCode { get; set; }
    }

    public static class HttpClientExtensions
    {
        /// <summary>
        /// Access Token olmadan public bir endpointe istek
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="client"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
       public async static Task<ApiResponse<TResponse>> GetAsync<TResponse>(this HttpClient client, string endpoint)
        {

            var request = await client.GetAsync(endpoint);
            var response = new ApiResponse<TResponse>();
            response.HttpStatusCode = (int)request.StatusCode;


            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();

                // bir nesneyi alırken deserialize olarak alacağız.
                var model = JsonConvert.DeserializeObject<TResponse>(jsonString);
                response.Data = model;
                response.IsSuccedeed = true;

            } 
            else
            {
                response.IsSuccedeed = false;

                switch (request.StatusCode) {

                    case System.Net.HttpStatusCode.NotFound:
                        response.ErrorMessage = "Kaynak bulunamadı";
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        response.ErrorMessage = "Kaynağı görüntüleme yetkiniz yok";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        response.ErrorMessage = "Yetersi yetki";
                        break;
                    case System.Net.HttpStatusCode.RequestTimeout:
                        response.ErrorMessage = "Sunucu ile bağlantı düştü";
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        response.ErrorMessage = "Sunucuda hata oluştu";
                        break;
                    default:
                        response.ErrorMessage = "Bilinmeyen Hata";
                        break;

                }


            }


            return  await Task.FromResult(response);          


        }


        /// <summary>
        /// Access Token olmadan Post isteği atar
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="client"></param>
        /// <param name="endPoint"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async static Task<ApiResponse<TResponse>> PostAsync<TParam, TResponse>(this HttpClient client, string endPoint, TParam @param)
        {
            var jsonString = JsonConvert.SerializeObject(@param);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var request = await client.PostAsync(endPoint, stringContent);
            var response = new ApiResponse<TResponse>();
            response.HttpStatusCode = (int)request.StatusCode;


            if (request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<TResponse>(content);
                response.Data = data;
                response.IsSuccedeed = true;
            }
            else
            {
                response.IsSuccedeed = false;

                switch (request.StatusCode)
                {

                    case System.Net.HttpStatusCode.NotFound:
                        response.ErrorMessage = "Kaynak bulunamadı";
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        response.ErrorMessage = "Kaynağı görüntüleme yetkiniz yok";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        response.ErrorMessage = "Yetersi yetki";
                        break;
                    case System.Net.HttpStatusCode.RequestTimeout:
                        response.ErrorMessage = "Sunucu ile bağlantı düştü";
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        response.ErrorMessage = "Sunucuda hata oluştu";
                        break;
                    default:
                        response.ErrorMessage = "Bilinmeyen Hata";
                        break;
                }

            }

            return await Task.FromResult(response);
        }


        /// <summary>
        /// Bearer Token implemente edilmiş hali
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="client"></param>
        /// <param name="endPoint"></param>
        /// <param name="param"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task<ApiResponse<TResponse>> PostAsync<TParam, TResponse>(this HttpClient client, string endPoint, TParam @param, HttpContext httpContext)
        {
            // await yerine GetAwaiter ve GetResult methodları ile async bir kodu senkron hale getirip propertylerine erişebiliriz.
            var accessToken = httpContext.AuthenticateAsync().GetAwaiter().GetResult()?.Properties?.GetTokenValue("AccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                // bir üsteki versiyonu çalıştır.
               return await Task.FromResult(await client.PostAsync<TParam,TResponse>(endPoint, @param));
            }

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // yukarıdaki kod ile request header'a access token ekledik.

            return await Task.FromResult(await client.PostAsync<TParam, TResponse>(endPoint, @param));
        }
    }
}
