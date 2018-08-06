using SLBMVC.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SLBMVC.Models
{
    public class AlbumsJson
    {
        public string[] album { get; set; }

        public AlbumsJson()
        {
            album = GetRandomAlbums();
        }

        private string[] GetRandomAlbums()
        {
            int numberAlbums = SqlHelper.NumberAllAlbums();
            List<AlbumModel> AlbumWithids = SqlHelper.GetIDs(1, numberAlbums);
            numberAlbums = AlbumWithids.Count();

            Random rand = new Random();
            int num = rand.Next(0, numberAlbums);
            List<string> Albums = new List<string>();
            List<int> nums = new List<int>();
            int iterator = 0;

            for (int i = 0; ; i++)
            {
                num = rand.Next(0, numberAlbums);
                if (nums.Contains(num)) continue;
                nums.Add(num);
                AlbumModel a = AlbumWithids[num].Reload();
                Albums.Add(a.Images[0]);
                iterator++;
                if (iterator == 6) break;
            }

            nums.Clear();
            iterator = 0;
            List<string> AlbumsNext = new List<string>();
            for (int i = 0; ; i++)
            {
                num = rand.Next(0, 6);
                if (nums.Contains(num)) continue;
                nums.Add(num);
                AlbumsNext.Add(Albums[num]);
                iterator++;
                if (iterator == 6) break;
            }
            Albums.AddRange(AlbumsNext);
            return Albums.ToArray();
     
        }
    }
}
