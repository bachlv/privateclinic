CREATE DATABASE PriClinic;

USE PriClinic;

CREATE TABLE Medicine (
	med_id VARCHAR(5) NOT NULL PRIMARY KEY,
	name VARCHAR(30),
	quantity VARCHAR(10),
	price DECIMAL (15,2),
	med_usage TEXT
);

CREATE TABLE Treatment (
	treat_id VARCHAR(5) NOT NULL PRIMARY KEY,
	med_id VARCHAR(5),
	symptom TEXT,
	cost DECIMAL (15,2),
	FOREIGN KEY (med_id) REFERENCES Medicine(med_id)
);

CREATE TABLE Patient (
	patient_id VARCHAR(5) Not NULL PRIMARY KEY,
	name VARCHAR(100),
	birthday DATE,
	gender CHAR,
	addess TEXT
);

CREATE TABLE Employee (
	employee_id VARCHAR(5) NOT NULL PRIMARY KEY,
	name VARCHAR(100),
	gender CHAR,
	position VARCHAR(20)
);

CREATE TABLE Doctor (
	employee_id VARCHAR(5) NOT NULL,
	specialist varchar(100),
	FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

CREATE TABLE DoctorPatient (
	patient_id VARCHAR(5),
	employee_id VARCHAR(5),
	appointment_id VARCHAR(5) NOT NULL PRIMARY KEY,
	FOREIGN KEY (patient_id) REFERENCES Patient(patient_id),
	FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

CREATE TABLE MedicalRecord (
	record_ID VARCHAR(5) NOT NULL PRIMARY KEY,
	employee_id VARCHAR(5),
	patient_id VARCHAR(5),
	treat_id VARCHAR(5),
	symptom TEXT,
	disease TEXT,
	appointment_id VARCHAR(5),
	FOREIGN KEY (appointment_id) REFERENCES DoctorPatient(appointment_id),
	FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
	FOREIGN KEY (patient_id) REFERENCES Patient(patient_id),
	FOREIGN KEY (treat_id) REFERENCES Treatment(treat_id)
)