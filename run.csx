using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("ACR WebHook Posted");

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    Uri _uri = new Uri("https://hooks.slack.com/services/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");

    string _message = data?.ToString() ?? "test from azure function";
    string _channel = "#imageupdates";
    string _username = "webhookbot";

    HttpClient _httpClient = new HttpClient();
    var payload = new
    {
        text = _message,
        _channel,
        _username,
    };
    var serializedPayload = JsonConvert.SerializeObject(payload);
    var response = _httpClient.PostAsync(_uri,
        new StringContent(serializedPayload, Encoding.UTF8, "application/json")).Result;

    return _message == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, response.ToString())
        : req.CreateResponse(HttpStatusCode.OK, String.Format("Message: {0}, Posted to: {1}", _message, _channel));

}
