using SLBMVC.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SLBMVC.Models
{
	public class AlbumModel
	{
		public AlbumModel(int iD)
		{
			ID = iD;
			Genres = new List<string>();
			Styles = new List<string>();
			TrackList = new List<TrackModel>();
			Videos = new List<string>();
			Images = new List<string>();
			ExtraArtists = new List<ArtistModel>();
		}

		public AlbumModel()
		{
			Genres = new List<string>();
			Styles = new List<string>();
			TrackList = new List<TrackModel>();
			Videos = new List<string>();
			Images = new List<string>();
			ExtraArtists = new List<ArtistModel>();
		}

		public int ID { get; set; } = 0;
		public string Title { get; set; }
		public string Artists { get; set; }
		public List<string> Genres { get; set; }
		public List<string> Styles { get; set; }
		public List<TrackModel> TrackList { get; set; }
		public List<string> Videos { get; set; }
		public List<string> Images { get; set; }
		public List<ArtistModel> ExtraArtists { get; set; }

		public int Save()
		{
			try
			{
				Console.Clear();
				if (SourceManagerSave.AlbumExists(this)) throw new Exception("Album exist in Base");

				int ID = SourceManagerSave.Add(this);
				if (ID == -1) throw new Exception("Error, Data didn't save to Base");

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"zapisano {Title} do Bazy Danych pod ID: {ID}");
				Console.ResetColor();
				return ID;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		public AlbumModel Reload()
		{
			try
			{
				if (ID <= 0) throw new Exception("Error, incorect album ID");

				SourceManagerLoad.Load(this);

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"Odczytano z bazy danych album o ID: {ID}");
				Console.ResetColor();
				return this;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				Console.ResetColor();
				return this;
			}
		}
	}
}
