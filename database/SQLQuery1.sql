USE PriClinic
create table Medicine
(
Med_ID varchar(5) primary key,
med_name varchar(30),
quantity varchar(10) ,
price money,
med_using varchar(max)
)

create table Treatment
(
Treat_ID varchar(5) primary key,
Med_ID varchar(5) FOREIGN KEY REFERENCES Medicine(Med_ID),
symptom varchar(max),
cost money,
)
create table Patient
(
Patient_ID varchar(5) primary key,
Pa_name varchar(100),
Yearofbirth date,
Gender char,
addres varchar(max)
)
create table Employee
(
Employee_ID varchar(5) primary key,
Em_name varchar(100),
Gender char,
Position varchar(20)
)
create table Doctor
(
Employee_ID varchar(5) FOREIGN KEY REFERENCES Employee(Employee_ID),
Specialist varchar(100)
)

create table DocAndPat
(
Patient_ID varchar(5) FOREIGN KEY REFERENCES Patient(Patient_ID),
Employee_ID varchar(5)FOREIGN KEY REFERENCES Employee(Employee_ID),
Appointment_ID varchar(5) primary key
)

create table MediRecord
(
Re_ID varchar(5) primary key,
Employee_ID varchar(5) FOREIGN KEY REFERENCES Employee(Employee_ID),
Patient_ID varchar(5) FOREIGN KEY REFERENCES Patient(Patient_ID),
Treat_ID varchar(5)  FOREIGN KEY REFERENCES Treatment(Treat_ID),
symptom varchar(max),
disease varchar(max),
Appointment_ID varchar(5) FOREIGN KEY REFERENCES DocAndPat (Appointment_ID)
)
