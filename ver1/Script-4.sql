create table roles (
    id serial primary key,
    role varchar(100) not null
);

create table users (
    id serial primary key,
    surname varchar(100) not null,
    name varchar(100) not null,
    patronymic varchar(100),
    login varchar(100) not null unique,
    role_id int not null references roles(id)
);

create table categories (
    id serial primary key,
    category varchar(100) not null
);

create table sub_categories (
    id serial primary key,
    sub_category varchar(100) not null
);

create table manufacturers (
    id serial primary key,
    manufacture varchar(150) not null
);

create table products (
    id serial primary key,
    category_id int not null references categories(id),
    sub_category_id int not null references sub_categories(id),
    image varchar(255),
    name varchar(255) not null,
    manufacture_id int not null references manufacturers(id),
    description text,
    structure text,
    cost numeric(10, 2) not null
);

create table sizes (
    id serial primary key,
    size numeric(4, 1) not null
);

create table stock_items (
    product_id int not null references products(id),
    size_id int not null references sizes(id),
    quantity int not null default 0,
    primary key (product_id, size_id)
);

create table orders (
    id serial primary key,
    date Date not null,
    user_id int not null references users(id)
)

create table orders_products (
	order_id int not null references orders(id),
	product_id int not null references products(id),
	quantity int not null default 1,
	size_id int not null references sizes(id),
	primary key (product_id, order_id)
)