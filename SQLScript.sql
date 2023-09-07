create database loans;
use loans;

create table item_master(
item_id varchar(6) primary key,
item_description varchar(25) NOT NULL,
issue_status char(1) NOT NULL,
item_make varchar(25) NOT NULL,
item_category varchar(20) NOT NULL,
item_valuation int not null check (item_valuation between 0 and 999999)
);

create table employee_master(
employee_id varchar(6) primary key,
employee_name varchar(20) not null,
designation varchar(25) not null,
department varchar(25) not null,
gender char(1) not null,
date_of_birth DATE default getdate(),
date_of_joining DATE default getdate()
);

create table employee_issue_details(
issue_id varchar(6) primary key,
employee_id varchar(6) references employee_master(employee_id),
item_id varchar(6) references item_master(item_id),
issue_date DATE default getdate(),
return_date DATE default getdate()
);

create table loan_card_master(
loan_id varchar(6) primary key,
loan_type varchar(15) not null,
duration_in_years int not null check (duration_in_years between 0 and 99)
);

create table employee_card_details(
employee_id varchar(6) references employee_master(employee_id),
loan_id varchar(6) references loan_card_master(loan_id),
card_issue_date DATE default getdate()
);

