using System.Net.Http.Headers;

namespace SmartlyCodingExercise.Web.Services
{
    public class BaseService
    {
        public static async Task MakeHttpCall(Action<HttpResponseMessage> respAction, 
            string baseUrl, string controller, string routeParams, string mediaType)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                    var apiUrl = $"{baseUrl}{controller}{routeParams}";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    respAction.Invoke(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
