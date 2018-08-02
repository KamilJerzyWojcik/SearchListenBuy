using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SLBMVC.Models.DataBase
{
	public class SourceManagerSave
	{

		public static int Add(AlbumModel album)
		{
			album.ID = AddTitleArtist(album);
			int LastGenreID = AddGenres(album);
			int LastStyleID = AddStyles(album);
			int LastTrackID = AddTrackList(album);
			int LastVideoID = AddVideosList(album);
			int LastImageID = AddImagesList(album);
			int LastExtraArtistID = AddExtraArtistList(album);

			return album.ID;
		}

		private static int AddTitleArtist(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Insert INTO Album (Title, Artists)
				VALUES (@Title, @Artists); SELECT CAST(scope_identity() AS int)";

					sqlCommand.Parameters.Add(new SqlParameter("@Title", album.Title));
					sqlCommand.Parameters.Add(new SqlParameter("@Artists", album.Artists));

					return (int)sqlCommand.ExecuteScalar();
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

		private static int AddGenres(AlbumModel album)
		{
			try
			{
				int result = -1;
				string genreRedundand = "";
				foreach (string genre in album.Genres)
				{
					if (genreRedundand == genre) continue;
					genreRedundand = genre;

					using (var connection = SqlHelper.GetConnection())
					{
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;


						sqlCommand.CommandText = @"Insert INTO Genres (Genre, AlbumID)
					VALUES (@Genre, @AlbumID); SELECT CAST(scope_identity() AS int)";

						sqlCommand.Parameters.Add(new SqlParameter("@Genre", genre));
						sqlCommand.Parameters.Add(new SqlParameter("@AlbumID", album.ID));

						result = (int)sqlCommand.ExecuteScalar();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int AddStyles(AlbumModel album)
		{
			try
			{
				int result = -1;
				for (int i = 0; i <= album.Styles.Count - 1; i++)
				{
					using (var connection = SqlHelper.GetConnection())
					{
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;


						sqlCommand.CommandText = @"Insert INTO Styles (Style, AlbumID)
					VALUES (@Style, @AlbumID); SELECT CAST(scope_identity() AS int)";

						sqlCommand.Parameters.Add(new SqlParameter("@Style", album.Styles[i]));
						sqlCommand.Parameters.Add(new SqlParameter("@AlbumID", album.ID));

						result = (int)sqlCommand.ExecuteScalar();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int AddTrackList(AlbumModel album)
		{
			try
			{
				int result = -1;
				for (int i = 0; i <= album.TrackList.Count - 1; i++)
				{
					using (var connection = SqlHelper.GetConnection())
					{
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;


						sqlCommand.CommandText = @"Insert INTO TrackList (Position, Duration, Title, AlbumID)
					VALUES (@Position, @Duration, @Title, @AlbumID); SELECT CAST(scope_identity() AS int)";

						sqlCommand.Parameters.Add(new SqlParameter("@Position", album.TrackList[i].Position));
						sqlCommand.Parameters.Add(new SqlParameter("@Duration", album.TrackList[i].Duration));
						sqlCommand.Parameters.Add(new SqlParameter("@Title", album.TrackList[i].Title));
						sqlCommand.Parameters.Add(new SqlParameter("@AlbumID", album.ID));

						result = (int)sqlCommand.ExecuteScalar();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int AddVideosList(AlbumModel album)
		{
			try
			{
				int result = -1;
				for (int i = 0; i <= album.Videos.Count - 1; i++)
				{
					using (var connection = SqlHelper.GetConnection())
					{
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;


						sqlCommand.CommandText = @"Insert INTO Videos (Video, AlbumID)
					VALUES (@Video, @AlbumID); SELECT CAST(scope_identity() AS int)";

						sqlCommand.Parameters.Add(new SqlParameter("@Video", album.Videos[i]));
						sqlCommand.Parameters.Add(new SqlParameter("@AlbumID", album.ID));

						result = (int)sqlCommand.ExecuteScalar();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int AddImagesList(AlbumModel album)
		{
			try
			{
				int result = -1;
				for (int i = 0; i <= album.Images.Count - 1; i++)
				{
					using (var connection = SqlHelper.GetConnection())
					{
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;


						sqlCommand.CommandText = @"Insert INTO Images (Image, AlbumID)
					VALUES (@Image, @AlbumID); SELECT CAST(scope_identity() AS int)";

						sqlCommand.Parameters.Add(new SqlParameter("@Image", album.Images[i]));
						sqlCommand.Parameters.Add(new SqlParameter("@AlbumID", album.ID));

						result = (int)sqlCommand.ExecuteScalar();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		private static int AddExtraArtistList(AlbumModel album)
		{
			try
			{
				int result = -1;
				foreach (ArtistModel artist in album.ExtraArtists)
				{
					using (var connection = SqlHelper.GetConnection())
					{
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;


						sqlCommand.CommandText = @"Insert INTO ExtraArtists (Name, Role, Title, AlbumID)
					VALUES (@Name, @Role, @Title, @AlbumID); SELECT CAST(scope_identity() AS int)";

						sqlCommand.Parameters.Add(new SqlParameter("@Role", artist.Role));
						sqlCommand.Parameters.Add(new SqlParameter("@Name", artist.Name));
						sqlCommand.Parameters.Add(new SqlParameter("@Title", artist.AlbumTitle));
						sqlCommand.Parameters.Add(new SqlParameter("@AlbumID", album.ID));

						result = (int)sqlCommand.ExecuteScalar();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: " + e.Message);
				Console.ResetColor();
				return -1;
			}
		}

		public static bool AlbumExists(AlbumModel album)
		{
			try
			{
				using (var connection = SqlHelper.GetConnection())
				{
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = @"Select AlbumID from Album where Title= @Title and Artists = @Artists";

					sqlCommand.Parameters.Add(new SqlParameter("@Title", album.Title));
					sqlCommand.Parameters.Add(new SqlParameter("@Artists", album.Artists));

					if ((sqlCommand.ExecuteScalar() is null))
						return false;

					return true;
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: " + e.Message);
				Console.ResetColor();
				return false;
			}
		}
	}
}
