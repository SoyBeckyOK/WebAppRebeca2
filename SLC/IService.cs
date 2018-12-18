using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Entities;
namespace SLC
{
    public interface IService
    {
        Products CreateProduct(Products newProduct);
        List<Products> GetAllProducts();
        Products RetrieveProductByID(int ID);
        HttpResponseMessage UpdateProduct(int id, Products productToUpdate);
        void RemoveProduct(int ID);
        List<Products> FilterProductByIDCategory(int categoryID);
    }
}
