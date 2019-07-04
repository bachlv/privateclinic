var mysql = require('mysql');
var connection = mysql.createConnection({
    host: "35.185.184.170",
    user: "lac",
    password: "laclaclaclaclac",
    database: "PriClinic",
    dateStrings: true
});

connection.connect(function(err) {
    if (err) throw err;
});

module.exports = connection;