using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}
