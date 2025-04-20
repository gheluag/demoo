drop database if exists AllForLife;
create database if not exists AllForLife;
use AllForLife;

create table roles(
idRole int primary key auto_increment,
roleName varchar(255) not null
);

insert into roles
values (1, 'Клиент'),
(2, 'Администратор');

create table users(
idUser int primary key auto_increment,
firstname varchar(255) not null,
lastname varchar(255) not null,
patronymic varchar(255) not null,
username varchar(255) not null unique,
passw varchar(255) not null,
roleId int not null,
foreign key (roleId) references roles (idRole)
);

select * from users

insert into users
values 
(17, 'лягуш', 'лягушкинск', 'лягушка', 'legush', 'legush', 2);

create table supplier(
idSupplier int primary key auto_increment,
nameSupplier varchar(255) not null
);

create table brand(
idBrand int primary key auto_increment,
nameBrand varchar(255) not null
);

create table category(
idCategory int primary key auto_increment,
nameCategory varchar(255) not null
);

create table product(
idProduct int primary key auto_increment,
productName varchar(255) not null,
article varchar(10) not null unique,
price decimal(10,2) not null,
maxSale int not null,
currentSale int not null,
count int not null,
prodDesc text not null,
imageURL varchar(500),
categoryId int not null,
brandId int not null,
supplierId int not null,
foreign key (categoryId) references category (idCategory),
foreign key (brandId) references brand (idBrand),
foreign key (supplierId) references supplier (idSupplier)
);

create table orders_statuses(
	id int primary key auto_increment,
    title varchar(32)
);

insert into orders_statuses(id, title)
values (1, 'Создан'), (2, 'Доставляется'), (3, 'Выполнен');

create table orders(
	id int primary key auto_increment,
    userId int NOT NULL,
    orders_status_id int NOT NULL,
    foreign key (userId) references users(idUser),
    foreign key (orders_status_id) references orders_statuses(id)
);

create table orders_products(
	id int primary key auto_increment,
    order_id int NOT NULL,
    product_id int NOT NULL,
    quantity int NOT NULL default 0,
    foreign key (order_id) references orders(id),
    foreign key (product_id) references product(idProduct)
);
