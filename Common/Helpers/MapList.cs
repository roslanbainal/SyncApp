using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class MapList
    {
        public static CombineModel AddList(List<PlatformModel> listPlatform)
        {
            List<PlatformModel> newListPlatform = new List<PlatformModel>();
            List<WellModel> newListWell = new List<WellModel>();

            foreach (PlatformModel platform in listPlatform)
            {
                foreach (WellModel well in platform.Well)
                {
                    newListWell.Add(well);
                }

                newListPlatform.Add(platform);
            }

            return new CombineModel
            {
                ListPlatform = newListPlatform,
                ListWell = newListWell
            };
        }
    }
}
