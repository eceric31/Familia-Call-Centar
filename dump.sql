-- MySQL dump 10.15  Distrib 10.0.22-MariaDB, for Linux (x86_64)
--
-- Host: localhost    Database: Isporuke
-- ------------------------------------------------------
-- Server version	10.0.22-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Checkpoint`
--

DROP TABLE IF EXISTS `Checkpoint`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Checkpoint` (
  `longitude` double NOT NULL,
  `latitude` double NOT NULL,
  `vrijeme_isporuke` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `voznjaID` int(11) NOT NULL,
  KEY `voznjaID` (`voznjaID`),
  CONSTRAINT `Checkpoint_ibfk_1` FOREIGN KEY (`voznjaID`) REFERENCES `Voznja` (`voznjaID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Checkpoint`
--

LOCK TABLES `Checkpoint` WRITE;
/*!40000 ALTER TABLE `Checkpoint` DISABLE KEYS */;
/*!40000 ALTER TABLE `Checkpoint` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Jelo`
--

DROP TABLE IF EXISTS `Jelo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Jelo` (
  `jeloID` int(11) NOT NULL AUTO_INCREMENT,
  `naziv` varchar(100) NOT NULL,
  `opis` varchar(100) DEFAULT NULL,
  `tip_jela` varchar(100) DEFAULT NULL,
  `cijena` double DEFAULT NULL,
  PRIMARY KEY (`jeloID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Jelo`
--

LOCK TABLES `Jelo` WRITE;
/*!40000 ALTER TABLE `Jelo` DISABLE KEYS */;
/*!40000 ALTER TABLE `Jelo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Lokacija`
--

DROP TABLE IF EXISTS `Lokacija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Lokacija` (
  `longitude` double NOT NULL,
  `latitude` double NOT NULL,
  `vrijeme_polaska` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `voznjaID` int(11) NOT NULL,
  KEY `voznjaID` (`voznjaID`),
  CONSTRAINT `Lokacija_ibfk_1` FOREIGN KEY (`voznjaID`) REFERENCES `Voznja` (`voznjaID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Lokacija`
--

LOCK TABLES `Lokacija` WRITE;
/*!40000 ALTER TABLE `Lokacija` DISABLE KEYS */;
/*!40000 ALTER TABLE `Lokacija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Narudzba`
--

DROP TABLE IF EXISTS `Narudzba`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Narudzba` (
  `narudzbaID` int(11) NOT NULL AUTO_INCREMENT,
  `ime_narucioca` varchar(50) NOT NULL,
  `prezime_narucioca` varchar(50) NOT NULL,
  `broj_telefona_narucioca` varchar(50) NOT NULL,
  `ime_firme` varchar(50) NOT NULL,
  `adresa_firme` varchar(50) NOT NULL,
  `ocekivano_vrijeme_isporuke` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `vrijeme_isporuke` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `voznjaID` int(11) NOT NULL,
  PRIMARY KEY (`narudzbaID`),
  KEY `voznjaID` (`voznjaID`),
  CONSTRAINT `Narudzba_ibfk_1` FOREIGN KEY (`voznjaID`) REFERENCES `Voznja` (`voznjaID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Narudzba`
--

LOCK TABLES `Narudzba` WRITE;
/*!40000 ALTER TABLE `Narudzba` DISABLE KEYS */;
/*!40000 ALTER TABLE `Narudzba` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Narudzba_item`
--

DROP TABLE IF EXISTS `Narudzba_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Narudzba_item` (
  `narudzba_itemID` int(11) NOT NULL AUTO_INCREMENT,
  `kvantitet` int(11) NOT NULL,
  `ukupna_cijena` double DEFAULT NULL,
  `jeloID` int(11) NOT NULL,
  `narudzbaID` int(11) NOT NULL,
  PRIMARY KEY (`narudzba_itemID`),
  KEY `jeloID` (`jeloID`),
  KEY `narudzbaID` (`narudzbaID`),
  CONSTRAINT `Narudzba_item_ibfk_1` FOREIGN KEY (`jeloID`) REFERENCES `Jelo` (`jeloID`),
  CONSTRAINT `Narudzba_item_ibfk_2` FOREIGN KEY (`narudzbaID`) REFERENCES `Narudzba` (`narudzbaID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Narudzba_item`
--

LOCK TABLES `Narudzba_item` WRITE;
/*!40000 ALTER TABLE `Narudzba_item` DISABLE KEYS */;
/*!40000 ALTER TABLE `Narudzba_item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Vozac`
--

DROP TABLE IF EXISTS `Vozac`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Vozac` (
  `vozacID` int(11) NOT NULL AUTO_INCREMENT,
  `ime` varchar(100) NOT NULL,
  `prezime` varchar(100) NOT NULL,
  `passsword` varchar(100) NOT NULL,
  PRIMARY KEY (`vozacID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Vozac`
--

LOCK TABLES `Vozac` WRITE;
/*!40000 ALTER TABLE `Vozac` DISABLE KEYS */;
/*!40000 ALTER TABLE `Vozac` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Vozilo`
--

DROP TABLE IF EXISTS `Vozilo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Vozilo` (
  `voziloID` int(11) NOT NULL AUTO_INCREMENT,
  `tip_vozila` varchar(100) NOT NULL,
  `nosivost` int(11) NOT NULL,
  PRIMARY KEY (`voziloID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Vozilo`
--

LOCK TABLES `Vozilo` WRITE;
/*!40000 ALTER TABLE `Vozilo` DISABLE KEYS */;
/*!40000 ALTER TABLE `Vozilo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Voznja`
--

DROP TABLE IF EXISTS `Voznja`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Voznja` (
  `voznjaID` int(11) NOT NULL AUTO_INCREMENT,
  `voziloID` int(11) NOT NULL,
  `vozacID` int(11) NOT NULL,
  PRIMARY KEY (`voznjaID`),
  KEY `voziloID` (`voziloID`),
  KEY `vozacID` (`vozacID`),
  CONSTRAINT `Voznja_ibfk_1` FOREIGN KEY (`voziloID`) REFERENCES `Vozilo` (`voziloID`),
  CONSTRAINT `Voznja_ibfk_2` FOREIGN KEY (`vozacID`) REFERENCES `Vozac` (`VozacID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Voznja`
--

LOCK TABLES `Voznja` WRITE;
/*!40000 ALTER TABLE `Voznja` DISABLE KEYS */;
/*!40000 ALTER TABLE `Voznja` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-22 16:14:56
