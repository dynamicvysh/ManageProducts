using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ManageProductsTest
{
    public class RequestClient
    {
        /// <summary>
        /// Handles the http request to api and gets the response code.
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="typeOfRequest"></param>
        /// <param name="testobjectForRequest"></param>
        /// <returns></returns>
        public HttpWebResponse Request(String requestUrl, String typeOfRequest, String testobjectForRequest)
        {
            HttpWebResponse response = null;
            try
            {
                JsonServiceClient client = new JsonServiceClient(requestUrl);
                if (typeOfRequest == ConfigurationManager.AppSettings["GetRequestType"])
                {
                    response = client.Get<HttpWebResponse>(requestUrl);
                }
                else if (typeOfRequest == ConfigurationManager.AppSettings["DeleteRequestType"])
                {
                    
                    response = client.Delete<HttpWebResponse>(requestUrl);
                }
                else if (typeOfRequest == ConfigurationManager.AppSettings["PostRequestType"])
                {                   
                    response = client.Post<HttpWebResponse>(requestUrl, testobjectForRequest);
                }
                else if (typeOfRequest == ConfigurationManager.AppSettings["PutRequestType"])
                {
                    response = client.Put<HttpWebResponse>(requestUrl, testobjectForRequest);
                }

                var responseValue = string.Empty;

                if (!(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created))
                {
                    var message = String.Format("API Failed: Received status is HTTP {0}", response.StatusCode);
                    throw new Exception(message);
                }



            }
            catch (Exception ex)
            {
                var message = "Failed: Received HTTP " + ex.GetStatus();
                throw new Exception (message);
            }
            return response;
        }

    }
}
