const Nexmo = require("nexmo");
var db = require('../database');

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
    const nexmo = new Nexmo({
      apiKey: "b8d18505",
      apiSecret: "MBOyEtcM9ZV64nhb"
    });

    var creation_time = new Date().addHours(7).toISOString().replace(/T/, ' ').replace(/\..+/, '');
    var newslot = `INSERT INTO AppointmentSlot (slot_time, slot_date, creation_time)
                  VALUES ('${requestBody.slot_time}', '${requestBody.slot_date}', '${creation_time}')`;
    
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

          let text = "Bạn đã đặt lịch khám thành công tại PriClinic." +
          " " +
          requestBody.appointment;
          console.log(requestBody.slot_date);
          
          const from = '45204';
          const to = '+84389255001';
          const option = {
            "type": "unicode"
          }

          

          /* nexmo.message.sendSms(from, to, text, option, (err, responseData) => {
            if (err) {
              console.log(err);
            } else {
              console.dir(responseData);
            }
          }); */
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
    db.query("SELECT * FROM AppointmentSlot", (err, slots) => res.json(slots));
  }
};

module.exports = appointmentController;
