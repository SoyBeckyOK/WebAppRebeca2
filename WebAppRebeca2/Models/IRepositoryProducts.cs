using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace WebAppRebeca2.Models
{
    public interface IRepositoryProducts
    {
        Products CreateProduct(Products products);
        List<Products> GetAllProducts();
        Products GetProducts(int id);
        void RemoveProducts(int id);
        bool UpdateProducts(Products products);
    }
}
