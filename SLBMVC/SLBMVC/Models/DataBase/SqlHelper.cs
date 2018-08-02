using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SLBMVC.Models.DataBase
{
	public class SqlHelper
	{

		static string connectionString = "Integrated Security=SSPI;" +
											 "Data Source=.\\SQLEXPRESS;" +
											 "Initial Catalog=SLB;";

		public static SqlConnection GetConnection()
		{
			SqlConnection conn = new SqlConnection(connectionString);
			conn.Open();
			return conn;
		}

		public static List<AlbumModel> GetIDs(int start, int take)
		{
			var albumList = new List<AlbumModel>();

			using (var connection = GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = "SELECT AlbumID FROM Album ORDER BY AlbumID OFFSET @Start ROWS FETCH NEXT @Take ROWS ONLY;";

				var sqlStartParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = (start - 1) * take,
					ParameterName = "@Start"
				};

				var sqlTakeParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = take,
					ParameterName = "@Take"
				};
				sqlCommand.Parameters.Add(sqlStartParam);
				sqlCommand.Parameters.Add(sqlTakeParam);

				var data = sqlCommand.ExecuteReader();

				while (data.HasRows && data.Read())
				{
					AlbumModel album = new AlbumModel();
					album.ID = (int)data["AlbumID"];
					albumList.Add(album);
				}
			}
			return albumList;
		}

		public static int NumberAllAlbums()
		{
			using (var connection = GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = @"Select COUNT(AlbumID) from Album;";

				return (int)sqlCommand.ExecuteScalar();
			}
		}

		public static AlbumModel GetAlbumByID(int id)
		{
			AlbumModel album = new AlbumModel();
			album.ID = id;
			album.Reload();

			return album;
		}

		public static List<AlbumModel> GetIDByName(string Title, out int numberAllAlbums, int page = -1)
		{
			numberAllAlbums = 0;
			using (var connection = GetConnection())
			{
				List<AlbumModel> albums = new List<AlbumModel>();

				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = "SELECT AlbumID FROM Album Where Title like @Title;";

				var sqlIdParam = new SqlParameter
				{
					DbType = System.Data.DbType.String,
					Value = Title += "%",
					ParameterName = "@Title"
				};
				sqlCommand.Parameters.Add(sqlIdParam);

				var data = sqlCommand.ExecuteReader();

				while (data.HasRows && data.Read())
				{
					AlbumModel album = new AlbumModel((int)data["AlbumID"]);
					albums.Add(album);
				}

				if (page == -1)
					return albums;

				numberAllAlbums = albums.Count;
				List<AlbumModel> albumPag = new List<AlbumModel>();
				for (int i = (page - 1) * 3; i <= (page - 1) * 3 + 2; i++)
				{
					if (i <= albums.Count - 1)
						albumPag.Add(albums[i]);
				}
				return albumPag;
			}
		}
	}
}
