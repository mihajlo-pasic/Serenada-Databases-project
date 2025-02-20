USE serenada;

CREATE VIEW PjesmeView AS
SELECT
    P.Naziv AS 'Naziv',
    I.Naziv AS 'Izvodjac',
    COALESCE(A.Naziv, P.Naziv) AS 'Album',
    P.DatumIzdanja AS 'DatumIzdanja',
    P.Trajanje AS 'Trajanje',
    P.BrojSvidjanja AS 'BrojSvidjanja',
    P.TekstPjesme AS 'TekstPjesme'
FROM
    PJESMA P
JOIN
    PJESMA_IZVODJACA PI ON P.IdPjesme = PI.PJESMA_IdPjesme
JOIN
    IZVODJAC I ON PI.IZVODJAC_IdIzvodjaca = I.IdIzvodjaca
LEFT JOIN
    ALBUM A ON P.ALBUM_IdAlbuma = A.IdAlbuma
ORDER BY
    P.Naziv;

CREATE VIEW AlbumView AS
SELECT
    ALB.Naziv AS 'Naziv',
    IZV.Naziv AS 'Izvođač',
    ALB.DatumIzdavanja AS 'DatumIzdanja',
    SEC_TO_TIME(SUM(TIME_TO_SEC(PJ.Trajanje))) AS 'Trajanje'
FROM
    ALBUM ALB
JOIN
    IZVODJAC IZV ON ALB.IZVODJAC_IdIzvodjaca = IZV.IdIzvodjaca
LEFT JOIN
    PJESMA PJ ON ALB.IdAlbuma = PJ.ALBUM_IdAlbuma
GROUP BY
    ALB.IdAlbuma
ORDER BY
    ALB.Naziv;
    

    
