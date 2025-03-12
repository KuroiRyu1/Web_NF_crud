using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Web_NF_crud.Models.Entities;
using Web_NF_crud.Models.ModelView;

namespace Web_NF_crud.Models.Repositories
{
    public sealed class ProductRepository : IRepository<ProductView>
    {
        private ProductRepository() { }
        private static ProductRepository _instance = null;
        public static ProductRepository Instance {  
            get {
                _instance = _instance?? new ProductRepository();
                return _instance; 
            } 
        }
        public HashSet<ProductView> All()
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = en.tbl_product.Join(en.tbl_category, p => p.pro_cate_id, d => d.cate_id, (p, d) => new ProductView
                {

                    Id = (int)p.pro_id,
                    Name = p.pro_name,
                    Description = p.pro_description,
                    Price = (int)p.pro_price,
                    category_Id = (int)p.pro_cate_id,
                    category_Title = d.cate_title,
                    ImageName = p.pro_image,
                    Active = (int)p.pro_active,
                }).ToHashSet();
                return item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return new HashSet<ProductView>();
        }

        public void create(ProductView entity)
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = new tbl_product
                {
                    pro_name = entity.Name,
                    pro_description = entity.Description,
                    pro_price = entity.Price,
                    pro_cate_id = entity.category_Id,
                    pro_active = entity.Active,
                    pro_image = entity.ImageName,
                };
                en.tbl_product.Add(item);
                en.SaveChanges();
                entity.Id = (int)item.pro_id;
            }
            catch (Exception e) { 
                Debug.WriteLine(e); 
            }
        }

        public int delete(ProductView entity)
        {
            int result=0;
            try
            {
                DemoEntities en = new DemoEntities();
                var rs = en.tbl_product.Any(p => (int)p.pro_id==entity.Id);
                if (rs)
                {
                    result = 1;
                }
                return result;
            }
            catch (Exception e) { 
                Debug.WriteLine(e);
            }
            return result;
        }

        public ProductView findById(int id)
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = en.tbl_product.Where(d=> d.pro_id == id).Select(d=> new ProductView
                {
                    Id = (int)d.pro_id,
                    Name = d.pro_name,
                    Description = d.pro_description,
                    Price = (int) d.pro_price,
                    Active = (int)d.pro_active,
                }).FirstOrDefault();
                return item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return new ProductView();
        }

        public HashSet<ProductView> findByKeyWord(string name)
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = en.tbl_product.Where(d=> d.pro_name.Contains(name)).Select(p=> new ProductView
                {
                    Id = (int)p.pro_id,
                    Name = p.pro_name,
                    Description = p.pro_description,
                    Price = (int)p.pro_price,
                    category_Id = (int)p.pro_cate_id,
                    Active = (int)p.pro_active,
                }).ToHashSet();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return new HashSet<ProductView>();
        }

        public int update(ProductView entity)
        {
            try
            {
                DemoEntities en=new DemoEntities();
                var item = en.tbl_product.Where(d => d.pro_id == entity.Id).FirstOrDefault();
                item.pro_name = entity.Name;
                item.pro_description = entity.Description;
                item.pro_active = entity.Active;
                item.pro_price = entity.Price;
                item.pro_cate_id = entity.category_Id;
                en.SaveChanges();
                return 1;
            }
            catch (Exception e) { 
                Debug.WriteLine(e); 
            }
            return 0;
        }
    }
}