using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Forms;
using Tes4._3_Tier.DTO;

namespace Tes4._3_Tier.DAL
{
    class AppointmentDAL
    {
        private static MySqlConnection dbConn;

        public void InitializeDB()
        {
            string connString = ConfigurationManager.AppSettings["MySQL"];

            Console.WriteLine(connString);

            dbConn = new MySqlConnection(connString);

            Application.ApplicationExit += (sender, args) =>
            {
                if (dbConn != null)
                {
                    dbConn.Dispose();
                    dbConn = null;
                }
            };

        }

        public List<AppointmentDTO> GetAppointments()
        {
            List<AppointmentDTO> appointments = new List<AppointmentDTO>();

            String query = "SELECT app.*, slot.slot_date, slot.slot_time FROM Appointment app JOIN AppointmentSlot slot ON app.slot_id = slot.slot_id; ";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["appointment_id"];
                String n = reader["name"].ToString();
                int gender = (int)reader["gender"];
                String yob = Convert.ToDateTime(reader["birthday"]).ToString("dd/MM/yyyy");
                String addr = reader["address"].ToString();
                String phone = reader["phone"].ToString();
                String email = reader["email"].ToString();
                String slotdate = Convert.ToDateTime(reader["slot_date"]).ToString("dd/MM/yyyy");
                int slottime = 1;
                AppointmentDTO appt = new AppointmentDTO(id, n, gender, yob, addr, phone, email, slotdate, slottime);
                appointments.Add(appt);
            }

            reader.Close();

            dbConn.Close();


            return appointments;
        }

        public AppointmentDTO Insert(String n, int gend, String bd, String addr, String ph, String eml, String sldate, int sltime)
        {
            String insertSlot = string.Format("INSERT INTO AppointmentSlot (slot_time, slot_date, creation_time) VALUES " +
                "({0}, '{1}', '{2}')", sltime, sldate, DateTime.Today.ToString("yyyy-MM-dd"));
            MySqlCommand cmd = new MySqlCommand(insertSlot, dbConn);
            dbConn.Open();
            cmd.ExecuteNonQuery();
            int slotID = (int)cmd.LastInsertedId;

            String insertAppt = string.Format("INSERT INTO Appointment (name, gender, birthday, address, phone, email, slot_id) VALUES " +
                "('{0}', {1}, '{2}', '{3}', '{4}', '{5}', {6})", n, gend, bd, addr, ph, eml, slotID);
            MySqlCommand cmd2 = new MySqlCommand(insertAppt, dbConn);
            cmd2.ExecuteNonQuery();
            int apptID = (int)cmd2.LastInsertedId;
            AppointmentDTO p = new AppointmentDTO(apptID, n, gend, bd, addr, ph, eml, sldate, sltime);
            dbConn.Close();

            return p;

        }

        public void Delete(int id)
        {
            String query = string.Format("DELETE FROM Appointment WHERE appointment_id={0}", id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
    }
}
