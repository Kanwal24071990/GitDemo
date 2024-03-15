Feature: Login

Login successful and login failed scenarios for different users

@CON-26182 @19.0 @CON-26739 @Functional
Scenario: login successful for Non Notification users
	Given User is on login page 
	When Login with username "ActiveNonNotifyUser" and password "abc"
	Then User "ActiveNonNotifyUser" is logged in 

@CON-26182 @19.0 @CON-26740 @Functional
Scenario Outline:login unsuccessful for InActive users
	Given User is on login page 
	When Login with username "<username>" and password "<password>"
	Then Login failed with error "Invalid User ID or Password"
Examples: 
| username                | password      | 
| InActNonNotifyUser      | abc           |
| InActNotifyUser         | abc           | 


@CON-26182 @19.0 @CON-26741 @Functional
Scenario: Login unscuccessful for Active Notification users
    Given User is on login page 
	When Login with username "ActiveNotifyUser" and password "abc"
	Then Login failed with error "User setup as notification user and cannot login to the system"

@CON-26182 @19.0 @CON-26788 @Functional
Scenario: Login unscuccessful for Suspended users
	Given User is on login page
	When Login with username "<username>" and password "<password>"
	Then Login failed with error "Your account has been suspended"
Examples: 
| username                | password      | 
| SuspendNonNotifyUser    | abc           |
| SuspendNotifyUser       | abc           |



@CON-26182 @19.0 @CON-26787 @Functional
Scenario: Impersonation successful for the EULA acknowledged users
	Given User "Admin" is on "Manage Users" page
	When Impersonate to the user "<username>"
	Then User "<username>" is logged in 
Examples: 
| username                 |  
| ActiveNonNotifyUser      |
| ActiveNotifyUser         |


