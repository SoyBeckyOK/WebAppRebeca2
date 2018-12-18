using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace WebAppRebeca2.Models
{
    public class RepositoryMemoryData : IRepositoryProducts
    {
        private List<Products> Products = new List<Products>();
        private static int NextID;

        public RepositoryMemoryData()
        {

            Products.Add(new Products { ProductID = 1, Nombre = "Avestruz", CategoryID = 1, Precio = 350, Existencias = 3 });
            Products.Add(new Products { ProductID = 2, Nombre = "Jirafa", CategoryID = 2, Precio = 7350, Existencias = 1 });
            Products.Add(new Products { ProductID = 3, Nombre = "Leon", CategoryID = 3, Precio = 1550, Existencias = 3 });
            Products.Add(new Products { ProductID = 4, Nombre = "Anaconda", CategoryID = 4, Precio = 450, Existencias = 4});
            Products.Add(new Products { ProductID = 5, Nombre = "Lechuza Blanca", CategoryID = 1, Precio = 550, Existencias = 5 });
        }

        public Products CreateProduct(Products products)
        {

            if (products == null)
            {
                throw new ArgumentException("Producto vacío");
            }
            products.ProductID = ++NextID;
            Products.Add(products);
            return products;
        }

        public void RemoveProducts(int id)
        {
            Products.RemoveAll(p => p.ProductID == id);
        }

        public List<Products> GetAllProducts()
        {
            return Products;
        }

        public Products GetProducts(int id)
        {
            return Products.Find(p => p.ProductID == id);
        }

        public bool UpdateProducts(Products products)
        {
            if (products == null)
            {
                throw new ArgumentException("Product");
            }

            int index = Products.FindIndex(p => p.ProductID == products.ProductID);
            if (index == -1)
            {
                return false;
            }
            Products.RemoveAt(index);
            Products.Add(products);
            return true;
        }
    }
}