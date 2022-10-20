CREATE DATABASE tpi_dabd;

USE tpi_dabd;

CREATE TABLE cartas (
id varchar (5) PRIMARY KEY,
carta varchar (25),
valor int);

INSERT INTO cartas (carta,valor,id) VALUES
("AS diamante",1, "1"),("AS pica",1,"2"),("AS trebol",1,"3"),("AS corazon",1,"4"),
("2 diamante",2,"5"),("2 pica",2,"6"),("2 trebol",2,"7"),("2 corazon",2,"8"),
("3 diamante",3,"9"),("3 pica",3,"10"),("3 trebol",3,"11"),("3 corazon",3,"12"),
("4 diamante",4,"13"),("4 pica",4,"14"),("4 trebol",4,"15"),("4 corazon",4,"16"),
("5 diamante",5,"17"),("5 pica",5,"18"),("5 trebol",5,"19"),("5 corazon",5,"20"),
("6 diamante",6,"21"),("6 pica",6,"22"),("6 trebol",6,"23"),("6 corazon",6,"24"),
("7 diamante",7,"25"),("7 pica",7,"26"),("7 trebol",7,"27"),("7 corazon",7,"28"),
("8 diamante",8,"29"),("8 pica",8,"30"),("8 trebol",8,"31"),("8 corazon",8,"32"),
("9 diamante",9,"33"),("9 pica",9,"34"),("9 trebol",9,"35"),("9 corazon",9,"36"),
("10 diamante",10,"37"),("10 pica",10,"38"),("10 trebol",10,"39"),("10 corazon",10,"40"),
("J diamante",10,"41"),("J pica",10,"42"),("J trebol",10,"43"),("J corazon",10,"44"),
("Q diamante",10,"45"),("Q pica",10,"46"),("Q trebol",10,"47"),("Q corazon",10,"48"),
("K diamante",10,"49"),("K pica",10,"50"),("K trebol",10,"51"),("K corazon",10,"52");

CREATE TABLE usuarios (
idUsuario int auto_increment primary key,
usuario varchar(50),
pass varchar(50)
);

CREATE TABLE sesiones (
idSesion int auto_increment primary key,
idUsuario int,
FOREIGN KEY (idUsuario) REFERENCES usuarios (idUsuario)
);

CREATE TABLE cartas_jugadas (
codJugadas int auto_increment primary key,
idCarta varchar(5),
idUsuario int,
FOREIGN KEY (idUsuario) REFERENCES usuarios (idUsuario),
FOREIGN KEY (idCarta) REFERENCES cartas (id));

CREATE TABLE cartas_sin_jugar (
codSinJugar int auto_increment primary key,
idCarta varchar(5),
idUsuario int ,
FOREIGN KEY (idUsuario) REFERENCES usuarios (idUsuario),
FOREIGN KEY (idCarta) REFERENCES cartas (id));

CREATE TABLE cartas_jugador (
codJugador int auto_increment primary key,
idCarta varchar(5),
idUsuario int ,
FOREIGN KEY (idUsuario) REFERENCES usuarios (idUsuario),
FOREIGN KEY (idCarta) REFERENCES cartas (id));

CREATE TABLE cartas_croupier (
codCroupier int auto_increment primary key,
idCarta varchar (5),
idUsuario int,
FOREIGN KEY (idUsuario) REFERENCES usuarios (idUsuario),
FOREIGN KEY (idCarta) REFERENCES cartas (id));

INSERT INTO usuarios (usuario,pass) VALUES 
("noe","123"),("nacho","123"),("gaston","123");