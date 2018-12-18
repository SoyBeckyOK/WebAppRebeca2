using System;
using System.Collections.Generic;
using System.Text;
using Entities;
namespace SLC
{
    public interface IService
    {
        Products CreateProduct(Products newProduct);
        List<Products> GetAllProducts();
        Products RetrieveProductByID(int ID);
        void UpdateProduct(int id, Products productToUpdate);
        void RemoveProduct(int ID);
        List<Products> FilterProductByIDCategory(int categoryID);
    }
}
