﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.CronJob.Service.Email.Interface
{
    public interface ISendGreetingEmailForNotification
    {
        public Task SendGreetingEmail(CancellationToken cancellationToken);
    }
}