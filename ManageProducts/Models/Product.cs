using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageProducts.Models
{
    /// <summary>
    /// Holds the product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id of the product
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Description of the product
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Model of the product
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Brand of the product
        /// </summary>
        public string Brand { get; set; }
    }
}