using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Management;
using Web_NF_crud.Models.Entities;
using Web_NF_crud.Models.ModelView;
using Web_NF_crud.Models.Utils;

namespace Web_NF_crud.Models.Repositories
{
    public sealed class CategoryRepository : IRepository<CategoryView>
    {
        private static CategoryRepository _instance=null;
        private CategoryRepository() { }
        public static CategoryRepository Instance
        {
            get
            {
               _instance = _instance ??new CategoryRepository();
                return _instance;
            }
        }
        public HashSet<CategoryView> All()
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = en.tbl_category.Select(d=> new CategoryView{
                    ID = d.cate_id,
                    Name = d.cate_title,
                    Active = d.cate_active??0,
                }).ToHashSet();
                return item;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }
            return new HashSet<CategoryView>();
        }
        public HashSet<CategoryView> ListCategoryActive()
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = en.tbl_category.Where(d=> d.cate_active ==1 ).Select(d => new CategoryView
                {
                    ID = d.cate_id,
                    Name = d.cate_title,
                }).ToHashSet();
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new HashSet<CategoryView>();
        }

        public void create(CategoryView entity)
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = new tbl_category {
                    cate_title = entity.Name,
                    cate_active = entity.Active,
                    cate_id = entity.ID,
                };
                en.tbl_category.Add(item);
                en.SaveChanges();
                entity.ID = item.cate_id;
            }
            catch(Exception e) { 
                Debug.WriteLine(e);
            }
        }

        public int delete(CategoryView entity)
        {
            int result = 0;
            try
            {
                DemoEntities en = new DemoEntities();
                var chkListPro = en.tbl_product.Any(d=>d.pro_cate_id == entity.ID);
                if ((chkListPro))
                {
                    result=2;//exit product relative
                }
                else {
                    var item = en.tbl_category.Where(d => d.cate_id == entity.ID).FirstOrDefault();
                    en.tbl_category.Remove(item);
                    result = en.SaveChanges();
                }
               
            }
            catch (Exception ex) { 
                Debug.WriteLine(ex);
            }
            return result;
        }

        public CategoryView findById(int id)
        {
            CategoryView categoryView = new CategoryView();
            try
            {
                DemoEntities en = new DemoEntities();
                var rs = en.tbl_category.Where(d=>d.cate_id==id).Select(d => new CategoryView
                {
                    ID = d.cate_id,
                    Name = d.cate_title,
                    Active = d.cate_active??0,
                }).FirstOrDefault();
                categoryView =rs?? new CategoryView();
            }
            catch (Exception ex) { 
                Debug.WriteLine(ex);
            }
            return categoryView;
        }

        public HashSet<CategoryView> findByKeyWord(string name)
        {
            throw new NotImplementedException();
        }

        public int update(CategoryView entity)
        {
            try
            {
                DemoEntities en = new DemoEntities();
                var item = en.tbl_category.Where(d=>d.cate_id == entity.ID).FirstOrDefault();
                item.cate_title = entity.Name;
                item.cate_active = entity.Active;
                en.SaveChanges();
                return 1;
            }
            catch(Exception ex) { 
                Debug.WriteLine(ex); 
            }
            return 0;
        }
        public HashSet<CategoryView> getAllPaging(int index =1, int PageSize =10)
        {
            try
            {
               DemoEntities en = new DemoEntities();
                var rs = en.tbl_category.Skip((index-1)*PageSize).Take(PageSize).Select(d=> new CategoryView
                {
                    ID = d.cate_id,
                    Name = d.cate_title,
                    Active = d.cate_active??0
                }).ToHashSet();
                return rs;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new HashSet<CategoryView>();
        }
        public string Active(int id,int active_status=0)
        {
            try
            {
                var en = new DemoEntities();
                var item = en.tbl_category.SingleOrDefault(d=> d.cate_id == id);
                item.cate_active=active_status;
                en.SaveChanges();
                return StringValue.MESSAGE_CHANGE_ACTIVE_SUCCESS;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return StringValue.MESSAGE_CHANGE_ACTIVE_FAILED;
        }
    }
}