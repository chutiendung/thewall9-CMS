﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using thewall9.web.parent.BLL;
using thewall9.web.parent.HtmlHelpers;

namespace thewall9.web.parent.Controllers
{

    public class ProductController : Controller
    {
        ProductBLL _ProductService = new ProductBLL();
        CategoryBLL _CategoryService = new CategoryBLL();
        ContentBLL _ContentService = new ContentBLL();

        //   [Route("product/{ProductCategoryFriendlyUrl?}")]
        //   public ActionResult Index(string ProductCategoryFriendlyUrl, int Page = 1)
        //   {
        //       ViewBag.Page = Page;
        //       ViewBag.ProductCategoryFriendlyUrl = ProductCategoryFriendlyUrl;

        ////       ViewBag.ProductContent = _ContentService.Get(Request.Url.Authority, "product");

        //       return View(_ProductService.Get(APP._SiteID
        //           , Request.Url.Authority
        //           , ProductCategoryFriendlyUrl
        //           , Page));
        //   }
        [Route("product/{ProductID}/{FriendlyUrl}")]
        public ActionResult Detail(int ProductID, string FriendlyUrl)
        {
            var _Model = _ProductService.GetDetail(Request.Url.Authority, ProductID, FriendlyUrl);
            if (_Model == null) throw new HttpException(404, "Page Not Found");

            ViewBag.Content = _ContentService.Get(Request.Url.Authority, "product");

            ViewBag.Title = _Model.ProductName;
            ViewBag.MetaDescription = _Model.Description;

            return View(_Model);
        }
        [Route("products/{View}/{ProductCategoryFriendlyUrl}/{Page?}")]
        public PartialViewResult List(string View, string ProductCategoryFriendlyUrl = null, int Page = 1)
        {
            var _P = _ProductService.Get(Request.Url.Authority
                , ProductCategoryFriendlyUrl
                , Page);
            return PartialView(View, _P);
        }
        //CATEGORIES
        [Route("category/{CategoryID}/{FriendlyUrl}/{Page?}")]
        public ActionResult Category(int CategoryID, string FriendlyUrl, int Page = 1)
        {
            var _Products = _ProductService.Get(Request.Url.Authority, FriendlyUrl, Page);
            var _Category = _CategoryService.GetByID(CategoryID, FriendlyUrl);
            if (_Products == null || _Category == null) throw new HttpException(404, "Page Not Found");

            ViewBag.Content = _ContentService.Get(Request.Url.Authority, "category");
            ViewBag.Products = _Products;

            ViewBag.Title = _Category.CategoryName;
            //TO-DO ADD DESCRIPTION TO CATEGORY
           // ViewBag.MetaDescription = _Model.Description;

            return View(_Category);
        }
    }
}