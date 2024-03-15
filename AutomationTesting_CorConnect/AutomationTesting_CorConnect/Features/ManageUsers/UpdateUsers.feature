Feature: UpdateUsers

Validate fields visible on  Edit User page and Update fields different types of  users 
Background: Load Application and navigate to Manage Users page
Given User "Admin" is on "Manage Users" page



@CON-26334 @CON-26182 @19.0 @CON-26734 @Smoke 
Scenario Outline: GUI validation of Edit New User Page for all types of users

When  User navigates to Edit User Page for "<username>"
Then  On Edit page valid fields are displayed for "<usertype>" and "<entitytype>"
Examples: 
| username          | usertype         | entitytype |
| SuppRegularUser   | Regular Users    | Dealer     |
| BuyrRegularUser   | Regular Users    | Fleet      |
| SupEntAdminUser   | Entity Admin     | Dealer     |
| ByrEntAdminUser   | Entity Admin     | Fleet      |
| SuperAdminUser    | Super Admin      |            |
| CommAdminUser     | Community Admin  |            |


@CON-26334 @CON-26182 @19.0 @CON-26786 @Functional
Scenario Outline: Update all fields for different Users
When Update all editable fields for User "<username>" on Edit User Page
Then User "<username>" of user type <User Type> and entity type <Entity Type> is updated

Examples: 
| username          | usertype         | entitytype |
| SuppRegularUser   | Regular Users    | Dealer     |
| BuyrRegularUser   | Regular Users    | Fleet      |
| SupEntAdminUser   | Entity Admin     | Dealer     |
| ByrEntAdminUser   | Entity Admin     | Fleet      |
| SuperAdminUser    | Super Admin      |            |
| CommAdminUser     | Community Admin  |            |
