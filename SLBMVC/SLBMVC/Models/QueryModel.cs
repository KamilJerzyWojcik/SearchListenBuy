using SLBMVC.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SLBMVC.Models
{
    public class QueryModel
    {
        public string Title { get; set; } = "";
        public string Artist { get; set; }
        public string Genres { get; set; }
        public string Styles { get; set; }
        public int Tracks { get; set; }
        public int Videos { get; set; }
        public int Images { get; set; }
        public int Extraartists { get; set; }
        public bool ExistInDB { get; set; }
        public int IDExistAlbum { get; set; }

        public static List<QueryModel> CreateQueryList(List<AlbumModel> albumList)
        {
            List<QueryModel> queryList = new List<QueryModel>();
            foreach (AlbumModel album in albumList)
            {
                QueryModel query = new QueryModel();
                query.ExistInDB = SourceManagerSave.AlbumExists(album);
                if(query.ExistInDB) SqlHelper.GetIDByNameAndTitle(album.Artists, album.Title);
                if(!(album.Title is null)) query.Title = album.Title;
                if (!(album.Artists is null)) query.Artist = album.Artists;
                if (album.ExtraArtists.Count > 0) query.Extraartists = album.ExtraArtists.Count;
                if (album.Genres.Count > 0) query.Genres = String.Join(", ", album.Genres.ToArray());
                if (album.Styles.Count > 0) query.Styles = String.Join(", ", album.Styles.ToArray());
                if (album.TrackList.Count > 0) query.Tracks = album.TrackList.Count;
                if (album.Videos.Count > 0) query.Videos = album.Videos.Count;
                if (album.Images.Count > 0) query.Images = album.Images.Count;
                queryList.Add(query);
            }
            return queryList;
        }           
    }
}
