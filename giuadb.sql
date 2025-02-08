CREATE DATABASE  IF NOT EXISTS `guiadb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `guiadb`;
-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: guiadb
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20250207034949_MigracaoInicial','8.0.2'),('20250207043043_AjusteTabelas','8.0.2'),('20250207043645_AddTipoFuncionario','8.0.2'),('20250207062750_AjusteUser','8.0.2');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `ClienteId` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Telefone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ClienteId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (1,'Carlos Souza','(79) 98765-4321'),(2,'Ana Paula Lima','(21) 99876-5432'),(3,'Pedro Henrique Costa','(31) 98765-1234'),(4,'Juliana Almeida','(41) 99876-6543');
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `moteis`
--

DROP TABLE IF EXISTS `moteis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `moteis` (
  `MotelId` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(70) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Endereco` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MotelId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `moteis`
--

LOCK TABLES `moteis` WRITE;
/*!40000 ALTER TABLE `moteis` DISABLE KEYS */;
INSERT INTO `moteis` VALUES (1,'Motel Lua de Mel','Avenida das Flores, 123'),(2,'Motel Estrela Cadente','Rua dos Pinheiros, 456');
/*!40000 ALTER TABLE `moteis` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reservas`
--

DROP TABLE IF EXISTS `reservas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservas` (
  `ReservaId` int NOT NULL AUTO_INCREMENT,
  `DataEntrada` datetime(6) NOT NULL,
  `DataSaida` datetime(6) NOT NULL,
  `Valor` decimal(12,2) NOT NULL,
  `ClienteId` int NOT NULL,
  `SuiteId` int NOT NULL,
  PRIMARY KEY (`ReservaId`),
  KEY `IX_Reservas_ClienteId` (`ClienteId`),
  KEY `IX_Reservas_SuiteId` (`SuiteId`),
  CONSTRAINT `FK_Reservas_Clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `clientes` (`ClienteId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Reservas_Suites_SuiteId` FOREIGN KEY (`SuiteId`) REFERENCES `suites` (`SuiteId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservas`
--

LOCK TABLES `reservas` WRITE;
/*!40000 ALTER TABLE `reservas` DISABLE KEYS */;
INSERT INTO `reservas` VALUES (1,'2024-05-10 14:00:00.000000','2024-05-11 12:00:00.000000',200.00,1,2),(2,'2024-05-15 18:00:00.000000','2024-05-16 16:00:00.000000',350.00,2,5),(3,'2024-05-20 10:00:00.000000','2024-05-21 08:00:00.000000',150.00,3,1),(4,'2024-05-25 22:00:00.000000','2024-05-26 20:00:00.000000',400.00,4,6),(5,'2024-05-30 16:00:00.000000','2024-05-31 14:00:00.000000',250.00,1,3);
/*!40000 ALTER TABLE `reservas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suites`
--

DROP TABLE IF EXISTS `suites`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suites` (
  `SuiteId` int NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Preco` decimal(10,2) NOT NULL,
  `MotelId` int NOT NULL,
  PRIMARY KEY (`SuiteId`),
  KEY `IX_Suites_MotelId` (`MotelId`),
  CONSTRAINT `FK_Suites_Moteis_MotelId` FOREIGN KEY (`MotelId`) REFERENCES `moteis` (`MotelId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suites`
--

LOCK TABLES `suites` WRITE;
/*!40000 ALTER TABLE `suites` DISABLE KEYS */;
INSERT INTO `suites` VALUES (1,'Prata',150.00,1),(2,'Ouro',250.00,1),(3,'Diamante',350.00,1),(4,'Prata',180.00,1),(5,'Ouro',300.00,1),(6,'Diamante',400.00,1),(7,'Prata',120.00,2),(8,'Ouro',200.00,2),(9,'Diamante',300.00,2),(10,'Prata',160.00,2),(11,'Ouro',280.00,2),(12,'Diamante',380.00,2);
/*!40000 ALTER TABLE `suites` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TipoFuncionario` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (8,'José Marco','jose.marco@teste.com','$2a$11$P/vGDOR7XKCmyx0JgkfjeuCO.DpVDAZPOdUEZMoSqbBJkfXzhzuci','webmaster'),(9,'Marco Melo','marco.melo@teste.com','$2a$11$GCh5GhCicRwaVgNLe4DMEuEFEXdcCg6MiT5Ici9RODaanBNPDW4HK','suporte'),(10,'Marco','marco@teste.com','$2a$11$Z4sMEuDg48qGR3efLE7fKuu9gcL4SdvX96YI9ZrCQa0n3k/3TkN1m','webmaster'),(11,'Matues','mateus@teste.com','$2a$11$UHnmZNFhjct/aX/GuidtF.opFe7u3.U9gcYyNIVtPG1bksYi2V.9W','suporte'),(13,'Douglas','douglas@teste.com','$2a$11$mNR54oDFvdbMJ5e4lBNZp.JHdfEVlWcBiY0qDlJ./NrbsaEgdY9CC','suporte'),(15,'Raimundo','raimundo@teste.com','$2a$11$bvPWqq4.W9FbqLW92R4Kl.MMGzsdfHu2sk6djl/5hPut6r05qgiU2','suporte'),(16,'Cauã','caua@teste.com','$2a$11$/lDZDE5n6Gxy9C3zc/HO5epIHy9OHyOT5087G/YUdHwYqDyv0iX9i','suporte'),(20,'melo','melo@teste.com','$2a$11$fnJ56O5UXBnJ24Kjw1H75u0/JgIbl5dz6R1CYRf5dxeF11AGTpFfO','webmaster');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-02-08 15:51:51
