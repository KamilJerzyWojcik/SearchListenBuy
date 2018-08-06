using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SLBMVC.Models;
using SLBMVC.Models.DataBase;

namespace SLBMVC.Controllers
{
	public class HomeController : Controller
	{
		List<AlbumModel> albums = new List<AlbumModel>();
		AlbumModel currentAlbum = new AlbumModel();

		public IActionResult Index(int page = 1)
		{
			ViewBag.Title = "SLB - Search Listen Buy";

			ViewBag.AllRows = Math.Ceiling(SqlHelper.NumberAllAlbums() / 6.0);
			ViewBag.page = page;
			albums = SqlHelper.GetIDs(page, 6);
			ReLoadAlbums(albums);
			return View(albums);
		}

		public IActionResult Album(int id)
		{
			currentAlbum = SqlHelper.GetAlbumByID(id);
			ViewBag.Title = "SLB - Search Listen Buy";

			return View(currentAlbum);
		}

        public IActionResult Random()
        {
            int numberAlbums = SqlHelper.NumberAllAlbums();
            List<AlbumModel> AlbumWithids = SqlHelper.GetIDs(1, numberAlbums);
            numberAlbums = AlbumWithids.Count();

            Random rand = new Random();
            int num = rand.Next(0, numberAlbums);
           

            currentAlbum = AlbumWithids[num];
            currentAlbum.Reload();
            ViewBag.Title = "SLB - Search Listen Buy";

            return Redirect($"/Home/Album/{currentAlbum.ID}");
        }

        private void GetAlbum(List<AlbumModel> albums, int id)
		{
			for (int i = 0; i <= albums.Count - 1; i++)
			{
				if (albums[i].ID == id)
				{
					currentAlbum = albums[i].Reload();
					break;
				}
			}
		}

		private void ReLoadAlbums(List<AlbumModel> albums)
		{
			for (int i = 0; i <= albums.Count - 1; i++)
			{
				albums[i].Reload();
			}
		}

	}
}
