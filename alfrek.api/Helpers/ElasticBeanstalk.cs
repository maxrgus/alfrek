using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace alfrek.api.Helpers
{
    public class ElasticBeanstalk
    {
        public static Dictionary<string, string> GetAwsDbConfig(IConfiguration configuration)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (IConfigurationSection pair in configuration.GetSection("iis:env").GetChildren())
            {
                string[] keypair = pair.Value.Split(new[] { '=' }, 2);
                dictionary.Add(keypair[0], keypair[1]);
            }
            return dictionary;
        }

    }
}
