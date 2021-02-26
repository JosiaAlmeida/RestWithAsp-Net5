ALTER TABLE `person` 
  ADD COLUMN `enable` BIT(1) NOT NULL DEFAULT b'1' AFTER `gender`;
