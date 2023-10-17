using Gotchi.Portfolios.DTOs;
using Gotchi.Portfolios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Core.Helpers;

public static class CoreHelper
{
    public static string NewId() => Guid.NewGuid().ToString();

    public static int NumberOfHoursPassed(DateTime since) => NumberOfHoursPassed(since, DateTime.Now);
    public static int NumberOfHoursPassed(DateTime since, DateTime until) 
    {
        return until.Subtract(since).Hours;
    }

}
