﻿using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        ClaimsIdentity Authorise(AccountModel a);
        string Register(AccountModel a, AccountInfoModel ai);
        AccountInfoModel GetAccountInfo(int id);
        AccountModel GetAccountByEmail(string email);
        string UpdateAccountInfo(int id, AccountInfoModel a);
        void LogLogin(bool status, string email, string ip);
        void AddFavorites(int id, int accId);
    }
}
