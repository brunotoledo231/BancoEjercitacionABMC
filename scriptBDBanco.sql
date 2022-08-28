create database Banco_Eje_1_3
use banco_eje_1_3
create table tipos_cuentas
(
	id_tipo_cuenta int identity (1,1),
	nombre_cuenta varchar (50),
	constraint pk_tipo_cuenta primary key (id_tipo_cuenta)
)

create table clientes
(
	id_cliente int identity (1,1),
	apellido varchar (50),
	nombre varchar (50),
	constraint pk_cliente primary key (id_cliente)
)

create table cuentas
(
	id_cuenta int identity (1,1),
	cbu int ,
	saldo decimal (12,2),
	ultimo_movimiento datetime,
	id_tipo_cuenta int,
	id_cliente int,
	constraint pk_cbu primary key (cbu),
	constraint fk_cuenta_tipo_cuenta foreign key (id_tipo_cuenta)
		references tipos_cuentas (id_tipo_cuenta),
	constraint fk_cuenta_cliente foreign key (id_cliente)
		references clientes (id_cliente)
)
set dateformat DMY

--insertando tipos de cuentas
insert into tipos_cuentas (nombre_cuenta) values ('Caja de Ahorros en Pesos')
insert into tipos_cuentas (nombre_cuenta) values ('Cuenta Corriente en Pesos')
insert into tipos_cuentas (nombre_cuenta) values ('Cuenta Corriente en Dolares')
insert into tipos_cuentas (nombre_cuenta) values ('Cuenta Sueldo')
--insertando clientes
insert into clientes (apellido,nombre,dni) values ('Perez','Damian',34567890)
insert into clientes (apellido,nombre,dni) values ('Locasio','Fernando',31567890)
insert into clientes (apellido,nombre,dni) values ('Maidana','Adriana',12456789)
insert into clientes (apellido,nombre,dni) values ('Farfan','Pablo',10234567)
insert into clientes (apellido,nombre,dni) values ('Torres','Paola',9789456)
--insertando una cuenta de prueba
insert into cuentas (cbu,saldo,ultimo_movimiento,id_tipo_cuenta,id_cliente)values(123467999,45123.12,'17/07/2022',1,1)

--sp para cargar combo de cuentas
create proc SP_LISTAR_TIPOS_CUENTAS
as
select *
from tipos_cuentas

exec SP_LISTAR_TIPOS_CUENTAS

--sp para cargar combo de clientes
create proc SP_LISTAR_CLIENTES
as
select *
from clientes

exec SP_LISTAR_CLIENTES

--Sp para mostrar datos en  datagridview
create proc SP_LISTAR_DETALLE
as
select c.cbu,cl.id_cliente,tc.nombre_cuenta,cl.apellido,cl.nombre,cl.dni,c.saldo,c.ultimo_movimiento,tc.id_tipo_cuenta
from cuentas c
join clientes cl on c.id_cliente=cl.id_cliente
join tipos_cuentas tc on c.id_tipo_cuenta=tc.id_tipo_cuenta

exec SP_LISTAR_DETALLE

--Sp para insertar un nuevo registro de cliente
create proc SP_INSERTAR_CLIENTESNUEVOS
@apellido varchar(50),
@nombre varchar(50),
@dni int
as
begin
insert into clientes(apellido,nombre,dni)
values( @apellido,@nombre,@dni);
end

-- para editar cuentas de clientes
create proc SP_EDITAR_CUENTAEXISTENTE
@cbunum int ,
@saldo decimal (10,2),
@tipocue int,
@fecha date,
@idcliente int
as
begin
update cuentas 
set 
saldo=@saldo,
id_tipo_cuenta=@tipocue,
ultimo_movimiento=@fecha,
id_cliente=@idcliente
where cbu=@cbunum

end

