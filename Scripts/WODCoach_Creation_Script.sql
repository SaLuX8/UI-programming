-- -----------------------------------------------------
-- Schema WODCoach
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `WODCoach` DEFAULT CHARACTER SET utf8 ;
USE `WODCoach` ;

-- -----------------------------------------------------
-- COACH
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WODCoach`.`Coach` (
  `idCoach` INT NOT NULL AUTO_INCREMENT,
  `fullName` VARCHAR(90) NOT NULL,
  `telNumber` VARCHAR(45) NULL,
  `created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  
  PRIMARY KEY (`idCoach`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- ATHLETE
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WODCoach`.`Athlete` (
  `idAthlete` INT NOT NULL AUTO_INCREMENT,
  `fullname` VARCHAR(90) NOT NULL,
  `telNumber` VARCHAR(45),
  `created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Coach_idCoach` INT NOT NULL,
  
  PRIMARY KEY (`idAthlete`),
  CONSTRAINT `fk_Coach`
    FOREIGN KEY (`Coach_idCoach`)
    REFERENCES `WODCoach`.`Coach` (`idCoach`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- WOD
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WODCoach`.`Wod` (
  `idWod` INT NOT NULL,
  `movementName` VARCHAR(60) NULL,
  `repsCount` INT(5) NULL,
  `roundCount` INT(4) NULL,
  `level` FLOAT NULL,
  `date` DATE NULL,
  `comment` varchar(200),
  `created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `idAthlete` INT,
  
  PRIMARY KEY (`idWod`),
  CONSTRAINT `fk_Athlete` FOREIGN KEY (`idAthlete`)
    REFERENCES Athlete (idAthlete)  
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Rating
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WODCoach`.`Rating` (
  `athlete_id` INT NOT NULL,
  `wod_id` INT NOT NULL,
  `rating` FLOAT,
  `comment` varchar(200),
  `created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  primary key (wod_id, athlete_id),
  constraint fk_athlete foreign key (athlete_id)
  references Athlete (idAthlete),
  constraint fk_wod foreign key (wod_id)
  references Wod (idWod)
)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
