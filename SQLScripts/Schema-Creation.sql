create database loans;
use loans;

create table Item_master(
Item_id varchar(6) primary key,
Item_description varchar(25) NOT NULL,
Issue_status char(1) NOT NULL,
Item_make varchar(25) NOT NULL,
Item_category varchar(20) NOT NULL,
Item_valuation int not null check (Item_valuation between 0 and 999999)
);

create table Employee_master(
Employee_id varchar(6) primary key,
Password_hash varchar(30) not null,
Salt varchar(32) not null,
Employee_name varchar(20) not null,
Designation varchar(25) not null,
Department varchar(25) not null,
Gender char(1) not null,
Date_of_birth DATE default getdate(),
Date_of_joining DATE default getdate()
);

create table Employee_issue_details(
Issue_id varchar(6) primary key,
Employee_id varchar(6) references Employee_master(Employee_id),
Item_id varchar(6) references Item_master(Item_id),
Issue_date DATE default getdate(),
Return_date DATE default getdate()
);

create table Loan_card_master(
Loan_id varchar(6) primary key,
Loan_type varchar(15) not null,
Duration_in_years int not null check (Duration_in_years between 0 and 99)
);

create table Employee_card_details(
Employee_id varchar(6) references Employee_master(Employee_id),
Loan_id varchar(6) references Loan_card_master(Loan_id),
Card_issue_date DATE default getdate()
);
