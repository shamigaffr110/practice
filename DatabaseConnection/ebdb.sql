use  ElectricityBillDB

-- =============================
-- 1. Users Table
-- =============================
CREATE TABLE Users (
    user_id INT IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    phone VARCHAR(15) NOT NULL,
    email VARCHAR(50) NOT NULL UNIQUE,
    dob DATE NULL,
    password VARCHAR(50) NOT NULL
);

-- =============================
-- 2. Admins Table
-- =============================
CREATE TABLE Admins (
    admin_id INT IDENTITY PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(50) NOT NULL
);

-- =============================
-- 3. Connections Table
-- =============================
CREATE TABLE Connections (
    connection_id INT IDENTITY PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES Users(user_id),
    consumer_number VARCHAR(20) UNIQUE, -- Assigned by admin later
    name VARCHAR(50),
    phone VARCHAR(15),
    email VARCHAR(50),
    address VARCHAR(100),
    locality VARCHAR(50),
    type VARCHAR(20),               -- Urban / Local
    load INT,                       -- kW
    connection_type VARCHAR(20),    -- Single / Three
    id_proof_path VARCHAR(150),
    photo_path VARCHAR(150),
    status VARCHAR(20) NOT NULL DEFAULT 'Pending', -- Pending / Active / Inactive
    created_at DATETIME NOT NULL DEFAULT GETDATE()
);

-- =============================
-- 4. ElectricityBill Table (Updated)
-- =============================
CREATE TABLE ElectricityBill (
    bill_id INT IDENTITY PRIMARY KEY,
    consumer_number VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Connections(consumer_number),
    consumer_name VARCHAR(50) NOT NULL,
    units_consumed INT NOT NULL,
    bill_amount FLOAT NOT NULL,
    bill_date DATETIME NOT NULL DEFAULT GETDATE(),
    due_date DATETIME NOT NULL DEFAULT DATEADD(DAY, 15, GETDATE()),
    status VARCHAR(20) NOT NULL DEFAULT 'Unpaid' -- Paid / Unpaid
);

-- =============================
-- 5. Payments Table
-- =============================
CREATE TABLE Payments (
    payment_id INT IDENTITY PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES Users(user_id),
    consumer_number VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Connections(consumer_number),
    amount FLOAT NOT NULL,
    payment_date DATETIME NOT NULL DEFAULT GETDATE(),
    method VARCHAR(20) NOT NULL, -- UPI / QR / NetBanking etc.
    status VARCHAR(20) NOT NULL, -- Success / Failed
    txn_ref VARCHAR(50) NULL
);

-- =============================
-- 6. Concerns Table
-- =============================
CREATE TABLE Concerns (
    concern_id INT IDENTITY PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES Users(user_id),
    consumer_number VARCHAR(20),
    message VARCHAR(200) NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'Open', -- Open / Resolved
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    resolved_at DATETIME NULL
);

-- =============================
-- 7. Notices Table
-- =============================
CREATE TABLE Notices (
    notice_id INT IDENTITY PRIMARY KEY,
    consumer_number VARCHAR(20) NOT NULL,
    message VARCHAR(200) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE()
);

-- =============================
-- 8. Seed Admin and Sample User
-- =============================
INSERT INTO Admins(username, password) VALUES ('admin', '12345');
INSERT INTO Users(name, phone, email, dob, password) 
VALUES ('Shami', '7991138923', 'xyz@gmail.com', '2002-02-25', 'Sh@12345');

-- =============================
-- 9. Indexes (Optional but Recommended)
-- =============================
CREATE INDEX IX_Payments_ConsumerNumber ON Payments(consumer_number);
CREATE INDEX IX_ElectricityBill_ConsumerNumber ON ElectricityBill(consumer_number);


INSERT INTO Connections(user_id, name, phone, email, address, locality, type, load, connection_type, id_proof_path, photo_path, status)
VALUES (
    1, -- User ID (Shami)
    'Shami',
    '7991138923',
    'xyz@gmail.com',
    '123 Street, Ranchi',
    'Lalpur',
    'Urban',
    5,
    'Single',
    'Uploads/IdProof/sample_id.jpg',
    'Uploads/Photo/sample_photo.jpg',
    'Active'
);
-------------------------------------

UPDATE Connections
SET consumer_number = 'CN001'
WHERE connection_id = 1;
-------------------------------
INSERT INTO ElectricityBill(consumer_number, consumer_name, units_consumed, bill_amount, bill_date, due_date, status)
VALUES 
('CN001', 'Shami', 150, 1200.00, '2025-07-01', '2025-07-15', 'Paid'),
('CN001', 'Shami', 220, 1800.00, '2025-08-01', '2025-08-15', 'Unpaid');
--------------------------------------------------------------------------
INSERT INTO Payments(user_id, consumer_number, amount, payment_date, method, status, txn_ref)
VALUES 
(1, 'CN001', 1200.00, '2025-07-05', 'UPI', 'Success', 'TXN123456');

-----------------------------------------------------------------------------
INSERT INTO Concerns(user_id, consumer_number, message, status, created_at)
VALUES 
(1, 'CN001', 'Why is the latest bill higher than usual?', 'Open', GETDATE());

------------------------------------------------------------------------------------

SELECT * FROM Users;
SELECT * FROM Connections;
SELECT * FROM ElectricityBill;
SELECT * FROM Payments;
SELECT * FROM Concerns;
SELECT * FROM Notices;


-- 1. Create new user
INSERT INTO Users(name, phone, email, dob, password)
VALUES ('Shami Gaffar', '9123456789', 'shamigaffar1110@gmail.com', '1990-05-15', 'Ravi@123');

-- 2. Create a new connection (assume this is user_id = 2 based on identity increment)
INSERT INTO Connections(user_id, name, phone, email, address, locality, type, load, connection_type, id_proof_path, photo_path, status)
VALUES (
    2, -- Newly inserted user ID
    'Shami Gaffar',
    '9123456789',
    'shamigaffar1110@gmail.com',
    '456 Park Lane, Patna',
    'Boring Road',
    'Urban',
    10,
    'Three',
    'Uploads/IdProof/ravi_id.jpg',
    'Uploads/Photo/ravi_photo.jpg',
    'Active'
);

-- 3. Update the connection with a consumer number
UPDATE Connections
SET consumer_number = 'CN002'
WHERE user_id = 2;

-- 4. Insert electricity bill (more than 1 year old, >50000, unpaid)

INSERT INTO ElectricityBill(consumer_number, consumer_name, units_consumed, bill_amount, bill_date, due_date, status)
VALUES (
    'CN002',
    'Ravi Kumar',
    500000,              
    62500.00,          
    '2024-08-01',       
    '2024-08-15',
    'Unpaid'
);



select * from users

select * from ElectricityBill