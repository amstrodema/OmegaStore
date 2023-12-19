﻿using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Interface
{
    public interface ILoginMonitor : IGeneric<LoginMonitor>
    {
        Task<LoginMonitor> GetMonitorByUserID(Guid userID, Guid storeID);
        Task<LoginMonitor> GetMonitorByUserID(Guid userID, Guid storeID, Guid appCode);
        Task<LoginMonitor> GetMonitorByUserIDOnly(Guid userID);

    }
}
