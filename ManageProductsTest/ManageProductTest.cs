using System;
using System.Configuration;
using System.Net;
using ManageProductsTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManageProductsTest
{
    [TestClass]
    public class ManageProductTest
    {
        RequestTrigger requestTrigger;
        public ManageProductTest()
        {
            requestTrigger = new RequestTrigger();
        }

        /// <summary>
        /// Tests the product get api
        /// </summary>
        [TestMethod]
        public void TestGetProducts()
        {
            string getURL = ConfigurationManager.AppSettings["BaseUrl"];
            requestTrigger.CheckStatus(requestTrigger.TriggerRequest(getURL, ConfigurationManager.AppSettings["GetRequestType"], ""));
        }

        /// <summary>
        /// Tests the product post api
        /// </summary>
        [TestMethod]
        public void TestPostProduct()
        {
            string url = ConfigurationManager.AppSettings["BaseUrl"] + "/" + ConfigurationManager.AppSettings["ProductIdToPost"];
            try
            {
                requestTrigger.TriggerRequest(url, ConfigurationManager.AppSettings["DeleteRequestType"], "");
            }
            catch (Exception ex)
            {
            }
            requestTrigger.CheckStatus(requestTrigger.TriggerRequest(url, ConfigurationManager.AppSettings["PostRequestType"], ConfigurationManager.AppSettings["PostProduct"]));
        }

        /// <summary>
        /// Tests the product delete api
        /// </summary>
        [TestMethod]
        public void TestDeleteProduct()
        {
            string url = ConfigurationManager.AppSettings["BaseUrl"] + "/" + ConfigurationManager.AppSettings["ProductIdToDelete"];
            string postUrl = ConfigurationManager.AppSettings["BaseUrl"];
            // Creates a product before deleting it. this helps to avoid product not found scenario.
            try
            {
                requestTrigger.TriggerRequest(postUrl, ConfigurationManager.AppSettings["PostRequestType"], ConfigurationManager.AppSettings["ProductToDelete"]);
            }
            catch (Exception ex)
            {

            }
            requestTrigger.CheckStatus(requestTrigger.TriggerRequest(url, ConfigurationManager.AppSettings["DeleteRequestType"], ""));
        }

        /// <summary>
        /// Tests the get producty by id api
        /// </summary>
        [TestMethod]
        public void TestGetProductsById()
        {
            string url = ConfigurationManager.AppSettings["BaseUrl"] + "/" + ConfigurationManager.AppSettings["ProductIdToGet"];
            requestTrigger.CheckStatus(requestTrigger.TriggerRequest(url, ConfigurationManager.AppSettings["GetRequestType"], ""));
        }

        /// <summary>
        /// Tests the put (update) product api.
        /// </summary>
        [TestMethod]
        public void TestPutProduct()
        {
            string url = ConfigurationManager.AppSettings["BaseUrl"] + "/" + ConfigurationManager.AppSettings["ProductIdToUpdate"];
            requestTrigger.CheckStatus(requestTrigger.TriggerRequest(url, ConfigurationManager.AppSettings["PutRequestType"], ConfigurationManager.AppSettings["PutProduct"]));
        }
    }
}
