using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ManageProductsTest.Helper
{
    public class RequestTrigger
    {
        /// <summary>
        /// Triggers the http request
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="type"></param>
        /// <param name="testObject"></param>
        /// <returns></returns>
        public int TriggerRequest(string requestUrl, string type, string testObject)
        {
            var client = new RequestClient();
            HttpWebResponse response = (HttpWebResponse)client.Request(requestUrl, type, testObject);
            return Convert.ToInt32(response.StatusCode);
        }

        /// <summary>
        /// Checks the response code to verify whether the api call is successful or not. 
        /// </summary>
        /// <param name="actualStatusCode"></param>
        public void CheckStatus(int actualStatusCode)
        {
            if (!(actualStatusCode == 200 || actualStatusCode == 204))
            {
                throw new Exception("Status code is not 200 OK");
            }
        }
    }
}
