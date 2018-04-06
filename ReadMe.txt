
TIC Info 
---------

TICNumber 		A0
TICLevel		A1
GuestEMail		A2
GuestMobNo		A3
GuestBookingWebsite	A4		


config
=======

BookedViaTajSite	TAJ		Booked Via Taj webSite
TICLevel		PLATINUM	TIC Level
	
IsEnableTICPlan		YES		Is Enable TIC Plan	

BookedViaTajSitePlanid	15		Booked Via Taj webSite Planid
TICLevelPlanId		16		TIC Level PlanId

AllFreePlanId		5,15,16		All Free PlanId (Free and TajSitePlanid)
NomadixUserDisplay	RG		Nomadix User Display




FIAS or AMADEUS
----------------

IsFindGuestNoOfStay			YES		IsFind Guest NoOf Stay
IsFindGuestNoOfStayPlanBased		YES		Enable NoOfStay PlanBased
ComplimentPlanId			5		Standard PlanId
IsEnableSecureLogin			YES		IsEnableSecureLogin
IsEnableMultiplePayPlan			YES		Enable Multiple Pay Plans

FIDELIO
----------------

IsFindGuestNoOfStay			NO		IsFind Guest NoOf Stay
IsFindGuestNoOfStayPlanBased		NO		Enable NoOfStay PlanBased
ComplimentPlanId			5		Standard PlanId
IsEnableSecureLogin			YES		IsEnableSecureLogin
IsEnableMultiplePayPlan			YES		Enable Multiple Pay Plans


*********************************



ErrorCode
===========

101	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your first Device
102	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your second Device
103	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your third Device
104	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your fourth Device
105	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your fifth Device
106	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your sixth Device
107	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your seventh Device
108	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your eigth Device
109	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your ninth Device
110	Dear Guest, Thank you for providing the details. Your internet service is now active.This is your tenth Device


*********************************


CREATE TABLE [dbo].[GuestPersonalInfo](
	[GuestId] [int] NULL,
	[GuestEmail] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GuestCountryCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GuestMobNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GuestCreatedOn] [datetime] NULL,
	[GuestModifiedOn] [datetime] NULL,
	[GuestILike] [bit] NULL,
	[GuestIAgree] [bit] NULL
) ON [PRIMARY]

*********************************


CREATE TABLE [dbo].[PasswordChange](
	[PasswordId] [int] IDENTITY(1,1) NOT NULL,
	[PasswordGrCId] [int] NULL,
	[PasswordNew] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PasswordModifiedOn] [datetime] NULL,
	[PasswordModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PasswordChangeCount] [int] NULL,
	[PasswordCreatedOn] [datetime] NULL,
 CONSTRAINT [PK_PasswordChange] PRIMARY KEY CLUSTERED 
(
	[PasswordId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

**************************** 


CREATE TABLE [dbo].[ResetpwdInfo](
	[ResetInfoGId] [int] NULL,
	[ResetInfoPassword] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ResetInfoModifiedOn] [datetime] NULL,
	[ResetInfoModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ResetInfoMAC] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ResetInfoMACcount] [int] NULL,
	[ResetInfoAfterMACcount] [int] NULL,
	[ResetInfoAfterMAC] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ResetInfoCalc] [int] NULL
) ON [PRIMARY]

**************************** 

ErrorCode
=========


32	Dear Guest, Invalid secure password. If you forgot password, call cyber support for re-set password.

**************************** 

Config
=======
AllowInputsInSecurePwd

**************************** 

Rights
======

90	MUADMOP		Menu->Admin->DeActivate Password
91	MUADMAOP	Menu->Admin->DeActivate Password



#############################################################

Taj Hotles and Resorts
======================

FIDELIO
--------

PMSName    : FIDELIO
PMSVersion : 6


AMADEUS, FIAS
-------------

PMSName    : AMADEUS
PMSVersion : 3

********************************************

NR-Login Config for Amadeus PMS
-------------------------------

QueueName		topms	NR Inquiry
NRTimeSpan		2	NRTimeSpan
NRTryCount		5	NRTryCount
NRBeforeDBTimeSpan	2	NRBeforeDBTimeSpan


LongStay configuration as follows,
-----------------------------------

IsEnableLongStay		NO	Enable LongStay Or Not, YES or NO	5
LongStayPlanId			12	Long stay planid			1
Discount<15			25	Discount < 15 Days, 25% Percentage	0
Discount>15			50	Discount > 15 Days, 50% Percentage	0


Note: create 1 plan as like "12" and configure in "config" table

Multiple pay plan, display in dropdownlist configuration as follows,
-------------------------------------------------------------------------
IsEnableMultiplePayPlan		YES	Enable Multiple Pay Plans	 	5


*******************************************
Installation date on 03-Dec-2014
================================


insert into config (Variable_name, Variable_Value, Variable_Desc, Variable_Type)
values('Amount<30days','5000','Long Stay Upto 29 Days','9')

insert into config (Variable_name, Variable_Value, Variable_Desc, Variable_Type)
values('Amount<7days','3500','Long Stay Upto 7 Days','9')

insert into config (Variable_name, Variable_Value, Variable_Desc, Variable_Type)
values('Amount>30days','9000','Long Stay More than 30 Days','9')

update config set Variable_Value='0.00' where Variable_name='ServiceTax'
update config set Variable_Value='0.00' where Variable_name='LuxuaryTax'

