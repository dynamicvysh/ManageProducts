using ManageProducts.Helper;
using ManageProducts.Models;
using ManageProducts.Utility;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ManageProducts.Controllers
{
    /// <summary>
    /// Controller for product related apis
    /// </summary>
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Returns list of all products which meets the search criteria if its met.
        /// </summary>
        /// <param name="filterOptions"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, "Returns the list of products", typeof (IEnumerable<Product>))]
        public IHttpActionResult Get([FromUri] FilterOptions filterOptions)
        {
            try
            {
                // filteredProductList is loaded with all items in the CSV first
                // then if brand is passed as a query paramter, then the filteredProductList items are reduced by applying the brand filter
                // then if model is passed as query parameter, then the filteredProductList items are reduced by applying the model filter
                // at last if filteredProductList doesnot have any product, then 'No Product Found' exception is send else the filtered list of items are returned.

                List<Product> filteredProductList = CSVLoader.productList;
                if (CSVLoader.productList?.Count > 0)
                {
                    if (filterOptions.Brand != null)
                    {
                        filteredProductList = filteredProductList.Where(product => product.Brand.ToLower() == filterOptions.Brand.ToLower()).ToList();
                    }
                    if (filterOptions.Model != null)
                    {
                        filteredProductList = filteredProductList.Where(product => product.Model.ToLower() == filterOptions.Model.ToLower()).ToList();
                    }
                    if (filteredProductList.Count > 0)
                    {
                        return Ok(filteredProductList);
                    }
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, Message.ProductNotFound);
                    throw new HttpResponseException(response);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, Message.ProductNotFound);
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns the product whose id is same as the id passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, "Returns the list of products", typeof(Product))]
        public IHttpActionResult Get(string id)
        {
            try
            {
                Product product = CSVLoader.productList.Where(pdt => pdt.Id == id).FirstOrDefault();
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, Message.ProductNotFound);
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adds new product to the list of products
        /// </summary>
        /// <param name="product"></param>
        [SwaggerResponse(HttpStatusCode.OK, "Returns the list of products", typeof(Product))]
        public IHttpActionResult Post([FromBody]Product product)
        {
            try
            {
                if (CSVLoader.productList.Where(pdt => pdt.Id == product.Id).Any())
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Conflict,Message.ProductExists);
                    throw new HttpResponseException(response);
                }
                else
                {
                    CSVLoader.productList.Add(product);
                    return Created<Product>("Product", product);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Updates product based on id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public IHttpActionResult Put(string id, [FromBody]Product value)
        {
            try
            {
                Product product = CSVLoader.productList.Where(pdt => pdt.Id == id).FirstOrDefault();
                if (product != null)
                {
                    product.Description = value.Description;
                    product.Model = value.Model;
                    product.Brand = value.Brand;
                    return Ok();
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, Message.ProductNotFound);
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Deletes a product based on id
        /// </summary>
        /// <param name="id"></param>
        public IHttpActionResult Delete(string id)
        {
            try
            {
                if (!CSVLoader.productList.Remove(CSVLoader.productList.Where(product => product.Id == id).FirstOrDefault()))
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, Message.ProductNotFound);
                    throw new HttpResponseException(response);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
