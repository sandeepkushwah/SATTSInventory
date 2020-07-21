using log4net;
using SATTSInventory.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace SATTSInventory.Common
{
    public class RequestResponseLogHandler : DelegatingHandler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            RequestResponseLog logData = null;
            HttpResponseMessage response = null;
            try
            {
                logData = PrepareRequestLog(request);
                response = await base.SendAsync(request, cancellationToken);
                logData = PrepareResponseLog(logData, response);
            }
            catch(Exception ex)
            {
                response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                await SendToLog(ex);
            }
            finally
            {
                await SendToLog(logData);
            }

            return response;
        }

        private RequestResponseLog PrepareRequestLog(HttpRequestMessage request)
        {
            RequestResponseLog log = new RequestResponseLog
            {
                RequestMethod = request.Method?.Method,
                RequestTimestamp = DateTime.Now,
                RequestUri = request?.RequestUri?.ToString()
            };
            return log;
        }
        private RequestResponseLog PrepareResponseLog(RequestResponseLog logData, HttpResponseMessage response)
        {
            logData.ResponseStatusCode = response.StatusCode;
            logData.ResponseTimestamp = DateTime.Now;
            logData.ResponseContentType = response.Content?.Headers?.ContentType?.MediaType;
            return logData;
        }
        private async Task<bool> SendToLog<T>(T logData)
        {
            var _serialisedLogData = Newtonsoft.Json.JsonConvert.SerializeObject(logData);
            await Task.Run(() => log.Info(_serialisedLogData));
            return true;
        }
    }
}