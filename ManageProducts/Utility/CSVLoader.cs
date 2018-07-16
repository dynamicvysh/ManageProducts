using ManageProducts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ManageProducts.Utility
{
    public  class CSVLoader
    {
        internal static List<Product> productList = new List<Product>();
        /// <summary>
        /// Loads the product to memory from a csv
        /// </summary>
        public static void LoadProducts()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("/ProductData/products.csv");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    productList.Add(new Product { Id = values[0], Description = values[1], Model = values[2], Brand = values[3] });
                }
            }
        }
    }
}