using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppRebeca2.Models;

namespace WebAppRebeca2.Controllers
{
    public class ProductController : ApiController, IService
    {
        static IRepositoryProducts Repository = new RepositoryMemoryData();

        [HttpPost]
        public Products CreateProduct(Products products)
        {

            var product = Repository.CreateProduct(products);
            return product;
        }

        [HttpGet]
        public  List<Products> GetAllProducts()
        {
            return Repository.GetAllProducts();
        }

        [HttpGet]
        public Products RetrieveProductByID(int id)
        {
            return Repository.GetProducts(id);
        }

        [HttpPut]
        public void UpdateProduct(int id, Products products)
        {
            products.ProductID = id;
            if (!Repository.UpdateProducts(products))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

        }
        //DELETE api/product/removeProduct?id={id}
        //DETELE api/product/removeProduct/id
        [HttpDelete]
        public void RemoveProduct(int id)
        {
            Entities.Products ProductToDelete = Repository.GetProducts(id);
            if (ProductToDelete == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Repository.RemoveProducts(ProductToDelete.ProductID);
        }

        [HttpGet]
        public List<Products> FilterProductByIDCategory(int catID)
        {
            
            return Repository.GetAllProducts().Where(p => p.CategoryID == catID).ToList();

        }
    }
}
