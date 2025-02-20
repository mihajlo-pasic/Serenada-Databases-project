-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema SERENADA
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema SERENADA
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `SERENADA` DEFAULT CHARACTER SET utf8 ;
USE `SERENADA` ;

-- -----------------------------------------------------
-- Table `SERENADA`.`KORISNIK`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`KORISNIK` (
  `IdKorisnika` INT NOT NULL AUTO_INCREMENT,
  `Ime` VARCHAR(50) NOT NULL,
  `Prezime` VARCHAR(50) NOT NULL,
  `Email` VARCHAR(100) NOT NULL,
  `Lozinka` VARCHAR(45) NOT NULL,
  `Telefon` VARCHAR(45) NOT NULL,
  `DatumRegistracije` DATE NOT NULL,
  `DatumBrisanja` DATE NULL,
  `Izbrisan` TINYINT NOT NULL,
  PRIMARY KEY (`IdKorisnika`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`IZVODJAC`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`IZVODJAC` (
  `IdIzvodjaca` INT NOT NULL AUTO_INCREMENT,
  `Naziv` VARCHAR(100) NOT NULL,
  `Zemlja porijekla` VARCHAR(100) NOT NULL,
  `GodinaFormiranja` INT NOT NULL,
  `BrojPratilaca` INT NOT NULL,
  `KratakOpis` MEDIUMTEXT NULL,
  PRIMARY KEY (`IdIzvodjaca`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`ALBUM`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`ALBUM` (
  `IdAlbuma` INT NOT NULL AUTO_INCREMENT,
  `Naziv` VARCHAR(200) NOT NULL,
  `DatumIzdavanja` DATE NOT NULL,
  `IZVODJAC_IdIzvodjaca` INT NOT NULL,
  PRIMARY KEY (`IdAlbuma`),
  INDEX `fk_ALBUM_IZVODJAC1_idx` (`IZVODJAC_IdIzvodjaca` ASC) VISIBLE,
  CONSTRAINT `fk_ALBUM_IZVODJAC1`
    FOREIGN KEY (`IZVODJAC_IdIzvodjaca`)
    REFERENCES `SERENADA`.`IZVODJAC` (`IdIzvodjaca`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`PJESMA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`PJESMA` (
  `IdPjesme` INT NOT NULL AUTO_INCREMENT,
  `Naziv` VARCHAR(200) NOT NULL,
  `Trajanje` TIME NOT NULL,
  `DatumIzdanja` DATE NOT NULL,
  `BrojSvidjanja` INT NOT NULL,
  `TekstPjesme` MEDIUMTEXT NULL,
  `ALBUM_IdAlbuma` INT NULL,
  PRIMARY KEY (`IdPjesme`),
  INDEX `fk_PJESMA_ALBUM1_idx` (`ALBUM_IdAlbuma` ASC) VISIBLE,
  CONSTRAINT `fk_PJESMA_ALBUM1`
    FOREIGN KEY (`ALBUM_IdAlbuma`)
    REFERENCES `SERENADA`.`ALBUM` (`IdAlbuma`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`PLEJLISTA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`PLEJLISTA` (
  `IdPLEJLISTA` INT NOT NULL AUTO_INCREMENT,
  `Naziv` VARCHAR(200) NOT NULL,
  `KORISNIK_IdKorisnika` INT NOT NULL,
  PRIMARY KEY (`IdPLEJLISTA`),
  INDEX `fk_PLEJLISTA_KORISNIK1_idx` (`KORISNIK_IdKorisnika` ASC) VISIBLE,
  CONSTRAINT `fk_PLEJLISTA_KORISNIK1`
    FOREIGN KEY (`KORISNIK_IdKorisnika`)
    REFERENCES `SERENADA`.`KORISNIK` (`IdKorisnika`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`ZANR`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`ZANR` (
  `IdZanra` INT NOT NULL AUTO_INCREMENT,
  `Naziv` VARCHAR(200) NOT NULL,
  PRIMARY KEY (`IdZanra`),
  UNIQUE INDEX `Naziv_UNIQUE` (`Naziv` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`SVIDJANJE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`SVIDJANJE` (
  `KORISNIK_IdKorisnika` INT NOT NULL,
  `PJESMA_IdPjesme` INT NOT NULL,
  `DatumDodavanja` DATE NOT NULL,
  PRIMARY KEY (`KORISNIK_IdKorisnika`, `PJESMA_IdPjesme`),
  INDEX `fk_KORISNIK_has_PJESMA_PJESMA1_idx` (`PJESMA_IdPjesme` ASC) VISIBLE,
  INDEX `fk_KORISNIK_has_PJESMA_KORISNIK_idx` (`KORISNIK_IdKorisnika` ASC) VISIBLE,
  CONSTRAINT `fk_KORISNIK_has_PJESMA_KORISNIK`
    FOREIGN KEY (`KORISNIK_IdKorisnika`)
    REFERENCES `SERENADA`.`KORISNIK` (`IdKorisnika`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KORISNIK_has_PJESMA_PJESMA1`
    FOREIGN KEY (`PJESMA_IdPjesme`)
    REFERENCES `SERENADA`.`PJESMA` (`IdPjesme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`PRACENJE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`PRACENJE` (
  `KORISNIK_IdKorisnika` INT NOT NULL,
  `IZVODJAC_IdIzvodjaca` INT NOT NULL,
  `DatumDodavanja` DATE NOT NULL,
  PRIMARY KEY (`KORISNIK_IdKorisnika`, `IZVODJAC_IdIzvodjaca`),
  INDEX `fk_KORISNIK_has_IZVODJAC_IZVODJAC1_idx` (`IZVODJAC_IdIzvodjaca` ASC) VISIBLE,
  INDEX `fk_KORISNIK_has_IZVODJAC_KORISNIK1_idx` (`KORISNIK_IdKorisnika` ASC) VISIBLE,
  CONSTRAINT `fk_KORISNIK_has_IZVODJAC_KORISNIK1`
    FOREIGN KEY (`KORISNIK_IdKorisnika`)
    REFERENCES `SERENADA`.`KORISNIK` (`IdKorisnika`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KORISNIK_has_IZVODJAC_IZVODJAC1`
    FOREIGN KEY (`IZVODJAC_IdIzvodjaca`)
    REFERENCES `SERENADA`.`IZVODJAC` (`IdIzvodjaca`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`PJESMA_IZVODJACA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`PJESMA_IZVODJACA` (
  `IZVODJAC_IdIzvodjaca` INT NOT NULL,
  `PJESMA_IdPjesme` INT NOT NULL,
  PRIMARY KEY (`IZVODJAC_IdIzvodjaca`, `PJESMA_IdPjesme`),
  INDEX `fk_IZVODJAC_has_PJESMA_PJESMA1_idx` (`PJESMA_IdPjesme` ASC) VISIBLE,
  INDEX `fk_IZVODJAC_has_PJESMA_IZVODJAC1_idx` (`IZVODJAC_IdIzvodjaca` ASC) VISIBLE,
  CONSTRAINT `fk_IZVODJAC_has_PJESMA_IZVODJAC1`
    FOREIGN KEY (`IZVODJAC_IdIzvodjaca`)
    REFERENCES `SERENADA`.`IZVODJAC` (`IdIzvodjaca`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_IZVODJAC_has_PJESMA_PJESMA1`
    FOREIGN KEY (`PJESMA_IdPjesme`)
    REFERENCES `SERENADA`.`PJESMA` (`IdPjesme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`LISTA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`LISTA` (
  `PLEJLISTA_idPLEJLISTA` INT NOT NULL,
  `PJESMA_IdPjesme` INT NOT NULL,
  `DatumDodavanja` DATE NOT NULL,
  PRIMARY KEY (`PLEJLISTA_idPLEJLISTA`, `PJESMA_IdPjesme`),
  INDEX `fk_PLEJLISTA_has_PJESMA_PJESMA1_idx` (`PJESMA_IdPjesme` ASC) VISIBLE,
  INDEX `fk_PLEJLISTA_has_PJESMA_PLEJLISTA1_idx` (`PLEJLISTA_idPLEJLISTA` ASC) VISIBLE,
  CONSTRAINT `fk_PLEJLISTA_has_PJESMA_PLEJLISTA1`
    FOREIGN KEY (`PLEJLISTA_idPLEJLISTA`)
    REFERENCES `SERENADA`.`PLEJLISTA` (`IdPLEJLISTA`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PLEJLISTA_has_PJESMA_PJESMA1`
    FOREIGN KEY (`PJESMA_IdPjesme`)
    REFERENCES `SERENADA`.`PJESMA` (`IdPjesme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`ZANR_PJESME`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`ZANR_PJESME` (
  `PJESMA_IdPjesme` INT NOT NULL,
  `ZANR_IdZanra` INT NOT NULL,
  PRIMARY KEY (`PJESMA_IdPjesme`, `ZANR_IdZanra`),
  INDEX `fk_PJESMA_has_ZANR_ZANR1_idx` (`ZANR_IdZanra` ASC) VISIBLE,
  INDEX `fk_PJESMA_has_ZANR_PJESMA1_idx` (`PJESMA_IdPjesme` ASC) VISIBLE,
  CONSTRAINT `fk_PJESMA_has_ZANR_PJESMA1`
    FOREIGN KEY (`PJESMA_IdPjesme`)
    REFERENCES `SERENADA`.`PJESMA` (`IdPjesme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PJESMA_has_ZANR_ZANR1`
    FOREIGN KEY (`ZANR_IdZanra`)
    REFERENCES `SERENADA`.`ZANR` (`IdZanra`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`ZANR_ALBUMA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`ZANR_ALBUMA` (
  `ALBUM_IdAlbuma` INT NOT NULL,
  `ZANR_IdZanra` INT NOT NULL,
  PRIMARY KEY (`ALBUM_IdAlbuma`, `ZANR_IdZanra`),
  INDEX `fk_ALBUM_has_ZANR_ZANR1_idx` (`ZANR_IdZanra` ASC) VISIBLE,
  INDEX `fk_ALBUM_has_ZANR_ALBUM1_idx` (`ALBUM_IdAlbuma` ASC) VISIBLE,
  CONSTRAINT `fk_ALBUM_has_ZANR_ALBUM1`
    FOREIGN KEY (`ALBUM_IdAlbuma`)
    REFERENCES `SERENADA`.`ALBUM` (`IdAlbuma`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ALBUM_has_ZANR_ZANR1`
    FOREIGN KEY (`ZANR_IdZanra`)
    REFERENCES `SERENADA`.`ZANR` (`IdZanra`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`PLAN`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`PLAN` (
  `IdPlana` INT NOT NULL AUTO_INCREMENT,
  `Naziv` VARCHAR(100) NOT NULL,
  `Opis` MEDIUMTEXT NOT NULL,
  `Cijena` DECIMAL NOT NULL,
  PRIMARY KEY (`IdPlana`),
  UNIQUE INDEX `Naziv_UNIQUE` (`Naziv` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`KARTICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`KARTICA` (
  `IdKartice` INT NOT NULL AUTO_INCREMENT,
  `Tip` VARCHAR(100) NOT NULL,
  `Broj` INT NOT NULL,
  `DatumIsteka` DATE NOT NULL,
  `ImeVlasnika` VARCHAR(100) NOT NULL,
  `AdresaNaplate` VARCHAR(300) NOT NULL,
  `CVC` INT NOT NULL,
  PRIMARY KEY (`IdKartice`),
  UNIQUE INDEX `Broj_UNIQUE` (`Broj` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`PRETPLATA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`PRETPLATA` (
  `IdPretplate` INT NOT NULL AUTO_INCREMENT,
  `PLAN_IdPlana` INT NOT NULL,
  `KORISNIK_IdKorisnika` INT NOT NULL,
  `Datum` DATE NOT NULL,
  `Aktivna` TINYINT NOT NULL,
  PRIMARY KEY (`IdPretplate`),
  INDEX `fk_PRETPLATA_PLAN1_idx` (`PLAN_IdPlana` ASC) VISIBLE,
  INDEX `fk_PRETPLATA_KORISNIK1_idx` (`KORISNIK_IdKorisnika` ASC) VISIBLE,
  CONSTRAINT `fk_PRETPLATA_PLAN1`
    FOREIGN KEY (`PLAN_IdPlana`)
    REFERENCES `SERENADA`.`PLAN` (`IdPlana`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PRETPLATA_KORISNIK1`
    FOREIGN KEY (`KORISNIK_IdKorisnika`)
    REFERENCES `SERENADA`.`KORISNIK` (`IdKorisnika`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERENADA`.`UPLATA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SERENADA`.`UPLATA` (
  `IdUplate` INT NOT NULL AUTO_INCREMENT,
  `KARTICA_IdKartice` INT NOT NULL,
  `DatumUplateOD` DATE NOT NULL,
  `DatumUplateDO` DATE NOT NULL,
  `Iznos` DECIMAL NOT NULL,
  `Status` TINYINT NOT NULL,
  `PRETPLATA_IdPretplate` INT NOT NULL,
  PRIMARY KEY (`IdUplate`),
  INDEX `fk_UPLATA_KARTICA1_idx` (`KARTICA_IdKartice` ASC) VISIBLE,
  INDEX `fk_UPLATA_PRETPLATA1_idx` (`PRETPLATA_IdPretplate` ASC) VISIBLE,
  CONSTRAINT `fk_UPLATA_KARTICA1`
    FOREIGN KEY (`KARTICA_IdKartice`)
    REFERENCES `SERENADA`.`KARTICA` (`IdKartice`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_UPLATA_PRETPLATA1`
    FOREIGN KEY (`PRETPLATA_IdPretplate`)
    REFERENCES `SERENADA`.`PRETPLATA` (`IdPretplate`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
