using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;


namespace SLBMVC.Models.DiscogsConnect
{
	public class DiscogsApp
	{
		public List<string> GetReleaseAlbumCDLink(string QueryResult)
		{
			List<string> Links = new List<string>();

			try
			{
				JObject parsed = JObject.Parse(QueryResult);

				for (int i = 0; i <= parsed["results"].ToList().Count - 1; i++)
				{
					JObject p = (JObject)parsed["results"][i];

					if (p["type"].ToString() == "release" && Array.IndexOf(p["format"].ToArray(), "CD") > -1 && Array.IndexOf(p["format"].ToArray(), "Album") > -1)
					{
						Links.Add(p["resource_url"].ToString());
					}
				}
				return Links;
			}
			catch (FormatException)
			{
				Console.WriteLine("Error: Wrong query format");
				return null;
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				return null;
			}
		}

		public void ShowRelease(string release)
		{
			try
			{
				JObject parserRelease = JObject.Parse(release);
				int numberVideos = 0;
				int numberImages = 0;
				int numberTracks = 0;
				int numberExtraArtists = 0;
				Console.WriteLine("----------------------------------------------------------------");
				Console.WriteLine("Album: ");
				Console.WriteLine();

				Console.WriteLine($"Tytuł: {parserRelease["title"]}");
				Console.WriteLine($"Artysta: {parserRelease["artists"][0]["name"]}");

				Console.WriteLine($"Gatunek: {string.Join(", ", parserRelease["genres"])}");
				Console.WriteLine($"Styl: {string.Join(", ", parserRelease["styles"])}");
				Console.WriteLine("Lista utworów: ");//możliwosc tworzenia likow pod nazwami (wklejanie yt na konto user -> BD )
				foreach (var track in parserRelease["tracklist"])
				{
					Console.WriteLine($"{track["position"]}\t{track["duration"]}\t{track["title"]}");
					numberTracks++;
				}

				Console.WriteLine("\nLista teledysków: ");
				if (!(parserRelease["videos"] is null))
				{
					foreach (var track in parserRelease["videos"])
					{
						Console.WriteLine($"{track["title"]}\t{track["uri"]}");
						numberVideos++;
					}
				}

				Console.WriteLine("\nObrazy: ");//lista okładek do sciągnięcia/przejrzenia jak allegro wiele obrazkow i paginacja
				if (!(parserRelease["images"] is null))
				{
					foreach (var track in parserRelease["images"])
					{
						Console.WriteLine($"{track["uri"]}");
						numberImages++;
					}
				}

				Console.WriteLine("\n\n pracownicy przy płycie:");
				if (!(parserRelease["extraartists"] is null))
				{
					foreach (var track in parserRelease["extraartists"])
					{
						Console.Write($"\n{track["role"]}: {track["name"]}");
						numberExtraArtists++;
					}
				}
				Console.WriteLine("\n----------------------------------------------------------------");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Images: " + numberImages);
				Console.WriteLine("Videos: " + numberVideos);
				Console.WriteLine("Tracks: " + numberTracks);
				Console.WriteLine("ExtraArtists: " + numberExtraArtists);
				Console.ResetColor();
				Console.WriteLine("\n----------------------------------------------------------------");

			}
			catch (FormatException)
			{
				Console.WriteLine("Error: Wrong release format");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}

		public bool SearchAlbumAndShow(string nameAlbum, out string release)
		{
			DiscogsClient discogsClient = new DiscogsClient("NYqvEPnYZdPWmAFFMURi", "kEvfDhiyBRunRURKFjlMmoCKPjIcYiVU");

			string result = discogsClient.SetQuery(nameAlbum).GetQueryResult(); //zapytanie o album

			List<string> link = GetReleaseAlbumCDLink(result); //wyciagniecie linku do konretnego wydania

			if (link is null)
			{
				release = "";
				return false;
			}

			string command = "";
			release = "";
			int index = 0;
			do
			{
				if (link.Count == 0) return false;

				Console.Clear();
				if (index < link.Count)
					release = discogsClient.SetLink(link[index]).GetLinkResult(); //zapytanie o wydanie konkretne
				else
					release = "";

				if (!string.IsNullOrEmpty(release)) ShowRelease(release);

				if (string.IsNullOrEmpty(release))
				{
					Console.Clear();
					Console.WriteLine("No Data, back to th first?: (y/n): ");
					command = Console.ReadLine();

					if (command == "n") return false;
					if (command == "y")
					{
						index = 0;
						continue;
					}
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("\nNext data?: (y/n): ");
					Console.ResetColor();
					command = Console.ReadLine();
				}

				if (command == "y") index++;
				if (command == "n") break;

			} while (true);

			return true;
		}

		public AlbumModel CreateAlbumByQuery(string nameAlbum)
		{
			AlbumModel album = new AlbumModel();
			DiscogsClient discogsClient = new DiscogsClient("NYqvEPnYZdPWmAFFMURi", "kEvfDhiyBRunRURKFjlMmoCKPjIcYiVU");

			string result = discogsClient.SetQuery(nameAlbum).GetQueryResult(); //zapytanie o album

			string link = GetReleaseAlbumCDLink(result)[0]; //wyciagniecie linku do konretnego wydania

			string release = discogsClient.SetLink(link).GetLinkResult(); //zapytanie o wydanie konkretne
			if (string.IsNullOrEmpty(release)) return null;
			album = CreateAlbumByRelease(release);

			return album;
		}

		public List<AlbumModel> CreateListAlbumByQuery(string nameAlbum)
		{
			DiscogsClient discogsClient = new DiscogsClient("NYqvEPnYZdPWmAFFMURi", "kEvfDhiyBRunRURKFjlMmoCKPjIcYiVU");

			string result = discogsClient.SetQuery(nameAlbum).GetQueryResult(); //zapytanie o album

			List<AlbumModel> albumList = new List<AlbumModel>();
			for (int i = 0; i <= GetReleaseAlbumCDLink(result).Count - 1; i++)
			{
				string l = GetReleaseAlbumCDLink(result)[i];

				string rel = discogsClient.SetLink(l).GetLinkResult();
				if (string.IsNullOrEmpty(rel)) return null;

				albumList.Add(CreateAlbumByRelease(rel));
			}


			return albumList;
		}

		public AlbumModel CreateAlbumByRelease(string release)
		{
			AlbumModel album = new AlbumModel();

			JObject parserRelease = JObject.Parse(release);

			if (!(parserRelease["title"] is null))
				album.Title = parserRelease["title"].ToString();

			if (!(parserRelease["artists"][0]["name"] is null))
				album.Artists = parserRelease["artists"][0]["name"].ToString();

			if (!(parserRelease["genres"] is null))
			{
				foreach (var genre in parserRelease["genres"])
					album.Genres.Add(genre.ToString());
			}

			if (!(parserRelease["styles"] is null))
			{
				foreach (var style in parserRelease["styles"])
					album.Styles.Add(style.ToString());
			}

			if (!(parserRelease["tracklist"] is null))
			{
				foreach (var track in parserRelease["tracklist"])
					album.TrackList.Add(new TrackModel(track["position"].ToString(), track["duration"].ToString(), track["title"].ToString()));
			}
			if (!(parserRelease["videos"] is null))
			{
				foreach (var video in parserRelease["videos"])
					album.Videos.Add(video["uri"].ToString());
			}

			if (!(parserRelease["images"] is null))
			{
				foreach (var image in parserRelease["images"])
					album.Images.Add(image["uri"].ToString());
			}

			if (!(parserRelease["extraartists"] is null))
			{
				foreach (var artist in parserRelease["extraartists"])
					album.ExtraArtists.Add(new ArtistModel(artist["name"].ToString(), artist["role"].ToString(), album.Title));
			}

			return album;
		}
	}
}
