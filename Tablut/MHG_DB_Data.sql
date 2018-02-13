-- Creator: Herzig Melvyn.
-- Version: 13.02.2018.
-- Environment: Created for MySQL.
-- Script made for: Create the database for the tablut's game.

-- --------------------------------------
-- Drop the schema if he already exists.
-- Create the schema.
-- --------------------------------------
DROP SCHEMA IF EXISTS Tablut;
CREATE SCHEMA Tablut;
USE Tablut;

-- ---------------------------
-- Create the Profile table.
-- ---------------------------
DROP TABLE IF EXISTS `Profile`;
CREATE TABLE IF NOT EXISTS `Profile` (
    `idProfile` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(20) NOT NULL,
    `Won_Attack` SMALLINT UNSIGNED NOT NULL DEFAULT 0,
    `Lost_Attack` SMALLINT UNSIGNED NOT NULL DEFAULT 0,
    `Won_Defence` SMALLINT UNSIGNED NOT NULL DEFAULT 0,
    `Lost_Defence` SMALLINT UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`idProfile`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;