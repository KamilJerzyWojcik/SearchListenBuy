using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace SLBMVC.Models.DiscogsConnect
{
	public class DiscogsClient
	{
		
		public DiscogsClient(string key, string secret)
		{
			MyStringWebResource = "https://api.discogs.com";
			Get = "/database/search?q={";
			Key = key;
			Secret = secret;
		}

		public string MyStringWebResource { get; private set; }
		public string Query { get; private set; }
		public string Get { get; private set; }
		public string Link { get; private set; }
		public string Key { get; private set; }
		public string Secret { get; private set; }


		public DiscogsClient SetQuery(string query)
		{
			try
			{
				if (string.IsNullOrEmpty(query)) throw new Exception("Wrong format query");
				Query = query;
				return this;
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				return this;
			}
		}

		public DiscogsClient SetLink(string link)
		{
			try
			{
				if (string.IsNullOrEmpty(link)) throw new Exception("Wrong format link");

				Link = link;
				return this;
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				return this;
			}
		}

		public string GetQueryResult()
		{
			string result;
			try
			{
				if (string.IsNullOrEmpty(Query)) throw new Exception("No Query, use SetQuery()");

				Get += Query + "}";

				MyStringWebResource = MyStringWebResource + Get;

				using (WebClient client = new WebClient())
				{
					client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
					client.Headers.Add("Content-Type", "application/json");
					client.Headers.Add($"Authorization: Discogs key={Key}, secret={Secret}");

					client.UseDefaultCredentials = true;

					result = client.DownloadString(MyStringWebResource);
				}

				return result;

			}
			catch (WebException e)
			{
				
				return "";
			}
			catch (Exception e)
			{
				
				return "";
			}

		}

		public string GetLinkResult()
		{
			string result;
			try
			{
				if (string.IsNullOrEmpty(Link)) throw new Exception("No Link, use SetLink()");

				using (WebClient client = new WebClient())
				{
					client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
					client.Headers.Add("Content-Type", "application/json");
					client.Headers.Add($"Authorization: Discogs key={Key}, secret={Secret}");

					client.UseDefaultCredentials = true;

					result = client.DownloadString(Link);
				}
				return result;
			}
			catch (WebException)
			{
				
				return "";
			}
			catch (Exception e)
			{
			
				return "";
			}
		}
	}
}
