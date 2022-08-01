--Admin table--

create table Admin(
	AdminId int identity (1,1) primary key,
	FullName varchar(200) not null,
	EmailId varchar(200) not null,
	Password varchar(200) not null,
	MobileNumber bigint not null
);

--inserting admin details--

insert into Admin 
values('Admin','admin@gmail.com','Anni1234',1234567890);

select * from Admin;

--admin login--
create procedure spAdminLogin(
	@EmailId varchar(200),
	@Password varchar(200)
	)
as
begin
	select * from Admin where EmailId=@EmailId and Password = @Password;
end
