USE serenada;

INSERT INTO izvodjac VALUES (1, "ABBA", "Svedska", 1970, 0,"Popularni svedski bend.");
INSERT INTO izvodjac VALUES(2, "Beyonce", "SAD", 1997, 0, "DIVA.");
INSERT INTO izvodjac VALUES(3, "Snezana Savic", "Srbija", 1975, 0, "Bolji zivot.");
INSERT INTO izvodjac VALUES(4,"Harry Styles","Ujedinjeno Kraljevstvo", 2010, 0, "One direction.");
INSERT INTO izvodjac VALUES(5, "Jelena Karleusa", "Srbija", 1994, 0, "Zvezda.");
INSERT INTO izvodjac VALUES(6, "Dzej", "Srbija", 1978, 0, "Veseljak.");

INSERT INTO album VALUES(1, "OMEGA", "2023-08-09", 5);
INSERT INTO album VALUES(2, "RENAISSANCE", "2022-09-13", 2);
INSERT INTO album VALUES(3, "Harry's House", "2021-03-17",4);

INSERT INTO pjesma VALUES(1,"Topolska 18","3:30:00","1995-04-09",1,
"Nocas zvone neki sitni sati
izgubljeni lutaju mi snovi
svi zivimo neki zivot novi
a molimo stari da se vrati

Ref.
Topolska 18, soba podstanara
njena vrata samo secanje otvara
Topolska 18, na vratima pise
na staroj adresi mene nema vise

Nocas sijaju one iste zvezde
sto sjale su i za ljubav nasu
kad imali smo samo jednu sobu
jedan krevet i jednu casu

Ref.", null);
INSERT INTO pjesma VALUES(2, "Heated", "4:20:00", "2022-09-13",0,null,2);
INSERT INTO pjesma VALUES(3, "Cozy", "3:18:56", "2022-09-13",0,null,2);
INSERT INTO pjesma VALUES(4, "Move", "3:45:13", "2022-09-13",0,null,2);
INSERT INTO pjesma VALUES(5, "Cinema", "2:59:11", "2021-03-17",0,null,3);
INSERT INTO pjesma VALUES(6, "As it was", "4:13:10", "2021-03-17",0,null,3);
INSERT INTO pjesma VALUES(7, "Matilda", "2:28:21", "2021-03-17",0,null,3);
INSERT INTO pjesma VALUES(8, "XY", "2:35:11", "2023-08-09",0,null,1);
INSERT INTO pjesma VALUES(9, "JA", "4:44:01", "2023-08-09",1,null,1);
INSERT INTO pjesma VALUES(10, "La Bomba", "2:52:21", "2023-08-09",0,null,1);
INSERT INTO pjesma VALUES(11, "Nedelja", "2:59:11", "1989-02-10",0,null,null);
INSERT INTO pjesma VALUES(12, "Ritam da te pitam", "4:13:10", "1999-05-17",0,null,null);
INSERT INTO pjesma VALUES(13, "Waterloo", "3:28:21", "1972-06-17",0,null,null);
INSERT INTO pjesma VALUES(14, "Mamma mia", "1:59:11", "1979-03-02",0,null,null);
INSERT INTO pjesma VALUES(15, "Dancing Queen", "4:22:10", "1974-03-17",0,null,null);
INSERT INTO pjesma VALUES(16, "SOS", "2:28:21", "1976-07-12",0,null,null);

INSERT INTO pjesma_izvodjaca VALUES(1, 13);
INSERT INTO pjesma_izvodjaca VALUES(1, 14);
INSERT INTO pjesma_izvodjaca VALUES(1, 15);
INSERT INTO pjesma_izvodjaca VALUES(1, 16);
INSERT INTO pjesma_izvodjaca VALUES(2, 2);
INSERT INTO pjesma_izvodjaca VALUES(2, 3);
INSERT INTO pjesma_izvodjaca VALUES(2, 4);
INSERT INTO pjesma_izvodjaca VALUES(3, 1);
INSERT INTO pjesma_izvodjaca VALUES(4, 5);
INSERT INTO pjesma_izvodjaca VALUES(4, 6);
INSERT INTO pjesma_izvodjaca VALUES(4, 7);
INSERT INTO pjesma_izvodjaca VALUES(5, 8);
INSERT INTO pjesma_izvodjaca VALUES(5, 9);
INSERT INTO pjesma_izvodjaca VALUES(5, 10);
INSERT INTO pjesma_izvodjaca VALUES(6, 11);
INSERT INTO pjesma_izvodjaca VALUES(6, 12);

INSERT INTO korisnik VALUES(1, "Mihajlo", "Pašić", "miha@mail.com", "miha", "065 333 222", "2023-05-07",null, 0);
INSERT INTO korisnik VALUES(2, "Admin", "Admin", "admin@mail.com", "admin", "065 111 111", "2021-01-01",null, 0);
INSERT INTO korisnik VALUES(3, "Nikola", "Tesla", "tesla@mail.com", "tesla", "065 333 999", "2024-02-13",null, 0);

INSERT INTO plejlista VALUES(1, "Veseli mix", 2);
INSERT INTO lista VALUES(1, 10, "2023-12-29");
INSERT INTO lista VALUES(1, 2, "2023-11-02");
INSERT INTO lista VALUES(1, 12, "2024-01-13");

INSERT INTO svidjanje VALUES(2, 1, "2023-08-08");
INSERT INTO svidjanje VALUES(2, 9, "2022-12-12");

INSERT INTO pracenje VALUES(2, 3, "2023-12-12");
INSERT INTO pracenje VALUES(2, 1, "2024-01-01");