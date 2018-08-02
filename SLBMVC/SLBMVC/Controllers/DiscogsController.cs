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
		public IActionResult Index()
		{
			
			return View();
		}

		[HttpGet]
		public IActionResult QueryDiscogs()
		{
			return View();
		}

		[HttpPost]
		public IActionResult QueryDiscogs(QueryModel query)
		{
			DiscogsApp discogsApp = new DiscogsApp();
			List<AlbumModel> albumList = discogsApp.CreateListAlbumByQuery(query.Title);
			return View("ListQueryEfect", albumList);
		}

		public IActionResult ListQueryEfect(List<AlbumModel> albumList)
		{

			return View();
		}

		public IActionResult ShowQueryEfect(int id, string title)
		{
			DiscogsApp discogsApp = new DiscogsApp();
			List<AlbumModel> albumList = discogsApp.CreateListAlbumByQuery(title);
			if (id < albumList.Count) return View(albumList[id]);
			else return View("ListQueryEfect", albumList);
		}
	}
}
