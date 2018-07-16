using ManageProducts.Helper;
using ManageProducts.Models;
using ManageProducts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManageProducts.Controllers
{
    /// <summary>
    /// Controller for product related apis
    /// </summary>
    public class ProductsController : ApiController
    {
        /// <summary>
        /// The method returns list of all products which meets the search criteria if its met.
        /// 
        /// Returns an exception with proper message if products are not found.
        /// Returns exception for any other generic exceptions
        /// </summary>
        /// <param name="filterOptions"></param>
        /// <returns></returns>
        public List<Product> Get([FromUri] FilterOptions filterOptions)
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
                        return filteredProductList;
                    }
                    throw new Exception(Message.NoProductFound);
                }
                else
                {
                    throw new Exception(Message.NoProductFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns the product whose id is same as the id passed
        /// Returns exception with proper message if product is not found
        /// Returns exception for any other exceptions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product Get(string id)
        {
            try
            {
                Product product = CSVLoader.productList.Where(pdt => pdt.Id == id).FirstOrDefault();
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new Exception(Message.ProductNotFound);
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
        public void Post([FromBody]Product product)
        {
            try
            {
                if (CSVLoader.productList.Where(pdt => pdt.Id == product.Id).Any())
                {
                    throw new Exception(Message.ProductExists);
                }
                else
                {
                    CSVLoader.productList.Add(product);
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
        public void Put(string id, [FromBody]Product value)
        {
            try
            {
                Product product = CSVLoader.productList.Where(pdt => pdt.Id == id).FirstOrDefault();
                if (product != null)
                {
                    product.Description = value.Description;
                    product.Model = value.Model;
                    product.Brand = value.Brand;
                }
                else
                {
                    throw new Exception(Message.ProductNotFound);
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
        public void Delete(string id)
        {
            try
            {
                if (!CSVLoader.productList.Remove(CSVLoader.productList.Where(product => product.Id == id).FirstOrDefault()))
                {
                    throw new Exception(Message.ProductNotFound);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
