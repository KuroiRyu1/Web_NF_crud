using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_NF_crud.Models.ModelView
{
    public class ProductView
    {

        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public int Price { get; set; }
        public int category_Id { get; set; } = 0;
        public string category_Title { get; set; }="no Title";
        public string ImageName { get; set; } = "noimage.jpg";
        public int Active { get; set; }= 0;
    }
}