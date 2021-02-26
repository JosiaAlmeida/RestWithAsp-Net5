-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           8.0.21 - MySQL Community Server - GPL
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              11.1.0.6116
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Copiando estrutura do banco de dados para rest_with_asp_net_udemay
CREATE DATABASE IF NOT EXISTS `rest_with_asp_net_udemay` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `rest_with_asp_net_udemay`;

-- Copiando estrutura para tabela rest_with_asp_net_udemay.book
CREATE TABLE IF NOT EXISTS `book` (
  `id` bigint DEFAULT NULL,
  `title` varchar(80) DEFAULT NULL,
  `descricao` varchar(100) DEFAULT NULL,
  `autor` varchar(80) DEFAULT NULL,
  `enable` bit(1) NOT NULL DEFAULT b'1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Copiando dados para a tabela rest_with_asp_net_udemay.book: ~3 rows (aproximadamente)
/*!40000 ALTER TABLE `book` DISABLE KEYS */;
INSERT INTO `book` (`id`, `title`, `descricao`, `autor`, `enable`) VALUES
	(1, 'Pai pobre pai rico', 'Pai pobre e pai rico kkkk', 'Almeida', b'1'),
	(2, NULL, NULL, NULL, b'1'),
	(3, 'Pai pobre pai rico', 'Pai pobre e pai rico kkkk', 'Josia', b'1');
/*!40000 ALTER TABLE `book` ENABLE KEYS */;

-- Copiando estrutura para tabela rest_with_asp_net_udemay.changelog
CREATE TABLE IF NOT EXISTS `changelog` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` tinyint DEFAULT NULL,
  `version` varchar(50) DEFAULT NULL,
  `description` varchar(200) NOT NULL,
  `name` varchar(300) NOT NULL,
  `checksum` varchar(32) DEFAULT NULL,
  `installed_by` varchar(100) NOT NULL,
  `installed_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `success` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Copiando dados para a tabela rest_with_asp_net_udemay.changelog: ~9 rows (aproximadamente)
/*!40000 ALTER TABLE `changelog` DISABLE KEYS */;
INSERT INTO `changelog` (`id`, `type`, `version`, `description`, `name`, `checksum`, `installed_by`, `installed_on`, `success`) VALUES
	(1, 2, '0', 'Empty schema found: rest_with_asp_net_udemay.', 'rest_with_asp_net_udemay', '', 'root', '2021-02-06 22:58:45', 1),
	(2, 0, '1', 'Create Table Person (2503 ms)', 'V1__Create_Table_Person.sql', '6DC03E647AA8E215948FB203F50F453C', 'root', '2021-02-06 22:58:54', 1),
	(3, 0, '2', 'Populate Table Person (145 ms)', 'V2__Populate_Table_Person.sql', 'F8C3B92E3C6937181FA98EA60C795C65', 'root', '2021-02-06 22:58:54', 1),
	(4, 0, '3', 'Create Table Books (3192 ms)', 'V3__Create_Table_Books.sql', '8EA416453FEA043FE95E16F0F859C232', 'root', '2021-02-06 22:58:58', 1),
	(5, 0, '4', 'Populate Table Books (268 ms)', 'V4__Populate_Table_Books.sql', 'D3FE89DE13E96CE48E30AEEDD40A4C7D', 'root', '2021-02-06 22:58:59', 1),
	(6, 0, '5', 'Create Table Users (9792 ms)', 'V5__Create_Table_Users.sql', 'E6356C056B401DFB6162C2F0394D5816', 'root', '2021-02-16 18:56:35', 1),
	(7, 0, '6', 'Insert Data In Users (74 ms)', 'V6__Insert_Data_In_Users.sql', 'DBBCB48EAC05AA2B95AB3B50A5CD3455', 'root', '2021-02-16 18:56:36', 1),
	(8, 0, '7', 'Alter Table Person (8748 ms)', 'V7__Alter_Table_Person.sql', 'BA62B845275E3DFE5DB1C80A9AD20357', 'root', '2021-02-22 14:05:21', 1),
	(9, 0, '8', 'Alter Table Book (16382 ms)', 'V8__Alter_Table_Book.sql', '35EC384B5F6071E43BDBFEA698F14070', 'root', '2021-02-22 19:04:54', 1),
	(10, 0, '9', 'Populate Person With Many (2102 ms)', 'V9__Populate_Person_With_Many.sql', 'AF1C5D71FDF61A673C03E5461A80F5B5', 'root', '2021-02-23 19:03:42', 0),
	(11, 0, '9', 'Populate Person With Many (735 ms)', 'V9__Populate_Person_With_Many.sql', 'AF1C5D71FDF61A673C03E5461A80F5B5', 'root', '2021-02-23 19:09:59', 0),
	(12, 0, '9', 'Populate Person With Many (5043 ms)', 'V9__Populate_Person_With_Many.sql', '92CAB756FD836788643E3E0E92A15E4E', 'root', '2021-02-23 19:16:34', 1);
/*!40000 ALTER TABLE `changelog` ENABLE KEYS */;

-- Copiando estrutura para tabela rest_with_asp_net_udemay.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_name` varchar(50) NOT NULL DEFAULT '0',
  `password` varchar(130) NOT NULL DEFAULT '0',
  `full_name` varchar(120) NOT NULL,
  `refresh_token` varchar(500) DEFAULT '0',
  `refresh_token_expiry_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `user_name` (`user_name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Copiando dados para a tabela rest_with_asp_net_udemay.users: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`id`, `user_name`, `password`, `full_name`, `refresh_token`, `refresh_token_expiry_time`) VALUES
	(1, 'leandro', '24-0B-E5-18-FA-BD-27-24-DD-B6-F0-4E-EB-1D-A5-96-74-48-D7-E8-31-C0-8C-8F-A8-22-80-9F-74-C7-20-A9', 'LEANDRO DA COSTA GONCALVES', 'WihWZLbjviHTFL371YF3B7i7R+9HbSrClTNqes0+tKQ=', '2021-03-03 15:53:39'),
	(2, 'Josia', '', 'Josia Almeida', '0', NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
