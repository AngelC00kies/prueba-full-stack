create database GestionVentasDB;
go

use GestionVentasDB;
go

create table usuario(
    id int identity(1,1) primary key,
    username nvarchar(50) not null,
    passwordhash nvarchar(200) not null,
    role nvarchar(20) not null default 'user',
    activo bit not null default 1,
    fechacreacion datetime not null default getdate(),
    constraint UQ_Usuario_Username unique (username)
);
go

create table cliente(
    id int primary key identity(1,1),
    nombre nvarchar(100) not null,
    email nvarchar(100) not null,
    telefono varchar(20) not null
);
go

create table producto(
    id int primary key identity(1,1),
    nombre nvarchar(30) not null,
    descripcion nvarchar(250) not null,
    precio decimal(18,2) not null,
    stock int not null
);
go

create table ventas(
    id int primary key identity(1,1),
    fecha datetime default getdate(),
    idcliente int not null,
    total decimal(18,2) not null,
    foreign key (idcliente) references cliente(id)
);
go

create table detalleventas(
    id int primary key identity(1,1),
    idventa int not null,
    idproducto int not null,
    cantidad int not null,
    preciounitario decimal(18,2) not null,
    foreign key (idventa) references ventas(id),
    foreign key (idproducto) references producto(id)
);
go
