CREATE TABLE `Account` (
	`ID` INT NOT NULL,
	`Username` varchar(255) NOT NULL,
	`Password` varchar(255) NOT NULL,
	`Role` varchar(255) NOT NULL,
	`RefreshToken` varchar(255),
	`ExpiredDate` DATETIME,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` VARCHAR(255),
	`UpdatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `User_manage` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` varchar(255) NOT NULL,
	`Cmnd` varchar(255) NOT NULL,
	`Address` varchar(255),
	`STK` varchar(255) NOT NULL,
	`SoDu` DECIMAL NOT NULL,
	`BankKind` varchar(255) NOT NULL,
	`Email` varchar(255) NOT NULL,
	`Phone` varchar(255) NOT NULL,
	`isStaff` BOOLEAN NOT NULL,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` VARCHAR(255),
	`UpdatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `Recipients` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`STK` varchar(255) NOT NULL,
	`Name` varchar(255) NOT NULL,
	`UserID` INT(255) NOT NULL,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` VARCHAR(255),
	`UpdatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `Transaction_banking` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`STKSend` VARCHAR(20) NOT NULL,
	`STKReceive` VARCHAR(20) NOT NULL,
	`Content` varchar(255) NOT NULL,
	`Money` DECIMAL NOT NULL,
	`TransactionTypeID` INT(255) NOT NULL,
	`PaymentFeeTypeID` INT NOT NULL,
	`BankReferenceID` INT,
	`RSA` VARCHAR(255),
	`IsDebtRemind` BOOLEAN NOT NULL DEFAULT FALSE,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedBy` VARCHAR(255),
	`CreatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `Debt_reminder` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`StkSend` varchar(255) NOT NULL,
	`StkReceive` varchar(255) NOT NULL,
	`SoTien` DECIMAL NOT NULL,
	`NoiDung` VARCHAR(255) NOT NULL,
	`Status` INT NOT NULL,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` VARCHAR(255),
	`UpdatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `PaymentFeeType` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(255) NOT NULL,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` DATETIME,
	`UpdatedBy` DATETIME,
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `TransactionType` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(255) NOT NULL,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` VARCHAR(255),
	`UpdatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `BankReference` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(255) NOT NULL,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` VARCHAR(255),
	`UpdatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `OTP_Table` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`UserID` INT,
	`TransactionID` INT,
	`OTP` VARCHAR(255) NOT NULL,
	`CreateDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`ExpiredDate` DATETIME NOT NULL,
	`Status` BOOLEAN,
	PRIMARY KEY (`ID`)
);


CREATE TABLE `Notification` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(255) NOT NULL,
	`STKSend` VARCHAR(20) NOT NULL,
	`STKReceive` VARCHAR(20) NOT NULL,
	`Content` varchar(255) NOT NULL,
	`CreatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`UpdatedDate` DATETIME DEFAULT CURRENT_TIMESTAMP,
	`CreatedBy` VARCHAR(255),
	`UpdatedBy` VARCHAR(255),
	`IsDeleted` BOOLEAN NOT NULL DEFAULT FALSE,
	PRIMARY KEY (`ID`)
);

ALTER TABLE `Account` ADD CONSTRAINT `Account_fk0` FOREIGN KEY (`ID`) REFERENCES `User_manage`(`ID`);

ALTER TABLE `Recipients` ADD CONSTRAINT `Recipients_fk0` FOREIGN KEY (`UserID`) REFERENCES `User_manage`(`ID`);

ALTER TABLE `Transaction_banking` ADD CONSTRAINT `Transaction_banking_fk0` FOREIGN KEY (`TransactionTypeID`) REFERENCES `TransactionType`(`ID`);

ALTER TABLE `Transaction_banking` ADD CONSTRAINT `Transaction_banking_fk1` FOREIGN KEY (`PaymentFeeTypeID`) REFERENCES `PaymentFeeType`(`ID`);

ALTER TABLE `Transaction_banking` ADD CONSTRAINT `Transaction_banking_fk2` FOREIGN KEY (`BankReferenceID`) REFERENCES `BankReference`(`ID`);

ALTER TABLE `Debt_reminder` ADD CONSTRAINT `Debt_reminder_fk0` FOREIGN KEY (`STK`) REFERENCES `User_manage`(`STK`);

ALTER TABLE `Account` ADD UNIQUE (`Username`);

ALTER TABLE `User_manage` ADD UNIQUE (`Email`);

ALTER TABLE `Recipients`ADD COLUMN `BankID` INT NOT NULL REFERENCES `BankReference`(`id`);
