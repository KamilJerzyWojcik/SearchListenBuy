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
        public IActionResult QueryDiscogs(List<QueryModel> queryList)
        {
            List<AlbumModel> albumList = discogsApp.CreateListAlbumByQuery(queryList[0].Title);
            if ((albumList[0] is null)) return View(queryList);
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
            if (album.Save() == -1) TempData["Info"] = "Nie udał się zapis" ;
            else TempData["Succes"]  ="zapisano do bazy";

            List<QueryModel> query = new List<QueryModel>();
            query.Add(new QueryModel());
            return Redirect("/Home/Index");
        }
    }
}
