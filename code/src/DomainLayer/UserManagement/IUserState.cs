﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public interface IUserState
    {
        bool isSubscribed();
        string Name();
        string Password();
        void logPurchase(UserPurchase userPurchase);
        bool isSystemAdmin();
    } 
}
