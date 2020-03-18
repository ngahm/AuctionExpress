<!--
*** Thanks for checking out this README Template. If you have a suggestion that would
*** make this better, please fork the repo and create a pull request or simply open
*** an issue with the tag "enhancement".
*** Thanks again! Now go create something AMAZING! :D
***
***
***
*** To avoid retyping too much info. Do a search and replace for the following:
*** github_username, repo, twitter_handle, email
-->





<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
<!-- AuctionExpress -->
<br />
<p align="center">
https://github.com/ngahm/AuctionExpress

  <h3 align="center">AuctionExpress</h3>
</p>

<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Installation](#installation)
* [Usage](#usage)
* [Roadmap](#roadmap)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)



<!-- ABOUT THE PROJECT -->
## About The Project

AuctionExpress is a .Net Entity Framework WebAPI utilizing an n-tier architecture built during the BlueBadge phase of the Jan.-April 2020 Full-time Software Development bootcamp at ElevenFifty Academy.

Through the Api, registered users can post products for sale in an online auction format, as well as bid on currently open auctions.  Once the auctions are over, the products sellers can create transactions, simulating a third-party service that would handle credit card transactions.

The repo also includes a simple front-end built using razor pages to access and demonstrate the api end-points.


### Built With

* [.Net Framework]()
* [Bootstrap]()
* [Razor Pages]()



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Installation
 
1. Clone the repo
```sh
git clone https://github.com/ngahm/AuctionExpress.git
```
2. Restoring NuGetPackages
```sh
Need to include command line for restoring
```
3. Database Setup
- Update Database connection if needed.
- Enable and add a migration
- Update database to populate database with seed content.

<!-- USAGE EXAMPLES -->
## Usage

Account Registration and Tokens
Using the Account/Register api endpoint, a user can register and gain access to user-specific endpoints.  A list of endpoint access restrictions can be found in the Roles section.

Tokens can be requested through /token and sending a registered user name and password.  The included front-end requests a token through the "Login" button and stores it as a cookie for use while the user is "signed in."  
```sh
public ActionResult Login(LoginBindingModel model, string returnUrl)
        {
                var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", model.UserName ),
                        new KeyValuePair<string, string> ( "Password", model.Password )
                    };
                var content = new FormUrlEncodedContent(pairs);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/");
                var response = client.PostAsync("token", content).Result;
                var token= response.Content.ReadAsStringAsync().Result;
                Response.Cookies.Add(CreateCookie(token));
                //Response.Flush();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetOpenProduct", "ProductView");
                }
                ModelState.AddModelError(string.Empty, response.Content.ReadAsStringAsync().Result);

                return View();
            }
```

Example of including stored token during api requests.
```sh
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("product");
                responseTask.Wait();

```
Clicking "Logout"  sets the cookie expiration to the previous day.
```sh
public ActionResult LogOff()
        {
            if (Request.Cookies["UserToken"] != null)
            {
                Response.Cookies["UserToken"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Login","AccountView");
        }
```
### Roles
There are three important user roles that the api uses to grant/restrict access to api endpoints:Admin, ActiveUser, InActiveUser.  

Upon registration, a user is automatically added into the ActiveUser role, allowing them to perform actions such as post and bid on products.

Throught the Account/Deactivate endpoint, a user is moved to the InActiveUser role and are no longer allowed to perform actions such as post and bid on products while maintaining the integrity of the database.

The Admin role is used for administration purposes such as assigning users to roles and creating product categories.

Below is a detailed view into which endpoints can be accessed based on user roles.

Non-signedin users/inactiveusers
- Account
- RegisterLogin
- GetOpenAuctions
- GetOpenAuctionsByCategory
- Getspecific auction byid
- Getlist of all cateogries
- get specific category

Admin
- All Admin endpoints
- Post new category
- Update Category
- Delete Category
- GetAllTransactions
- GetAllBids

ActiveUser
- DeactivateUser
- PostProduct
- Get User's products
- UpdateProduct
- DeleteProduct
- All Bid endpoints
- All Transaction endpoints
- GetAllProducts

### Products
Users can post products they want to sell with the following information.
```sh
public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ProductQuantity { get; set; }
        public DateTimeOffset ProductStartTime { get; set; } = DateTimeOffset.Now;
        [Required]
        public DateTimeOffset ProductCloseTime { get; set; }
        public bool ProductIsActive
        {
            get
            {
                if (DateTimeOffset.Now < ProductStartTime || DateTimeOffset.Now > ProductCloseTime)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        [ForeignKey(nameof(ProductCategoryCombo))]
        public int? ProductCategoryId { get; set; }
        public virtual Category ProductCategoryCombo { get; set; }

        [ForeignKey(nameof(Seller))]
        [Required]
        public string ProductSeller { get; set; }
        public virtual ApplicationUser Seller { get; set; }

        public virtual ICollection<Bid> ProductBids { get; set; }

        public double HighestBid
        {
            get
            {
                if (ProductBids.Count >0)
                {
                    var item = ProductBids.Max(x => x.BidPrice);
                    return item;
                }
                    return 0;
            }
        }
        public double MinimumSellingPrice { get; set; }
    }
```

### Bids
Users can also bid on products.  When a user attempts to place a bid, it is evaluated against the existing product to determine if the bid is valid.
```sh
        public string ValidateBid(BidCreate bid)
        {
            var prodDetail = GetProductById(bid.ProductId);

            if (prodDetail == null)
                return "Product has been removed or does not exist.";
            if (prodDetail.ProductSeller == _userId.ToString())
                return "Users can not bid on products they are selling.";
            if (!prodDetail.ProductIsActive)
                return "Auction is closed";
            if (prodDetail.MinimumSellingPrice > bid.BidPrice)
                return "Bid must be higher than produt's minimum selling price.";
            if (prodDetail.HighestBid > bid.BidPrice)
                return "Bid must be higher than current selling price.";
            return "";
        }
```
### Categories
Basic class for grouping similar products together.  Administered by users in the Admin role.

### Transactions
When an auction ends, the product owner can create and administer a transaction based off the winning bid.  This is to simulate a third party service that would handle payment processing from the information of the winning bidder.

<!-- ROADMAP -->
## Roadmap
- Proposed additional feautures.
- Ability to post pictures.
- Transaction creation triggered when auction closes.
- Payment service integration.

<!-- CONTACT -->
## Contact

- Aaron Barchet - 
- Nick Gahm
- Jeremy Hansen - https://github.com/jhansen1344

Project Link: [https://github.com/ngahm/AuctionExpress]


<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

### Database Related Resources

Preserve User Data

https://stackoverflow.com/questions/8594448/best-way-to-store-deleted-user-data
https://github.com/dotnet/aspnetcore/blob/master/src/Identity/Core/src/SignInManager.cs
https://stackoverflow.com/questions/22652118/disable-user-in-aspnet-identity-2-0

Dealing with Multiplicity
https://www.codeproject.com/Questions/1000957/Multiplicity-not-allowed-in-Entity-Framework

Unmapped database entries (Accessing Read-only properties)

https://forums.asp.net/t/2031441.aspx?LINQ+to+Entities+does+not+recognize+the+method+method+and+this+method+cannot+be+translated+into+a+store+expression+
https://stackoverflow.com/questions/6919709/only-initializers-entity-members-and-entity-navigation-properties-are-supporte

Dealing with querying id's that do not exist in table

https://docs.microsoft.com/en-us/dotnet/api/system.linq.queryable.firstordefault?view=netframework-4.8

### Data Validation/Annotations
Data annotations

https://www.c-sharpcorner.com/article/model-validation-using-data-annotations-in-asp-net-mvc/

Custom model validation

http://dotnetmentors.com/mvc/how-to-do-custom-validation-using-validationattribute-of-aspnet-mvc.aspx 
https://forums.asp.net/t/2087963.aspx?Date+validation+with+data+annotation+where+restrict+back+dates
https://stackoverflow.com/questions/26991258/print-results-remaining-in-the-validationresult-error-list-net


### Api related resources

Creating descriptions for api endpoints

https://stackoverflow.com/questions/24284413/webapi-help-page-description
https://stackoverflow.com/questions/7982525/visual-studio-disabling-missing-xml-comment-warning/8532145

Tokens and authorizing users in mvc

https://dotnettutorials.net/lesson/token-based-authentication-web-api/
https://www.tektutorialshub.com/asp-net/asp-net-identity-tutorial-owin-setup/

Requesting and including token in api requests
https://stackoverflow.com/questions/38661090/token-based-authentication-in-web-api-without-any-user-interface

Creating cookies

https://stackoverflow.com/questions/39390240/create-cookie-asp-net-mvc
https://stormpath.com/blog/where-to-store-your-jwts-cookies-vs-html5-web-storage

Deleting cookies

https://stackoverflow.com/questions/6635349/how-to-delete-cookies-on-an-asp-net-website

### User Roles

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1
http://geekswithblogs.net/MightyZot/archive/2014/12/28/implementing-rolemanager-in-asp.net-mvc-5.aspx
https://stackoverflow.com/questions/19689183/add-user-to-role-asp-net-identity

Creating Roles

https://csharp-video-tutorials.blogspot.com/2019/07/creating-roles-in-aspnet-core.html

Edit roles

https://csharp-video-tutorials.blogspot.com/2019/07/edit-role-in-aspnet-core.html
Add/Remove Users from role

https://csharp-video-tutorials.blogspot.com/2019/07/add-or-remove-users-from-role-in-aspnet.html

Authorization

https://csharp-video-tutorials.blogspot.com/2019/06/authorization-in-aspnet-core.html
https://csharp-video-tutorials.blogspot.com/2019/07/aspnet-core-role-based-authorization.html

### Razor Pages resources

Consume WebAPI MVC Razer

https://www.tutorialsteacher.com/webapi/consume-web-api-get-method-in-aspnet-mvc

Disable input field in razor page

https://stackoverflow.com/questions/26179718/disable-input-element-using-razor
https://www.learnrazorpages.com/razor-pages/state-management

Razor pages model binding for adding users to role(s)

https://www.learnrazorpages.com/razor-pages/model-binding


Displaying DateTimeOffset in razor page

https://stackoverflow.com/questions/42072386/show-datetimeoffset-in-view-as-normal-date-format
https://stackoverflow.com/questions/21754071/templates-can-be-used-only-with-field-access-property-access-single-dimension

DateTimeOffset Formatting

https://stackoverflow.com/questions/44299292/best-way-to-convert-string-to-datetimeoffset

Redirect to view from different controller

https://stackoverflow.com/questions/879852/display-a-view-from-another-controller-in-asp-net-mvc

### C# related resources
Delegates
Anonymous Methods
Lambda Expressions 

https://itnext.io/delegates-anonymous-methods-and-lambda-expressions-5ea4e56bbd05
 
IEnumerable(TIEnumerable(T

https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic?view=netframework-4.8

LINQ
ToList() 
ToArray()

https://stackoverflow.com/questions/12014969/when-to-use-linqs-tolist-or-toarray

### ReadMe
https://github.com/othneildrew/Best-README-Template
