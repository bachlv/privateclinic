using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;
namespace Tes4._3_Tier.BUS
{
    class GeoInfo_BUS
    {
        private GeoInfo_DAL geoDAL;
        public GeoInfo_BUS()
        {
            geoDAL = new GeoInfo_DAL();
            geoDAL.InitializeDB();
        }
        public List<string> getProvince()
        { 
            return geoDAL.getProvince();
        }
        public List<string> getDistrict(string dieukien)
        {
            return geoDAL.getDistrict(dieukien);
        }
        public List<string> getTown(string dieukien)
        {
            return geoDAL.getTown(dieukien);
        }
    }
}
