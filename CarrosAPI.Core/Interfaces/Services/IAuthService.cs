﻿using CarrosAPI.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrosAPI.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> AuthenticateAsync(UserInfo userInfo);
    }
}
