using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DAL;
using Tes4._3_Tier.DTO;

namespace Tes4._3_Tier.BUS
{
    class AppointmentBUS
    {
        private AppointmentDAL appointDAL;
        public AppointmentBUS()
        {
            appointDAL = new AppointmentDAL();
            appointDAL.InitializeDB();
        }
        public List<AppointmentDTO> GetAppointments()
        {
            return appointDAL.GetAppointments();
        }

        public AppointmentDTO InsertAppointment(String n, int gend, String bd, String addr, String ph, String eml, String sldate, int sltime)
        {
            return appointDAL.Insert(n, gend, bd, addr, ph, eml, sldate, sltime);
        }

        public void DeleteAppointment(int id)
        {
            appointDAL.Delete(id);
        }
    }
}
