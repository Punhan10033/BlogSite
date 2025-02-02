using System;
using System.Text;

namespace BlogSite.Cyrpto
{
	public static class Cyrpto1
	{
		public static string Hash(string value)
		{
			return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
				.ComputeHash(Encoding.UTF8.GetBytes(value)));
		}
	}
}
