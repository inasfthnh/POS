using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosDataModel;

namespace PosRepository
{
    public class ProductRepo
    {
        //List
        public static List<Product> All()
        {
            List<Product> result = new List<Product>();
            using (var db = new PosContext())
            {
                result = db.Product
                    .Include("Variant")
                    .ToList();
            }
            return result;
        }

        //List by Variant
        public static List<Product> ByVariant(long varId)
        {
            // varId => Variant Id
            List<Product> result = new List<Product>();
            using (var db = new PosContext())
            {
                result = db.Product
                    .Where(o => o.VariantId == varId)
                    .ToList();
            }
            return result;
        }

        //Create
        public static ResponseResult Update(Product entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new PosContext())
                {
                    if (entity.Id == 0)
                    {
                        // Create
                        entity.CreatedBy = "Inas";
                        entity.CreatedDate = DateTime.Now;
                        db.Product.Add(entity);
                        db.SaveChanges();
                        result.Message = "Created";
                        result.Entity = entity;
                    }
                    else
                    {
                        //Edit
                        Product product = db.Product
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                        if (product == null)
                        {
                            result.Success = false;
                        }
                        else
                        {
                            product.VariantId = entity.VariantId;

                            product.Initial = entity.Initial;
                            product.Name = entity.Name;
                            product.Description = entity.Description;
                            product.Price = entity.Price;
                            product.Stock = entity.Stock;
                            product.Active = entity.Active;
                            product.ModifyBy = "Inas";
                            product.ModifyDate = DateTime.Now;
                            db.SaveChanges();

                            result.Message = "Updated";
                            result.Entity = entity;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
            }
            return result;
        }
    }
}
