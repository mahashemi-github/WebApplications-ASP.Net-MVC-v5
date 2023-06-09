using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApplications.DataAccess.Data;
using WebApplications.DataAccess.Repository.IRepository;
using WebApplications.Models;

namespace WebApplications.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Product product)
        {
            //_db.Products.Update(product);
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id); 
            if (objFromDb != null) {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Author = product.Author;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.CategoryId = product.CategoryId;
                if(objFromDb.ImgUrl != null)
                {
                    objFromDb.ImgUrl = product.ImgUrl;
                }
            }
        }
    }
}
