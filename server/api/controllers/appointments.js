const Nexmo = require("nexmo");
const moment = require("moment");
const db = require('../database');
const nexmo = new Nexmo({
  apiKey: "b8d18505",
  apiSecret: "MBOyEtcM9ZV64nhb"
});


Date.prototype.addHours= function(h){
  this.setHours(this.getHours()+h);
  return this;
}

const appointmentController = {
  all(req, res) {
    // Returns all appointments
    // Appointment.find({}).exec((err, appointments) => res.json(appointments));
    db.query("SELECT * FROM Appointment", (err, appointments) => res.json(appointments));
  },
  create(req, res) {

    var requestBody = req.body;
  
    var creation_time = new Date().addHours(7).toISOString().replace(/T/, ' ').replace(/\..+/, '');
    var newslot = `INSERT INTO AppointmentSlot (slot_time, slot_date, creation_time)
                  VALUES ('${requestBody.slot_time}', '${requestBody.slot_date}', '${creation_time}')`;
    var time;
    switch (requestBody.slot_time) {
      case 0:
        time = "9h sáng";
        break;
      case 1:
        time = "10h sáng";
        break;
      case 2:
        time = "11h trưa";
        break;
      case 3:
        time = "12h trưa";
        break;
      case 4:
        time = "13h chiều";
        break;
      case 5:
        time = "14h chiều";
        break;
      case 6:
        time = "15h chiều";
        break;
      case 7:
        time = "16h chiều";
    }
    
    db.query(newslot, (err, savedslot) => {
      if (err) throw err;
      var newappointment = `INSERT INTO Appointment (name, email, phone, slot_id)
                    VALUES ('${requestBody.name}', '${requestBody.email}',
                    '${requestBody.phone}', ${savedslot.insertId})`;
      db.query(newappointment, (err, savedappointment) => { 
        if (err) throw err;
        var validation = `SELECT * FROM Appointment WHERE appointment_id = ${savedappointment.insertId};`
        db.query(validation, (err, result) => {
          if (err) throw err;
          res.json(result);

          const dateString = moment(requestBody.slot_date, "YYYY-MM-DD").locale('vi').format("dddd[, ngày] LL");
          var text = "Cảm ơn " + requestBody.name + " đã đặt lịch. Vui lòng đến khám vào " 
            + dateString + " lúc " + time + ".";
          
          var from = '45204';
          var to = "+84" + requestBody.phone.substr(1);
          const option = {
            "type": "unicode"
          }

          nexmo.message.sendSms(from, to, text, option, (err, responseData) => {
            if (err) {
              console.log(err);
            } else {
              console.dir(responseData);
            }
          });
        });
      });
    });
    
    //var newapp = `INSERT INTO Appointment (name, email, phone, slot_id) VALUES ('${requestBody.name}', '${requestBody.email}', '${requestBody.phone}', ${newslotID})`;
    

    
    // Creates a new record from a submitted form
    /* var newappointment = new Appointment({
      name: requestBody.name,
      email: requestBody.email,
      phone: requestBody.phone,
      slots: newslot._id
    }); */

    


    // and saves the record to
    // the data base
    /* newappointment.save((err, saved) => {
      // Returns the saved appointment
      // after a successful save
      Appointment.find({ _id: saved._id })
        .populate("slots")
        .exec((err, appointment) => res.json(appointment));

      const from = 1;
      const to = 2;

      nexmo.message.sendSms(from, to, msg, (err, responseData) => {
        if (err) {
          console.log(err);
        } else {
          console.dir(responseData);
        }
      });
    }); */
  },
  getSlots (req, res) {
    db.query(`SELECT slot_date, slot_time, COUNT(*) AS singleSlotCount
    FROM AppointmentSlot GROUP BY slot_date, slot_time
    HAVING singleSlotCount > ${process.env.maxSingleSlotCount - 1 || 4}`, (err, slots) => res.json(slots));
  }
};

module.exports = appointmentController;
