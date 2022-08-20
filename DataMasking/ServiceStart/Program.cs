﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DataMasking.ServiceStart
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            var serviceRun = new ServiceBase[]
            {
                new ServiceStartDataMasking(), 
            };
            ServiceBase.Run(serviceRun);
        }
    }
}
