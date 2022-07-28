--Create DB--
CREATE DATABASE BookStore;

--Use--
USE BookStore;

--Table for User--
CREATE TABLE Users (
	UserId int identity (1,1) primary key,
	FullName varchar(200) not null,
	EmailId varchar(200) not null,
	Password varchar(200) not null,
	MobileNumber bigint not null
);

--Stored procedures for user--
--Register--

create procedure spRegister(
	@FullName varchar(200),
	@EmailId varchar(200),
	@Password varchar(200),
	@MobileNumber bigint
	)
as
begin
	insert into Users
	values(@FullName,@EmailId,@Password,@MobileNumber);
end

--Login--
create procedure spLogin(
	@EmailId varchar(200)
	)
as
begin
	select * from Users where EmailId=@EmailId;
end

--Forget Password--
create procedure spForget(
	@EmailId varchar(200)
	)
as
begin
	select * from Users where EmailId=@EmailId;
end

--reset password--
create procedure spResetPassword(
	@EmailId varchar(200),
	@Password varchar(200)
	)
as
begin
	update Users set Password = @Password where EmailId = @EmailId;
end