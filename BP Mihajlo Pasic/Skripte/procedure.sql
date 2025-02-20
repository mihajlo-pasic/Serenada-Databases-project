USE serenada;

/*DELIMITER //
CREATE PROCEDURE VerifyUser(
    IN p_email VARCHAR(100),
    IN p_password VARCHAR(45),
    OUT p_authenticated INT
)
BEGIN
    DECLARE user_count INT;
    DECLARE stored_password VARCHAR(45);
    DECLARE izbrisan_tinyint INT;
	
    
    -- Provjeri postojanje korisnika
    SELECT COUNT(*) INTO user_count FROM korisnik WHERE Email = p_email;
	
    
    IF user_count > 0 THEN
		SELECT Izbrisan INTO izbrisan_tinyint FROM korisnik WHERE Email = p_email;
        IF izbrisan_tinyint = 1 THEN
			SET p_authenticated = 0;
		ELSE
			-- Dohvati pohranjenu lozinku
			SELECT Lozinka INTO stored_password FROM korisnik WHERE Email = p_email;
        
			-- Provjeri usklađenost lozinke
			IF stored_password = p_password THEN
				SET p_authenticated = 1;
			ELSE
				SET p_authenticated = 0;
			END IF;
		END IF;
    ELSE
        SET p_authenticated = 0;
    END IF;
    
    
END //

DELIMITER ;*/

DELIMITER //

CREATE PROCEDURE VerifyUser(
    IN p_email VARCHAR(100),
    IN p_password VARCHAR(45),
    OUT p_authenticated INT
)
BEGIN
    DECLARE user_count INT;
    DECLARE active_user_count INT;

    -- Provera postojanja korisnika
    SELECT COUNT(*) INTO user_count FROM korisnik WHERE Email = p_email;

    IF user_count > 0 THEN
        -- Provera koliko od korisnika sa istom email adresom ima aktivan nalog
        SELECT COUNT(*) INTO active_user_count FROM korisnik WHERE Email = p_email AND Izbrisan = 0;

        IF active_user_count > 0 THEN
            -- Provera poklapanja lozinke
            SELECT COUNT(*) INTO p_authenticated FROM korisnik WHERE Email = p_email AND Lozinka = p_password;
        ELSE
            SET p_authenticated = 0; -- Nijedan korisnik sa ovom email adresom nije aktivan
        END IF;
    ELSE
        SET p_authenticated = 0; -- Korisnik sa datom email adresom ne postoji
    END IF;

END //

DELIMITER ;


DELIMITER //
CREATE PROCEDURE RegistracijaKorisnika (
    IN p_ime VARCHAR(50),
    IN p_prezime VARCHAR(50),
    IN p_email VARCHAR(100),
    IN p_lozinka VARCHAR(45),
    IN p_potvrda_lozinke VARCHAR(45),
    IN p_telefon VARCHAR(45),
    OUT p_uspjeh BOOLEAN
)
BEGIN
    DECLARE postoji INT;
    DECLARE izbrisan_tinyint INT;
    
    -- Inicijaliziraj izlazni parametar na FALSE
    SET p_uspjeh = FALSE;
    
    -- Provjeri da li korisnik sa datim email-om postoji u bazi
    SELECT COUNT(*) INTO postoji FROM korisnik WHERE Email = p_email;
    
    IF postoji > 0 THEN
        -- Korisnik već postoji u bazi
        SELECT Izbrisan INTO izbrisan_tinyint FROM korisnik WHERE Email = p_email;
        IF izbrisan_tinyint = 1 THEN
			IF p_lozinka = p_potvrda_lozinke THEN
				-- Postoji ali je označen kao izbrisan, pa se može ponovo koristiti
				INSERT INTO korisnik (Ime, Prezime, Email, Lozinka, Telefon, DatumRegistracije, DatumBrisanja, Izbrisan) VALUES (p_ime, p_prezime, p_email, p_lozinka, p_telefon, NOW(), NULL, 0);
				-- Postavljanje izlaznog parametra na TRUE jer je registracija uspješna
				SET p_uspjeh = TRUE;
			ELSE
				SET p_uspjeh = FALSE;
			END IF;
        ELSE
			SET p_uspjeh = FALSE;
            -- Korisnik već postoji i nije označen kao izbrisan
        END IF;
    ELSE
        -- Kreiraj novog korisnika
        IF p_lozinka = p_potvrda_lozinke THEN
            INSERT INTO korisnik (Ime, Prezime, Email, Lozinka, Telefon, DatumRegistracije, DatumBrisanja, Izbrisan) VALUES (p_ime, p_prezime, p_email, p_lozinka, p_telefon, NOW(), NULL, 0);
            -- Postavljanje izlaznog parametra na TRUE jer je registracija uspješna
            SET p_uspjeh = TRUE;
        ELSE
			SET p_uspjeh = FALSE;
            -- Lozinke se ne podudaraju
        END IF;
    END IF;
