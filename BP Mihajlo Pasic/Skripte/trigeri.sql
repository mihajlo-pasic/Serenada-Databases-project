USE serenada;

DELIMITER //

CREATE TRIGGER UpdateBrojSvidjanja
AFTER INSERT ON SVIDJANJE
FOR EACH ROW
BEGIN
    UPDATE PJESMA
    SET BrojSvidjanja = BrojSvidjanja + 1
    WHERE IdPjesme = NEW.PJESMA_IdPjesme;
END //

DELIMITER ;

DROP TRIGGER IF EXISTS UpdateBrojPracenja;


DELIMITER //

CREATE TRIGGER UpdateBrojPracenja
AFTER INSERT ON PRACENJE
FOR EACH ROW
BEGIN
    UPDATE IZVODJAC
    SET BrojPratilaca = BrojPratilaca + 1
    WHERE IdIzvodjaca = NEW.IZVODJAC_IdIzvodjaca;
END //

DELIMITER ;

DELIMITER //

CREATE TRIGGER UmanjiBrojSvidjanja
AFTER DELETE ON SVIDJANJE
FOR EACH ROW
BEGIN
    DECLARE p_idPjesme INT;
    DECLARE p_brojSvidjanja INT;

    -- Dohvati IdPjesme iz obrisanog zapisa
    SET p_idPjesme = OLD.PJESMA_IdPjesme;

    -- Dohvati trenutni BrojSviđanja za pjesmu
    SELECT BrojSvidjanja INTO p_brojSvidjanja
    FROM PJESMA
    WHERE IdPjesme = p_idPjesme;

    -- Ažuriraj BrojSviđanja za pjesmu
    UPDATE PJESMA
    SET BrojSvidjanja = p_brojSvidjanja - 1
    WHERE IdPjesme = p_idPjesme;
END //

DELIMITER ;

DELIMITER //

CREATE TRIGGER UmanjiBrojPratilaca
AFTER DELETE ON PRACENJE
FOR EACH ROW
BEGIN
    DECLARE p_idIzvodjaca INT;
    DECLARE p_brojPratilaca INT;

    -- Dohvati IdIzvodjaca iz obrisanog zapisa
    SET p_idIzvodjaca = OLD.IZVODJAC_IdIzvodjaca;

    -- Dohvati trenutni BrojPratilaca za izvođača
    SELECT BrojPratilaca INTO p_brojPratilaca
    FROM IZVODJAC
    WHERE IdIzvodjaca = p_idIzvodjaca;

    -- Ažuriraj BrojPratilaca za izvođača
    UPDATE IZVODJAC
    SET BrojPratilaca = p_brojPratilaca - 1
    WHERE IdIzvodjaca = p_idIzvodjaca;
END //

DELIMITER ;

DELIMITER //

CREATE TRIGGER DeleteListaRecords
AFTER DELETE ON PLEJLISTA
FOR EACH ROW
BEGIN
    -- Obriši zapise iz tabele LISTA koji pripadaju obrisanoj plejlisti
    DELETE FROM LISTA
    WHERE PLEJLISTA_idPLEJLISTA = OLD.IdPLEJLISTA;
END //

DELIMITER ;

DELIMITER //

CREATE TRIGGER DecreaseLikesAndFollowers AFTER DELETE ON KORISNIK
FOR EACH ROW
BEGIN
    -- Umanjivanje BrojSvidjanja za svaku pesmu u listi sviđanja korisnika koji se briše
    UPDATE PJESMA
    SET BrojSvidjanja = BrojSvidjanja - 1
    WHERE IdPjesme IN (SELECT PJESMA_IdPjesme FROM SVIDJANJE WHERE KORISNIK_IdKorisnika = OLD.IdKorisnika);

    -- Umanjivanje BrojPratilaca za svakog izvođača u listi praćenja korisnika koji se briše
    UPDATE IZVODJAC
    SET BrojPratilaca = BrojPratilaca - 1
    WHERE IdIzvodjaca IN (SELECT IZVODJAC_IdIzvodjaca FROM PRACENJE WHERE KORISNIK_IdKorisnika = OLD.IdKorisnika);
END //

DELIMITER ;


