/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE `account` (
  `ID` int(11) NOT NULL,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Role` varchar(255) NOT NULL,
  `RefreshToken` varchar(255) DEFAULT NULL,
  `ExpiredDate` datetime DEFAULT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Username` (`Username`),
  CONSTRAINT `Account_fk0` FOREIGN KEY (`ID`) REFERENCES `user_manage` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `bankreference` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `debt_reminder` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `StkSend` varchar(20) NOT NULL,
  `StkReceive` varchar(20) NOT NULL,
  `SoTien` decimal(10,0) NOT NULL,
  `NoiDung` varchar(255) NOT NULL,
  `Status` int(11) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `notification` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `STKSend` varchar(20) NOT NULL,
  `STKReceive` varchar(20) NOT NULL,
  `Content` varchar(255) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `otp_table` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) DEFAULT NULL,
  `TransactionID` int(11) DEFAULT NULL,
  `OTP` varchar(255) NOT NULL,
  `CreateDate` datetime NOT NULL DEFAULT current_timestamp(),
  `ExpiredDate` datetime NOT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `paymentfeetype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` datetime DEFAULT NULL,
  `UpdatedBy` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `recipients` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `STK` varchar(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `UserID` int(255) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `BankID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Recipients_fk0` (`UserID`),
  CONSTRAINT `Recipients_fk0` FOREIGN KEY (`UserID`) REFERENCES `user_manage` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `transaction_banking` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `STKSend` varchar(20) NOT NULL,
  `STKReceive` varchar(20) NOT NULL,
  `Content` varchar(255) NOT NULL,
  `Money` decimal(10,0) NOT NULL,
  `TransactionTypeID` int(255) NOT NULL,
  `PaymentFeeTypeID` int(11) NOT NULL,
  `BankReferenceID` int(11) DEFAULT NULL,
  `RSA` varchar(255) DEFAULT NULL,
  `IsDebtRemind` tinyint(1) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `CreatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`),
  KEY `Transaction_banking_fk0` (`TransactionTypeID`),
  KEY `Transaction_banking_fk1` (`PaymentFeeTypeID`),
  KEY `Transaction_banking_fk2` (`BankReferenceID`),
  CONSTRAINT `Transaction_banking_fk0` FOREIGN KEY (`TransactionTypeID`) REFERENCES `transactiontype` (`ID`),
  CONSTRAINT `Transaction_banking_fk1` FOREIGN KEY (`PaymentFeeTypeID`) REFERENCES `paymentfeetype` (`ID`),
  CONSTRAINT `Transaction_banking_fk2` FOREIGN KEY (`BankReferenceID`) REFERENCES `bankreference` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `transactiontype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `user_manage` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Cmnd` varchar(255) NOT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `STK` varchar(20) NOT NULL,
  `SoDu` decimal(10,0) DEFAULT NULL,
  `BankKind` varchar(255) DEFAULT NULL,
  `Email` varchar(255) NOT NULL,
  `Phone` varchar(255) NOT NULL,
  `isStaff` tinyint(1) NOT NULL,
  `UpdatedDate` datetime DEFAULT current_timestamp(),
  `CreatedDate` datetime DEFAULT current_timestamp(),
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `account` (`ID`, `Username`, `Password`, `Role`, `RefreshToken`, `ExpiredDate`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(1, 'string', '$2a$11$2hNPaooAvrpprEOyHVIUUu6iyiVfrgE8BJaG9XCGhEJBDNb.UV9w6', '0', 'cOT4W4HcOupMZthPsGGJuzQrIdRY3YS9TnpNkEvhEkSdhlZDwLUYDOsIE4P5AvrtUDqKT7iehni4iJg/8sjgnQ==', '2023-01-19 00:21:44', NULL, NULL, NULL, NULL, 0);
INSERT INTO `account` (`ID`, `Username`, `Password`, `Role`, `RefreshToken`, `ExpiredDate`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(2, 'string12', '$2a$11$tJB0pUZsCMnGmKL6k8AiH.nVmluSCZUfDaBsZqwCjCPbzkmWKMnZm', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO `account` (`ID`, `Username`, `Password`, `Role`, `RefreshToken`, `ExpiredDate`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(3, 'string123', '$2a$11$9MsHool4TFkGYvaf4PL7wu3k65zG5gv.wEU0rmRvfVAATaho73.96', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO `account` (`ID`, `Username`, `Password`, `Role`, `RefreshToken`, `ExpiredDate`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(4, 'string1234', '$2a$11$AtJ6VCYNXgYJrNu9x8n68efCLgTRA.wsCMN5OD.clCyUFK4cGtLhW', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(5, 'string12345', '$2a$11$WPvb9TFYce2iWDW5UuyjQ.XZ7B2CuYiRINoCi0EnDr/wXZ6BVdd0a', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(6, 'stringk', '$2a$11$aUomBFdgGkXpY8gF1nS6s.uLG4fvzfPtu6YOPHp38rsFFez/PXrca', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(7, 'string123123', '$2a$11$kgAOFc7HxbLKg/wmnohaiOjYFE74Xwv2Jdodi1cDO2TkzWW/5hgxu', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(8, 'string1223123', '$2a$11$oaqoVUsV75hEIwF2LmUFFeHIavtLGb/rJ7esrBOFbCu7DKaomOQyu', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(9, 'string4324', '$2a$11$8lvnFVxlE1Rz2yn3MJ.TgOEUxNMYIamX/Vcy.N97jbWDhWdsrfr1C', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(10, 'stringkjj', '$2a$11$sFJaojkei8P.BGIVkFPA4ONe0wxQbBPiK.x6L41gI.NumqZQgor/a', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(11, 'stringkjj1', '$2a$11$eYBE/TgdCjilyB8NTlR0LePd29lVtHAFwgUqE3hx3QPndDKyLhp8i', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(12, 'stringkjj2', '$2a$11$XiTZINXrRUyoreIwdNreMOzjfLdrptg5UpnpeVhpFE0AJmg0uk7o2', '0', NULL, NULL, NULL, NULL, NULL, NULL, 0),
(13, 'khuong01', '$2a$11$ie85s0D8fLCaMJ4BMjs8aObd51CYrc8ITWYAKptu1XpEV1WVZ18HS', '1', 'j2AOLFclX+I1OP2SUNMMZhULg6BL2jiudXPF3Nsw6YxQj/xECp9qCL7bRlNfYzjHr8uy/EzGQkvdahyXHI4vsQ==', '2023-01-18 19:37:30', NULL, NULL, NULL, NULL, 0);

INSERT INTO `bankreference` (`ID`, `Name`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(1, 'Group1Bank', '2022-12-24 11:21:30', '2022-12-24 11:21:30', 'Admin', 'Admin', 0);
INSERT INTO `bankreference` (`ID`, `Name`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(2, 'Group2Bank', '2022-12-24 11:21:47', '2022-12-24 11:21:47', 'Admin', 'Admin', 0);








INSERT INTO `paymentfeetype` (`ID`, `Name`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(1, 'Người chuyển trả phí', '2022-12-26 15:03:55', '2022-12-26 15:03:55', NULL, NULL, 0);
INSERT INTO `paymentfeetype` (`ID`, `Name`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(2, 'Người nhận trả phí', '2022-12-26 15:03:56', '2022-12-26 15:03:56', NULL, NULL, 0);


INSERT INTO `recipients` (`ID`, `STK`, `Name`, `UserID`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`, `BankID`) VALUES
(37, '312312638', '11', 1, '2023-01-01 03:35:47', '2023-01-01 03:35:47', NULL, NULL, 0, 1);
INSERT INTO `recipients` (`ID`, `STK`, `Name`, `UserID`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`, `BankID`) VALUES
(38, '2312', 'rwer', 1, '2023-01-02 15:59:56', '2023-01-02 15:59:56', NULL, NULL, 0, 1);
INSERT INTO `recipients` (`ID`, `STK`, `Name`, `UserID`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`, `BankID`) VALUES
(39, '4234', 'dfsdf', 1, '2023-01-02 15:59:56', '2023-01-02 15:59:56', NULL, NULL, 0, 1);
INSERT INTO `recipients` (`ID`, `STK`, `Name`, `UserID`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`, `BankID`) VALUES
(40, '342', 'fsdfsd', 1, '2023-01-02 15:59:56', '2023-01-02 15:59:56', NULL, NULL, 0, 1),
(41, '3244', '3123', 1, '2023-01-02 15:59:57', '2023-01-02 15:59:57', NULL, NULL, 0, 1),
(42, '231231', '324f', 1, '2023-01-02 15:59:58', '2023-01-02 15:59:58', NULL, NULL, 0, 1);

INSERT INTO `transaction_banking` (`ID`, `STKSend`, `STKReceive`, `Content`, `Money`, `TransactionTypeID`, `PaymentFeeTypeID`, `BankReferenceID`, `RSA`, `IsDebtRemind`, `CreatedDate`, `UpdatedDate`, `UpdatedBy`, `CreatedBy`, `IsDeleted`) VALUES
(4, '463029405607', '1000002', 'string', 10000, 1, 1, 2, 'string', 0, '2022-12-27 14:35:00', NULL, NULL, NULL, 0);
INSERT INTO `transaction_banking` (`ID`, `STKSend`, `STKReceive`, `Content`, `Money`, `TransactionTypeID`, `PaymentFeeTypeID`, `BankReferenceID`, `RSA`, `IsDebtRemind`, `CreatedDate`, `UpdatedDate`, `UpdatedBy`, `CreatedBy`, `IsDeleted`) VALUES
(5, '463029405607', '1000002', 'Chuyen tien test 1', 10000, 1, 1, 2, 'XpYeFqi2pbvpBLiZgKfK++EYdBumem6IkQc0gBXlOeU03gd+ZoMVB+foS/TDXiKh/PsFJEgT0QsovZ/Pw9zZQhJwX6V67YUiSqYwY7R9Bl+UwTPgWNLLH6LicdDCRNKKr/yEdwFxLn27KB7urEyLAfoJskoWOk07SP1S0gDbdfc=', 0, '2022-12-27 14:54:42', NULL, NULL, NULL, 0);
INSERT INTO `transaction_banking` (`ID`, `STKSend`, `STKReceive`, `Content`, `Money`, `TransactionTypeID`, `PaymentFeeTypeID`, `BankReferenceID`, `RSA`, `IsDebtRemind`, `CreatedDate`, `UpdatedDate`, `UpdatedBy`, `CreatedBy`, `IsDeleted`) VALUES
(6, '463029405607', '1000002', 'Chuyen tien test 1', 10000, 1, 1, 2, 'V8F/ua1E27UhC7OU85X5cx2akWADVEnkBNG7qhshY2g3Y/81Pn9OFx+6azFrUUEJj4ziZuxnujlW+etpviee5SrW4W+xIqCFAHLYiJfhV2AGzPjGgxy+8XjHo590VJ6W/16GP7iU+fZe/rNUkx4r7onkup/kapSP9J7SxEIWJkA=', 0, '2022-12-27 15:11:41', NULL, NULL, NULL, 0);
INSERT INTO `transaction_banking` (`ID`, `STKSend`, `STKReceive`, `Content`, `Money`, `TransactionTypeID`, `PaymentFeeTypeID`, `BankReferenceID`, `RSA`, `IsDebtRemind`, `CreatedDate`, `UpdatedDate`, `UpdatedBy`, `CreatedBy`, `IsDeleted`) VALUES
(7, '463029405607', '1000002', 'Chuyen tien test 1', 10000, 1, 1, 2, 'evYAznjuyQOUKicWP3ouqrEBurx7NNzbFC5WzanNaM+YeyY0DHRBkHp66OYGKfd5gQMqlZ32bV+UluBOmDMbDM7J01dxMsnO9Qe+zvW38QmukiFa+6X+uKE3O5fLVYUnbk6oOke/QNvhyCfYMTpUAub9RvFgYmE9ZcLo/oy03MU=', 0, '2022-12-27 15:13:30', NULL, NULL, NULL, 0),
(8, '463029405607', '1000002', 'Chuyen tien test 1', 10000, 1, 1, 2, 'UvzEQK8PAi3TpQovrLGO+iWBDnhG5Mjbcyx2sH8rz/FNEzomByArI9ilWlB22LlLYWrukfon1lTAIeeNIUF7AJe3vaAasQYz2iaX6/apy3kuMaHvTQq076my/TqxdhvigbGKSLiZnfzcKVf4xAJhjvels00EQAbGIb0SqjgIinQ=', 0, '2022-12-27 15:16:04', NULL, NULL, NULL, 0),
(9, '463029405607', '1000002', 'Chuyen tien test 1', 10000, 1, 1, 2, 'Wu7uUbDTl28EwtSNvpAPtRQz7ncKXXOj48AE8xelSojq0KrgdW76qctUNdfDdqeFBfOUR/ctMwxgyBDYGKeHJqWH8yXU6mVgj+zbu9PRN61MBPWwwsIitFHfAOjq5yB8M4SpfkHS/MJfcDgCzTc10fIx3IP+tGXYu0mn1TxrzHE=', 0, '2022-12-27 15:17:19', NULL, NULL, NULL, 0),
(10, '200797396183', '1000002', 'KHA Chuyển tiền liên ngân hàng cho nhóm 1', 750000, 1, 1, 2, 'reCuN2QKVNIB2HkkTLa/6RS/yc4BRJr+IRPicApmOFzEObtqQ8CBgU9CfRQR/wD8AzAwwOKYMQDv6lzHj48s4b4X922EXWyLrVSAcCYD5L3mXEIqHzrnYSY7HKZ7GetW8r1sP6xWpvdBP1FzaAxEs2x8Bh9EB91H8/hKl8uMwTA=', 0, '2022-12-27 17:37:28', NULL, NULL, NULL, 0),
(11, '200797396183', '1000001', 'NGUYỄN HOÀI KHA Chuyển tiền Nhóm 1', 79000, 1, 1, 2, 'jIO9YMspi9zwHk8+oex8Py2GU6m9BRIW1vaSQTpiqyd+RmudWMBAO/w70FpvB039FkD9hCE/CT6esewWSQTXHYmIaqwFYRl4UZUh7voe+kNZH7dW1V7g5W9RUYiE3efbd32N6IE+8NDF57VDaQmgrMPjkTDxn+JLMDQrf4JCtCg=', 0, '2022-12-27 18:10:48', NULL, NULL, NULL, 0),
(12, '200797396183', '1000001', 'Nhóm 12 thử api chuyển tiền liên ngân hàng cho nhóm 1', 66000, 1, 1, 2, 'SsFIVriVaGAQIbUHT376XJBbHB4WJj6MniBLSSLBluc5DOu+hSqjz9PbqVFG2k9kmSWSfre6yRb+JNsdWrtWMQeMQjbqqE3gk9g7ZF+UMLywQFgb/yGAfoIFMT7dsm8QFpddRR7FinVLeKC8f1gZ/Tmf9UVlYMWc4Mp358cTzzQ=', 0, '2022-12-27 18:12:41', NULL, NULL, NULL, 0),
(13, '200797396183', '1000001', 'KHA Chuyển tiền cho Nhóm 1', 94000, 1, 1, 2, 'mA3CpXRnMs8JNnE3OiBSA+3JfDPsBkc2L18a/yCuEKWZ81bBecsQnJVWHZBJFYDJVwVfcSzmKoOTKynbFWLEGaxktxGR2JRnHgDdwWc9z89G7o9FDEWbu1sYKREHD8v0AKIaMXTSIyfVr+NI4J31hv5TtPn5YxZVR14iTx+Rjn0=', 0, '2022-12-28 02:23:44', NULL, NULL, NULL, 0),
(15, '1000001', '493747116445', 'Chuyen tien nhieu nhieu nhieu', 500000, 1, 1, 1, 'fLT5EMNRfv19TEpj0cpjYNUj/kwlGklyD9UQfIp9ydZJLLnPxYlklj+IOGVqPtumFL6V5AbTJXDVhFWhsqYnN1UmRIsV/0RwFFt5PF4Nplc3YFR/fJbZAaUj1g3GT5N/n+bYn0g7B+QDZmthA62W0jdvBn3yXVljKjRKlCOhLWo=', 0, '2022-12-28 10:16:12', NULL, NULL, NULL, 0),
(17, '1000000', '22852329', 'string', 5000, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0);

INSERT INTO `transactiontype` (`ID`, `Name`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(1, 'Chuyển nội bộ', '2022-12-24 11:22:26', '2022-12-24 11:22:26', 'Admin', 'Admin', 0);
INSERT INTO `transactiontype` (`ID`, `Name`, `CreatedDate`, `UpdatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(2, 'Chuyển liên ngân hàng', '2022-12-24 11:22:26', '2022-12-24 11:22:26', 'Admin', 'Admin', 0);


INSERT INTO `user_manage` (`ID`, `Name`, `Cmnd`, `Address`, `STK`, `SoDu`, `BankKind`, `Email`, `Phone`, `isStaff`, `UpdatedDate`, `CreatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(1, 'string', 'string', 'string', '1000000', 100000000, 'string', 'string', 'string', 0, NULL, '2022-12-24 18:11:13', NULL, NULL, 0);
INSERT INTO `user_manage` (`ID`, `Name`, `Cmnd`, `Address`, `STK`, `SoDu`, `BankKind`, `Email`, `Phone`, `isStaff`, `UpdatedDate`, `CreatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(2, 'string', 'string', 'string', '10001', 100000000, 'string', 'string12', 'string12', 0, NULL, '2022-12-24 18:14:49', NULL, NULL, 0);
INSERT INTO `user_manage` (`ID`, `Name`, `Cmnd`, `Address`, `STK`, `SoDu`, `BankKind`, `Email`, `Phone`, `isStaff`, `UpdatedDate`, `CreatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(3, 'string', 'string', 'string', '10002', 100000000, 'string', 'string123', 'string123', 0, NULL, '2022-12-24 18:15:55', NULL, NULL, 0);
INSERT INTO `user_manage` (`ID`, `Name`, `Cmnd`, `Address`, `STK`, `SoDu`, `BankKind`, `Email`, `Phone`, `isStaff`, `UpdatedDate`, `CreatedDate`, `CreatedBy`, `UpdatedBy`, `IsDeleted`) VALUES
(4, 'string', 'string', 'string', '1000005', 100000000, 'string', 'string1234', 'string1234', 0, NULL, '2022-12-24 18:16:52', NULL, NULL, 0),
(5, 'string', 'string', 'string', '1000001', 99594000, 'string', 'string12345', 'string12345', 0, NULL, '2022-12-24 18:17:22', NULL, NULL, 0),
(6, 'string', 'string', 'string', '1000002', 100000000, 'string', 'stringk', 'stringk', 0, NULL, '2022-12-24 18:19:20', NULL, NULL, 0),
(7, 'string', 'string', 'string', '22841439', 100000000, 'string', 'string123123', 'string123123', 0, NULL, '2022-12-24 18:37:34', NULL, NULL, 0),
(8, 'string', 'string', 'string', '22841500', 100000000, 'string', 'string3123', 'string12312', 0, NULL, '2022-12-24 18:40:12', NULL, NULL, 0),
(9, 'string', 'string', 'string', '22841501', 100000000, 'string', 'string34', 'string34', 0, NULL, '2022-12-24 18:42:30', NULL, NULL, 0),
(10, 'string', 'string', 'string', '22841438', 100000000, 'string', 'stringkjj', 'stringkjj', 0, NULL, '2022-12-24 18:57:22', NULL, NULL, 0),
(11, 'string', 'string', 'string', '22389583', 100000000, 'string', 'stringkjj1', 'stringkjj1', 0, NULL, '2022-12-24 18:57:59', NULL, NULL, 0),
(12, 'string', 'string', 'string', '22852329', 100005000, 'string', 'stringkjj2', 'stringkjj2', 0, NULL, '2022-12-24 18:59:50', NULL, NULL, 0),
(13, 'khuong', '23123123', 'fdsf', '1000045', 4234, 'string', 'nakhuong@gmail.com', '123', 1, NULL, '2022-12-29 14:22:04', NULL, NULL, 0);


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;