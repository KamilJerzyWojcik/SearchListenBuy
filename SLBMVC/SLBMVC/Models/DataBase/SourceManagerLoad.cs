using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SLBMVC.Models.DataBase
{
	public class SourceManagerLoad
	{
		public static bool Load(AlbumModel album)
		{
			int TitleArtistID = LoadTitleArtist(album);
			int LastGenresID = LoadGenres(album);
			int LastStylesID = LoadStyles(album);
			int LastTrackID = LoadTracks(album);
			int LastVideosID = LoadVideos(album);
			int LastImageID = LoadImages(album);
			int LastExtraArtistID = LoadExtraArtists(album);

			return true;
		}

		private static int LoadTitleArtist(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select * From Album Where AlbumID = @ID";

					sqlCommand.Parameters.Add(new SqlParameter("@ID", album.ID));

					var data = sqlCommand.ExecuteReader();


					while (data.HasRows && data.Read())
					{
						album.Artists = data["Artists"].ToString();
						album.Title = data["Title"].ToString();
					}

					return album.ID;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int LoadGenres(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select * From Genres Where AlbumID = @ID";

					sqlCommand.Parameters.Add(new SqlParameter("@ID", album.ID));

					var data = sqlCommand.ExecuteReader();
					int genreId = -1;
					while (data.HasRows && data.Read())
					{
						album.Genres.Add(data["Genre"].ToString());
						genreId = (int)data["ID"];
					}

					return genreId;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int LoadStyles(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select * From Styles Where AlbumID = @ID";

					sqlCommand.Parameters.Add(new SqlParameter("@ID", album.ID));

					var data = sqlCommand.ExecuteReader();
					int styleId = -1;
					while (data.HasRows && data.Read())
					{
						album.Styles.Add(data["Style"].ToString());
						styleId = (int)data["ID"];
					}

					return styleId;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int LoadTracks(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select * From TrackList Where AlbumID = @ID";

					sqlCommand.Parameters.Add(new SqlParameter("@ID", album.ID));

					var data = sqlCommand.ExecuteReader();
					int trackId = -1;
					while (data.HasRows && data.Read())
					{
						album.TrackList.Add(new TrackModel(
							data["Position"].ToString(),
							data["Duration"].ToString(),
							data["Title"].ToString()
						));
						trackId = (int)data["ID"];
					}

					return trackId;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int LoadVideos(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select * From Videos Where AlbumID = @ID";

					sqlCommand.Parameters.Add(new SqlParameter("@ID", album.ID));

					var data = sqlCommand.ExecuteReader();
					int videoId = -1;
					while (data.HasRows && data.Read())
					{
						album.Videos.Add(data["Video"].ToString());
						videoId = (int)data["ID"];
					}

					return videoId;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int LoadImages(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select * From Images Where AlbumID = @ID";

					sqlCommand.Parameters.Add(new SqlParameter("@ID", album.ID));

					var data = sqlCommand.ExecuteReader();
					int id = -1;
					while (data.HasRows && data.Read())
					{
						album.Images.Add(data["Image"].ToString());
						id = (int)data["ID"];
					}

					return id;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int LoadExtraArtists(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select * From ExtraArtists Where AlbumID = @ID";

					sqlCommand.Parameters.Add(new SqlParameter("@ID", album.ID));

					var data = sqlCommand.ExecuteReader();
					int id = -1;
					while (data.HasRows && data.Read())
					{
						album.ExtraArtists.Add(new ArtistModel(
							data["Name"].ToString(),
							data["Role"].ToString(),
							data["Title"].ToString()
						));
						id = (int)data["ID"];
					}

					return id;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}
	}
}
