using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SLBMVC.Models;
using SLBMVC.Models.DiscogsConnect;

namespace SLBMVC.Controllers
{
    public class DiscogsController : Controller
    {
        DiscogsApp discogsApp = new DiscogsApp();

        public IActionResult QueryDiscogs()
        {
            List<QueryModel> query = new List<QueryModel>();
            query.Add(new QueryModel());
            return View(query);
        }

        [HttpPost]
        public IActionResult QueryDiscogs(List<QueryModel> queryList, string title = "")
        {
            List<AlbumModel> albumList = new List<AlbumModel>();
            if (title != "")
            {
                queryList.Add(new QueryModel());
                queryList[0].Title = title;
            }

            albumList = discogsApp.CreateListAlbumByQuery(queryList[0].Title);
            if (albumList.Count <= 0 || (albumList[0] is null))
            {
                TempData["Info"] = "No content avalible";
                return View(queryList);
            }
            queryList.AddRange(QueryModel.CreateQueryList(albumList));
            return View(queryList);
        }


        public IActionResult ShowQueryEfect(string title, int id)
        {
            AlbumModel album = discogsApp.CreateAlbumByQuery(title, id);
            ViewBag.id = id;
            ViewBag.title = title;
            return View(album);
        }


        public IActionResult Add(string title, int id)
        {
            AlbumModel album = discogsApp.CreateAlbumByQuery(title, id);
            if (album.Save() == -1) TempData["Info"] = "Erorr, Album didn't saved" ;
            else TempData["Succes"]  ="Album saved to base";

            List<QueryModel> query = new List<QueryModel>();
            query.Add(new QueryModel());
            return Redirect("/Home/Index");
        }
    }
}
