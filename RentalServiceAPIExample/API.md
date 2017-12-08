# Rental Service Example API Documentation


# Allowed HTTPs requests:
<pre>
POST    : Update resource
GET     : Get a resource or list of resources
DELETE  : To delete resource
</pre>

# Description Of Usual Server Responses:
- 200 `OK` - the request was successful (some API calls may return 201 instead).
- 201 `Created` - the request was successful and a resource was created.
- 204 `No Content` - the request was successful but there is no representation to return (i.e. the response is empty).
- 400 `Bad Request` - the request could not be understood or was missing required parameters.
- 401 `Unauthorized` - authentication failed or user doesn't have permissions for requested operation.
- 403 `Forbidden` - access denied.
- 404 `Not Found` - resource was not found.

# User Controller [/api/user]


---


---
## Login[/Login]
Logs in the passed in user.
+ Verbs
	+POST
+ Parameters
	+ username (required, string) ... The username of the user
	+ password (required, string) ... The password of the user
	+ persist (required, bool) ... whether or not the auth cookie should persist
	
+ Response 200 
	Returns the Authentication cookie you need to authenticate with other API calls
+ Response 403
	Login failed.

## Register[/Register]
Registers a new user
+ Verbs
	+POST
+Parameters
	+ Username (required, string) ... The username of the user
	+ Email (required, string) ... The Email of the user
	+ Password (required, string) ... The password of the user
+ Response 200 
	Account Creation was successful.
	Returns the Authentication cookie you need to authenticate with other API calls
+ Response 403
	User Creation was successful, but the system failed to log the user in immediately.
+ Response 400
	User Creation was unsuccessful, please check parameters
	
## LogOut[/LogOut]
+ Verbs
	+POST
Logs out.
+ Response 204
	Your auth cookie has been terminated
	
## GetAccountDetails[/GetAccountDetails]
+ Verbs
	+GET
Gets the details of your account
+ Response 200
	{
		Email : '',
		Username : '',
		RentalHistories : [],
		SettingsValues : [] 
	}
	
## ChangePassword[/ChangePassword]
Changes your password
+ Verbs
	+POST
+ Parameters
	+ CurrentPassword (required, string) ... Your existing password.
	+ Password (required, string) ... The password you're changing to.
	+ ConfirmPassword (required, string) ... The password, repeated to ensure accuracy

+ Response 204
	Password change successful
+ Response 400
	Password either didn't match the confirm field or didn't meet the minimum security requirements. Check the response message.

## AddPaymentMethod[/AddPaymentMethod]
Adds a new payment method to the current user.
+ Verbs
	+POST
+ Parameters
	+ NotACCNumber (required, int) ... A mock value, meant to represent CC information prior to secure storage
	
+ Response 201
	Payment Method was created

## RemoveAPaymentMethod[/RemoveAPaymentMethod]
Removes a Payment Method
+ Verbs
	+POST
+ Parameters
	+ paymentMethodId (required, string) ... GUID, representing the payment method.
	
+ Response 204
	Payment Method Removed
+ Response 400
	Malformed Payment ID.
+ Response 404
	Payment Method matching the ID was not found.
	
## UpdateUserAddress
Updates the current user's address
+ Verbs
	+POST
+ Parameters
	+ Address 1 (optional, string) ... First component of an Adresss
	+ Address 2 (optional, string) ... Second component of an Address
	+ City (optional, string) ... City component of an Address
	+ State (optional, string) ... State Component of an Address
	+ Zip (optional, string) ... Zip Component of an Address
	
+ Response 204
	Either adds or updates the on file address of the user.
	
# Titles Controller[/api/titles]
## GetAll [/GetAll]
Get's all of the Titles
+ Verbs
	+GET
+ Response 200
	[
		{
			DisplayName : '',
			TotalStock : 0,
			AvailableStock : 0,
			MediaType : '',
			TitleMetaValues : [],
			RentalHistories : [] //Always an empty array
		}
	]
## GetTitle[/GetTitle]
+ Verbs
	+GET
+ Parameters
	+ id (required, string) ... GUID of a TitleMetaValues
+ Response 200

	{
		DisplayName : '',
		TotalStock : 0,
		AvailableStock : 0,
		MediaType : '',
		TitleMetaValues : [],
		RentalHistories : [] //Always an empty array
	}
+ Response 400
	Malformed Title ID
+ Response 404
	Title not Found`

## Create[/Create]
Allows the Admin to create a new Title
+ Verbs
	+POST
+ Parameters
	+ DisplayName (optional, string) ... The display name
	+ TotalStock (optional/default of 0, int) ... The number of copies in inventory
	+ MediaType (required, enum) ... valid values of DVD or BluRay
+ Response 201
	Title Created
+ Response 403
	Unable to authenticate or lacks the admin role
	
## Update [/Update]
+ Verbs
	+POST
+ Parameters
	+ Id (required, string) ... The GUID of the title you're updating
	+ DisplayName (required, string) ... The display name
	+ TotalStock (required, int) ... The number of copies in inventory
	+ MediaType (required, enum) ... valid values of DVD or BluRay
+ Response 200
	Title Updated
+ Response 403
	Unable to authenticate or lacks the admin role
	
## Delete[/Delete]
+ Verbs
	+POST
+ Parameters
	+ Id (required, string) ... The GUID of the title you're updating
+ Response 204
	Title Deleted
+ Response 403
	Unable to authenticate or lacks the admin role
+ Response 404
	Title not Found

## ProcessReturn[/ProcessReturn]
Admin function that updates the status of a rental to the Returned state.
+ Verbs
	+POST
+ Parameters
	+ Id (required, string) ... The GUID of the rental history you're processing
+ Response 204
	Return has been processed
+ Response 400
	Malformed ID
+ Response 404
	RentalHistory not Found
+ Response 409
	This Rental History has already been processed!

## AddMetaTagToTitle[/AddMetaTagToTitle]
Admin function that adds a metatag to a title
+ Verbs
	+POST
+ Parameters
	+ Value (required, string) - The Value of the Metatag you're applying
	+ MetaTagType (required, int) - The type of Metatag you're applying. Current valid values are 7(TitleDirector), 8(TitleGenre), 9(TitleActor)
	+ TitleId (required, string) - The GUID of the title
+ Response 201
	Metatag created
+ Response 400
	Malformed ID or Invalid MetatagType
+ Response 404
	Title not Found	

## CheckOutTitle[/CheckOutTitle]
+ Verbs
	+POST
+ Parameters
		+ TitleId (required, string) ... The GUID of the Title you're renting.
		+ PaymentMethodId (required, string) ... The GUID of the PaymentMethod you're using
+ Response 200
	Check out request recieved and sent to processing.
+ Response 400
	Malformed ID or Title has no available stock or payment method was rejected
+ Response 404
	Title not Found	

## GetRentalHistory[/GetRentalHistory]
Gets the Rental History of the current user.
+ Verbs
	+Get
+ Response 200
	[
		{
			UserId : '',
			TitleId : '',
			CurrentStatus : '', //enum with valid values of Processing, Shipping, Shipped, Arrived, Returned
			ReturnByDate : '',
			ReturnedDate : '',
			Returned : false,
			CreatedDate : '',
			CreatedBy : '',
			UpdatedDate : '',
			UpdatedBy : '',
			User: {},
			Title : {}
		},
	]