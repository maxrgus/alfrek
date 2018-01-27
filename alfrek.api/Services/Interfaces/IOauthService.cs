using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alfrek.api.Services.Interfaces
{
    public interface IOauthService
    {
        void GetToken(string authorizationCode);
    }
}
