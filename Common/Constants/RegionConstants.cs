using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constants
{
    public static class RegionConstants
    {
        /// <summary>
        /// Dictionary that stores all regions and list of nearest region in order of nearest to farthest.
        /// </summary>
        public static Dictionary<Region, List<Region>> NearestRegionsForRegion = new()
        {
            {
                Region.NorthAmerica,
                new() { Region.SouthAmerica, Region.NorthAsia, Region.SouthAsia, Region.Australia, Region.WesternEurope, Region.EasternEurope, Region.CIS }
            },
            {
                Region.SouthAmerica,
                new() { Region.NorthAmerica, Region.Australia, Region.SouthAsia, Region.NorthAsia, Region.WesternEurope, Region.EasternEurope, Region.CIS }
            },
            {
                Region.Australia,
                new() { Region.SouthAsia, Region.NorthAsia, Region.SouthAmerica, Region.NorthAmerica, Region.CIS, Region.EasternEurope, Region.WesternEurope }
            },
            {
                Region.NorthAsia,
                new() { Region.SouthAsia, Region.CIS, Region.Australia, Region.EasternEurope, Region.NorthAmerica, Region.WesternEurope, Region.SouthAmerica }
            },
            {
                Region.SouthAsia,
                new() { Region.NorthAsia, Region.Australia, Region.CIS, Region.SouthAmerica, Region.EasternEurope, Region.NorthAmerica, Region.WesternEurope }
            },
            {
                Region.CIS,
                new() { Region.EasternEurope, Region.NorthAsia, Region.WesternEurope, Region.SouthAsia, Region.Australia, Region.NorthAmerica, Region.SouthAmerica }
            },
            {
                Region.EasternEurope,
                new() { Region.WesternEurope, Region.CIS, Region.NorthAsia, Region.SouthAsia, Region.NorthAmerica, Region.SouthAmerica, Region.Australia }
            },
            {
                Region.WesternEurope,
                new() { Region.EasternEurope, Region.CIS, Region.NorthAmerica, Region.SouthAmerica, Region.NorthAsia, Region.SouthAsia, Region.Australia }
            }
        };
    }
}
