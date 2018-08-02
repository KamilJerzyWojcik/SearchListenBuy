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
                query.Title = album.Title;
                query.Artist = album.Artists;
                query.Extraartists = album.ExtraArtists.Count;
                query.Genres = String.Join(", ", album.Genres.ToArray());
                query.Styles = String.Join(", ", album.Styles.ToArray());
                query.Tracks = album.TrackList.Count;
                query.Videos = album.Videos.Count;
                query.Images = album.Images.Count;
                query.Extraartists = album.ExtraArtists.Count;
                queryList.Add(query);
            }
            return queryList;
        }           
    }
}
