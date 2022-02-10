using ARINLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services.Statistic
{
    public interface IStatisticsService
    {
        public List<StatisticCard> GetStatisticsCard();
        public List<StatisticCard> GetMyStatisticsCard(string userId);

    }
}
