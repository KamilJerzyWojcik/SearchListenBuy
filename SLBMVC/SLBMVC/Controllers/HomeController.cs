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

			ViewBag.AllRows = Math.Ceiling(SqlHelper.NumberAllAlbums() / 3.0);
			ViewBag.page = page;
			albums = SqlHelper.GetIDs(page, 3);
			ReLoadAlbums(albums);

			return View(albums);
		}

		public IActionResult Album(int id)
		{
			currentAlbum = SqlHelper.GetAlbumByID(id);
			ViewBag.Title = "SLB - Search Listen Buy";

			return View(currentAlbum);
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
