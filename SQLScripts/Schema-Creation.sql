drop database loans
create database loans;
use loans;

--all item details
--Item category is same as loan_type in loan_card_master
create table Item_master(
Item_id varchar(100) primary key,
Item_description varchar(200) NOT NULL,
Issue_status char(1) NOT NULL,
Item_make varchar(50) NOT NULL,
Item_category varchar(50) NOT NULL,
Item_valuation int NOT NULL check (Item_valuation between 0 and 999999)
);

--all employee details
create table Employee_master(
Employee_id varchar(100) primary key,
Password_hash varchar(100) NOT NULL,
Salt varchar(32) NOT NULL,
Employee_name varchar(50) NOT NULL,
Designation varchar(25) NOT NULL,
Department varchar(50) NOT NULL,
Gender char(1) NOT NULL,
Date_of_birth DATE,
Date_of_joining DATE default getdate()
);

--stores the loan card categories
create table Loan_card_master(
Loan_id varchar(100) primary key,
Loan_type varchar(50) unique,
Duration_in_years int not null check (Duration_in_years between 0 and 99)
);

--stores ALL the requests of all employees
create table Employee_request_details(
Request_id varchar(100) primary key,
Employee_id varchar(100) references Employee_master(Employee_id),
Item_id varchar(100) references Item_master(Item_id),
Request_date DATE default getdate(),
Request_status varchar(50) default 'Pending Approval',
Return_date DATE,
);

--stores the approved loans of an employee
--have added card id field since there is no unique key
create table Employee_loan_card_details(
Card_id varchar(100) primary key,
Employee_id varchar(100) references Employee_master(Employee_id),
Loan_id varchar(100) references Loan_card_master(Loan_id),
Card_issue_date DATE default getdate()
);
