# Private clinic management: Requirements Specification

----------
# **Business status**
## **Domestic affairs**
- President
- Specialized clinics
- Department of Medical Equipment and Supplies
- Integrated planning Department
- Financial Accounting
## **Foreign affairs**
- Ministry of Health
- Cho Ray Hospital
- Pharmaceutical companies


----------
# **Profession status**
## **Check-in**

**Patients**

- Get the order number to do the procedure.
- Present health insurance card, photo ID, medical profile and reservation details for the appointment.
- For cases of crossing the route (using medical services of facilities apart from default facilities), the patients seeking medical examination and requested treatment have to make advance payment.

**Hospital**

- Arrange counters to receive and check health insurance cards and medical documents.
- Enter the patient's information into the computer, determine the appropriate examination room, print the medical examination card and give the order number.
- Keep the health insurance card, hospital transfer records and appointment for re-examination.
- Collect advances for patients who have passed the route

        

## **Clinical examination and diagnosis**
> Depending on the condition of the disease, physicians may appoint tests, diagnostic pictures, functional probes or diagnoses and prescribe treatment without the need to specify laboratory tests.

**Patients**

- Wait for examination according to the serial number already recorded on the medical examination card.
- Go to the examination when notified.     

**Hospital**

- Notice and guide the patient throughout the diagnosis.
- Arrange clinical examination, specialty clinic.
- Examining, recording information about disease status, diagnosis and treatment indication.
- Prescribe drugs, print prescriptions
## **Payment**

**Patients**

- If the patient has health insurance:
  - Submit payment slip.
  - Queue for waiting for payment.
  - Pay money to pay and return health insurance card.
- Patients without health insurance pay hospital fees as prescribed.

**Hospital**

  - Check the statistical content in form, sign for certification.
  - Collect payment.
## **Medicine**

**Patients**

- Submit the prescription at the medicine counter.
- Check and compare drugs in prescription and received drugs.
- Receive prescription, medication and sign receipt.

**Hospital**

- Checking prescriptions, giving medicine.
- Counseling on patients on prescriptions and drugs.


# **IT Status**
## **Devices**
| **Device**                                  | **Quantity**                           | **Tech specs**                                                                                                                                                                                                       | **Location**                              | **Connectivity**         |
| ------------------------------------------- | -------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------- | ------------------------ |
| PC                                          | N/A (PCs are installed in every rooms) | - 1x CPU:≥ 2 cores, base clock ≥ 1500MHz<br>- 1x RAM stick: ≥ 2GB, DDR2/DDR3/DDR4<br>- 1x HDD: ≥ 1TB<br>- 1x Motherboard<br>- 1x Case<br>- 1x PSU: ≥ 300W, ~220V@60Hz<br>- 1x Mouse<br>- 1x Keyboard<br>- 1x Monitor | - Rooms<br>- Labs<br>- Counters           | Always connected (Wired) |
| Scanner<br>(for scanning medical documents) | 1                                      | - Document size: 216×297mm (A4)<br>- DPI: ≥ 6400<br>- Interface: USB 2.0<br>- Compatible with Windows 7 or later                                                                                                     | Check-in counter                          | Always connected (Wired) |
| Document printer                            | 3                                      | - Color printing capability.<br>- DPI: ≥ 600x600<br>- Paper tray capacities: ≥ 100 sheets of plain paper<br>- Paper size: ≥ A4<br>- Compatible with Windows 7 or later                                               | - Check-in counter<br>- Appointment room. | Always connected (Wired) |
| Slip printer                                | 1                                      | - Print method: Direct thermal<br>- Print speed: ≤ 300mm/sec<br>- Compatible with Windows 7 or later                                                                                                                 | Payment counter                           | Connected (Wired)        |
| Barcode scanner                             | 1                                      | - Scanner capability: 1D<br>- Connectivity: USB 2.0<br>- Compatible with Windows 7 or later                                                                                                                          | Payment counter                           | Not connected            |

## **Software & platforms**

**Operating systems**

- Windows 7 or later (for PCs).
- Password-encrypted.
- Updated with latest security patches.
- Updated driver software.

**Database management system**

- SQL (Oracle/MSSQL/MySQL/MariaDB/PostgresSQL).
- Cloud hosted.
- Database replication enabled.
- Limited permissions for different specialized positions.

**Other tools & platforms**

- Microsoft Office (word processing, spreadsheets).
- Medical software (for medical tests and diagnosis).
- Financial software (for expense and revenue management).

