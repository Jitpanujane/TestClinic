using Jose;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Security
{
    public class JwtHandler
    {
        private static readonly byte[] _securityKey = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["SecurityKey"]);

        public static T Decode<T>(string token)
        {
            try
            {
                var payload = JWT.Decode<JwtPayload<T>>(token, _securityKey, JwsAlgorithm.HS256);

                if (payload == null || payload.Expiration < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                {
                    return default(T);
                }

                return payload.Data;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="expiration">As minutes</param>
        /// <returns></returns>
        public static string Encode<T>(T data, int expiration = 60)
        {
            var payload = new JwtPayload<T>
            {
                Data = data,
                Expiration = DateTimeOffset.UtcNow.AddMinutes(expiration).ToUnixTimeSeconds()
            };

            return JWT.Encode(payload, _securityKey, JwsAlgorithm.HS256);
        }
    }
}
