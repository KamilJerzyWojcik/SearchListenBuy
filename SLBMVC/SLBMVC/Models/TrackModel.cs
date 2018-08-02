using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SLBMVC.Models
{
	public class TrackModel
	{
		public TrackModel(string position, string duration, string title)
		{
			Position = position;
			Duration = duration;
			Title = title;
		}

		public string Position { get; set; }
		public string Duration { get; set; }
		public string Title { get; set; }
	}
}
