CREATE TABLE IF NOT EXISTS `person` (
  `id` bigint DEFAULT NULL,
  `firstname` varchar(80) DEFAULT NULL,
  `lastname` varchar(80) DEFAULT NULL,
  `gender` varchar(80) DEFAULT NULL,
  `address` varchar(180) DEFAULT NULL
);