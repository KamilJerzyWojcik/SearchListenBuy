using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SLBMVC.Models
{
	public class ArtistModel
	{
		public ArtistModel(string name, string role, string albumTitle)
		{
			Name = name;
			Role = role;
			AlbumTitle = albumTitle;
		}

		public string Name { get; set; }
		public string Role { get; set; }
		public string AlbumTitle { get; set; }
	}
}