END //

DELIMITER ;

/*DELIMITER //

CREATE PROCEDURE PronadjiIdKorisnika(
    IN p_email VARCHAR(255),
    OUT p_idKorisnika INT
)
BEGIN
    SELECT IdKorisnika INTO p_idKorisnika FROM Korisnik WHERE Email = p_email;
END //

DELIMITER ;*/

DELIMITER //

CREATE PROCEDURE PronadjiIdKorisnika(
    IN p_email VARCHAR(255),
    OUT p_idKorisnika INT
)
BEGIN
    DECLARE active_user_count INT;

    -- Broj aktivnih korisnika sa datom email adresom
    SELECT COUNT(*) INTO active_user_count 
    FROM Korisnik 
    WHERE Email = p_email AND Izbrisan = 0;

    IF active_user_count = 1 THEN
        -- Ako postoji samo jedan aktivan korisnik sa datom email adresom
        SELECT IdKorisnika INTO p_idKorisnika 
        FROM Korisnik 
        WHERE Email = p_email AND Izbrisan = 0;
    ELSE
        -- Ako postoji više od jednog aktvnog korisnika sa istom email adresom,
        -- uzimamo IdKorisnika prvog koji nije obrisan
        SELECT IdKorisnika INTO p_idKorisnika 
        FROM Korisnik 
        WHERE Email = p_email AND Izbrisan = 0 
        ORDER BY IdKorisnika LIMIT 1;
    END IF;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE PrikaziPlejlisteZaKorisnika(
    IN p_idKorisnika INT
)
BEGIN
    SELECT 
        PL.Naziv AS 'Naziv', 
        SEC_TO_TIME(SUM(TIME_TO_SEC(PJ.Trajanje))) AS 'Trajanje'
    FROM 
        PLEJLISTA PL
    JOIN 
        LISTA L ON PL.IdPLEJLISTA = L.PLEJLISTA_idPLEJLISTA
    JOIN 
        PJESMA PJ ON L.PJESMA_IdPjesme = PJ.IdPjesme
    WHERE 
        PL.KORISNIK_IdKorisnika = p_idKorisnika
    GROUP BY 
        PL.IdPLEJLISTA
    ORDER BY 
        PL.Naziv;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE PrikaziOmiljenePjesmeKorisnika(
    IN p_idKorisnika INT
)
BEGIN
    SELECT 
        PJ.Naziv AS 'Naziv',
        IFNULL(ALB.Naziv, PJ.Naziv) AS 'Album',
        IZV.Naziv AS 'Izvodjac',
        PJ.Trajanje AS 'Trajanje',
        SV.DatumDodavanja AS 'DatumDodavanja'
    FROM 
        SVIDJANJE SV
    JOIN 
        PJESMA PJ ON SV.PJESMA_IdPjesme = PJ.IdPjesme
    LEFT JOIN 
        ALBUM ALB ON PJ.ALBUM_IdAlbuma = ALB.IdAlbuma
    JOIN 
        PJESMA_IZVODJACA PI ON PJ.IdPjesme = PI.PJESMA_IdPjesme
    JOIN 
        IZVODJAC IZV ON PI.IZVODJAC_IdIzvodjaca = IZV.IdIzvodjaca
    WHERE 
        SV.KORISNIK_IdKorisnika = p_idKorisnika
    ORDER BY 
        SV.DatumDodavanja;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE PrikaziPratioceZaKorisnika(
    IN p_idKorisnika INT
)
BEGIN
    SELECT 
        IZV.Naziv AS 'Izvodjac',
        PR.DatumDodavanja AS 'DatumDodavanja'
    FROM 
        PRACENJE PR
    JOIN 
        IZVODJAC IZV ON PR.IZVODJAC_IdIzvodjaca = IZV.IdIzvodjaca
    WHERE 
        PR.KORISNIK_IdKorisnika = p_idKorisnika
    ORDER BY 
        PR.DatumDodavanja;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE DodajPjesmuUSvidjanja(
    IN p_idKorisnika INT,
    IN p_idPjesme INT
)
BEGIN
    INSERT INTO SVIDJANJE (KORISNIK_IdKorisnika, PJESMA_IdPjesme, DatumDodavanja)
    VALUES (p_idKorisnika, p_idPjesme, NOW());
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE ZapratiIzvodjaca(
    IN p_idKorisnika INT,
    IN p_idIzvodjaca INT
)
BEGIN
    INSERT INTO PRACENJE (KORISNIK_IdKorisnika, IZVODJAC_IdIzvodjaca, DatumDodavanja)
    VALUES (p_idKorisnika, p_idIzvodjaca, NOW());
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetIdAlbumaByNaziv(
    IN p_nazivAlbuma VARCHAR(200),
    OUT p_idAlbuma INT
)
BEGIN
    SELECT IdAlbuma INTO p_idAlbuma
    FROM ALBUM
    WHERE Naziv = p_nazivAlbuma;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetPjesmeByAlbumId(
    IN p_idAlbuma INT
)
BEGIN
    SELECT Naziv, Trajanje, DatumIzdanja, BrojSvidjanja, TekstPjesme
    FROM PJESMA
    WHERE ALBUM_IdAlbuma = p_idAlbuma
    ORDER BY Naziv;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetIdPlejlisteByNaziv(
    IN p_nazivPlejliste VARCHAR(200),
    OUT p_idPlejliste INT
)
BEGIN
    SELECT IdPLEJLISTA INTO p_idPlejliste
    FROM PLEJLISTA
    WHERE Naziv = p_nazivPlejliste;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetPjesmeByPlejlistaId(
    IN p_idPlejliste INT
)
BEGIN
    SELECT Naziv, Trajanje, DatumIzdanja, BrojSvidjanja, TekstPjesme
    FROM PJESMA
    INNER JOIN LISTA ON PJESMA.IdPjesme = LISTA.PJESMA_IdPjesme
    WHERE LISTA.PLEJLISTA_idPLEJLISTA = p_idPlejliste
    ORDER BY Naziv;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE RemovePjesmaFromSvidjanje(
    IN p_idKorisnika INT,
    IN p_nazivPjesme VARCHAR(200)
)
BEGIN
    DECLARE p_idPjesme INT;

    -- Dohvati IdPjesme na osnovu naziva pjesme
    SELECT IdPjesme INTO p_idPjesme
    FROM PJESMA
    WHERE Naziv = p_nazivPjesme;

    -- Ukloni zapis iz tabele SVIDJANJE
    DELETE FROM SVIDJANJE
    WHERE KORISNIK_IdKorisnika = p_idKorisnika AND PJESMA_IdPjesme = p_idPjesme;
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE RemoveIzvodjacFromPracenje(
    IN p_idKorisnika INT,
    IN p_nazivIzvodjaca VARCHAR(100)
)
BEGIN
    DECLARE p_idIzvodjaca INT;

    -- Dohvati IdIzvodjaca na osnovu naziva izvođača
    SELECT IdIzvodjaca INTO p_idIzvodjaca
    FROM IZVODJAC
    WHERE Naziv = p_nazivIzvodjaca;

    -- Ukloni zapis iz tabele PRACENJE
    DELETE FROM PRACENJE
    WHERE KORISNIK_IdKorisnika = p_idKorisnika AND IZVODJAC_IdIzvodjaca = p_idIzvodjaca;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE DeletePlaylist(
    IN p_idKorisnika INT,
    IN p_nazivPlejliste VARCHAR(200)
)
BEGIN
    DECLARE p_idPlejliste INT;

    -- Dohvati IdPlejliste na osnovu naziva plejliste
    SELECT IdPLEJLISTA INTO p_idPlejliste
    FROM PLEJLISTA
    WHERE KORISNIK_IdKorisnika = p_idKorisnika AND Naziv = p_nazivPlejliste;

	-- Izbrisi zapise iz tabele LISTA koji sadrze IdPlejliste
    DELETE FROM LISTA
    WHERE PLEJLISTA_idPLEJLISTA = p_idPlejliste;
    
    -- Izbrisi plejlistu iz tabele PLEJLISTA
    DELETE FROM PLEJLISTA
    WHERE IdPLEJLISTA = p_idPlejliste;
END //

DELIMITER ;



DELIMITER //

DELIMITER //

CREATE PROCEDURE DeleteUser(IN userId INT)
BEGIN
    -- Datum brisanja korisnika
    UPDATE KORISNIK SET DatumBrisanja = CURRENT_DATE(), Izbrisan = 1 WHERE IdKorisnika = userId;
	
    DELETE FROM LISTA WHERE PLEJLISTA_idPLEJLISTA IN (SELECT IdPLEJLISTA FROM PLEJLISTA WHERE KORISNIK_IdKorisnika = userId);
    
    -- Brisanje plejlista korisnika
    DELETE FROM PLEJLISTA WHERE KORISNIK_IdKorisnika = userId;

    -- Brisanje lista sviđanja korisnika
    DELETE FROM SVIDJANJE WHERE KORISNIK_IdKorisnika = userId;

    -- Brisanje lista praćenja korisnika
    DELETE FROM PRACENJE WHERE KORISNIK_IdKorisnika = userId;
END //

DELIMITER ;











