using ITEC305_Project.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ITEC305_Project.Auth
{
    public class Authenticator
    {
		private static readonly object semphore = new object();
		private static Authenticator Auth
		{
			get
			{
				lock(semphore)
				{
					if (_instance == null)
						return _instance = new Authenticator();
					else
						return _instance;
				}
			}
		}
		private static Authenticator _instance;

		private Authenticator()
		{
			_activeSessions = new Dictionary<string, UserPrincipal>();
		}

		private Dictionary<string, UserPrincipal> _activeSessions;

		public static UserPrincipal GetUserIdenity(string token) => (Auth._activeSessions.ContainsKey(token)) ? Auth._activeSessions[token] : null;

		public static string Authenticate(UserPrincipal user)
		{
			var token = GenerateToken();
			Auth._activeSessions.Add(token, user);
			return token;
		}

		public static string GenerateToken() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
	}
}
