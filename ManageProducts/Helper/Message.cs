using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageProducts.Helper
{
    /// <summary>
    /// Contains messages for the api
    /// </summary>
    public class Message
    {
        public const string NoProductFound = "No product found.";
        public const string ProductNotFound = "Product not found.";
        public const string ProductExists = "Product already exist with same id.";
    }
}