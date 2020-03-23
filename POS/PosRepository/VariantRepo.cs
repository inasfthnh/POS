using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosDataModel;

namespace PosRepository
{
    public class VariantRepo
    {
        //List
        public static List<Variant> All()
        {
            List<Variant> result = new List<Variant>();
            using (var db = new PosContext())
            {
                result = db.Variant
                    .Include("Category")
                    .ToList();
            }
            return result;
        }

        //List by Category
        public static List<Variant> ByCategory(long catId)
        {
            // catId => Category Id
            List<Variant> result = new List<Variant>();
            using (var db = new PosContext())
            {
                result = db.Variant
                    //.Include("Category")
                    .Where(o => o.CategoryId == (catId != 0 ? catId : o.CategoryId)) 
                    // jika Id = 0, maka akan dibandingkan dgn categoryId, jika true semua maka akan tampil semua Id
                    .ToList();
            }
            return result;
        }

        //Create
        public static ResponseResult Update(Variant entity)
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
                        db.Variant.Add(entity);
                        db.SaveChanges();
                        result.Message = "Created";
                        result.Entity = entity;
                    }
                    else
                    {
                        //Edit
                        Variant variant = db.Variant
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                        if (variant == null)
                        {
                            result.Success = false;
                        }
                        else
                        {
                            variant.CategoryId = entity.CategoryId;

                            variant.Initial = entity.Initial;
                            variant.Name = entity.Name;
                            variant.Active = entity.Active;
                            variant.ModifyBy = "Inas";
                            variant.ModifyDate = DateTime.Now;
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
