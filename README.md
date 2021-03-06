# Graph API App-Only Web API Sample #

This provides Web API samples built-in ASP.NET 5 and ASP.NET MVC 6 using Graph API, registered as an App-only application.

> This requires [Visual Studio 2015](https://www.visualstudio.com).


## Steps to Run this Sample Web API Application ##

As this sample application is using [Azure Active Directory (AAD)](https://azure.microsoft.com/en-us/services/active-directory) and [Graph API](https://graph.microsoft.io), it's a mandatory to register this app on your AAD first. Please follow the steps below.


### Clone Repository ###

Clone this repository to get sample source code.


### Create User Account on AAD ###

> You can skip this step, if you have already got a user account on AAD.
> **NOTE**: Microsoft Account won't work with this sample app.

Create a user account on AAD tenant. Once created, the user account should be a co-administrator of the Azure subscription that is currently being used.


### Register Application to AAD ###

Create and register a new application to AAD so that the sample Web API app can communicate with AAD and Graph API.

#### Step 1 &ndash; Create Application to AAD ####

* Login to [Azure Management Portal](https://manage.windowsazure.com).
* Select Active Directory.

![](./images/graph-api-app-only-sample-01.png)

* Create a new application.

![](./images/graph-api-app-only-sample-02.png)

* Choose the `Add an application my organization is developing` option.

![](./images/graph-api-app-only-sample-03.png)

* Enter the application name like `Graph API App-only Sample` and select the `Web Application and/or Web API` option. 

![](./images/graph-api-app-only-sample-04.png)

* Enter `https://[tenant-name]/GraphApiAppOnlySample` for both fields. `[tenant-name]` should look like `contoso.onmicrosoft.com`. Please note that both won't be used at all.

![](./images/graph-api-app-only-sample-05.png)

Now the app has been created.


#### Step 2 &ndash; Configure Application ####

* Once the app is created, click the `configure` tab.

![](./images/graph-api-app-only-sample-06.png)

* Get the `Client ID`.

![](./images/graph-api-app-only-sample-07.png)

* Get the secret key. Note that the key is only displayed once after click the `Save` button at the bottom.

![](./images/graph-api-app-only-sample-08.png)

* Add another application called `Microsoft Graph`

![](./images/graph-api-app-only-sample-10.png)

* Give "Read directory data" permission to `Microsoft Graph`, as this permission is only necessary to run the sample app.

![](./images/graph-api-app-only-sample-11.png)

> **ATTENTION**: In the production environment, appropriate number of application permissions **MUST** be given to avoid any security breach.

The app has been configured.


### Update Settings in Sample Application ###

As the app has been registered and configured, the sample Web API app should be setup with appropriate settings. Firstly, open `appsettings.json`

```
{
  ...
  "GraphApp": {
    "Tenant": "contoso.onmicrosoft.com",
    "AuthUrl": "https://login.microsoftonline.com/{0}",
    "GraphUrl": "https://graph.microsoft.com",
    "Version": "v1.0",
    "ClientId": "client-id-value",
    "ClientSecret": "client-secret-key-value",
    "AppId": "https://contoso.onmicrosoft.com/GraphApiAppOnlySample"
  }
}
```

Then change values:

* `Tenant`: `contoso.onmicrosoft.com` to your tenant name.
* `ClientId`: Client ID from the app.
* `SlientSecret`: Secret key from the app.
* `AppId`: `contoso.onmicrosoft.com` to your tenant name.


### Trust IIS or IIS Express with a Self-signed Certificate ###

> * You can skip this step, if you intend to publish this app to Azure.
> * You can skip this step, if you already have a self-signed certificate on your root certificate storage.

All communications with AAD and Graph API are performed through a secure channel (SSL/TLS), this sample app **MUST** be signed with a root certificate. However, this is a developer's local environment, so a self-signed certificate should be issued and stored as a root certificate. If you don't store the self-signed certificate, you'll see the following message popped up when you run VS2015.

![](./images/graph-api-app-only-sample-12.png)

The following steps show how to register self-signed certificate to the root certificate store using PowerShell.


#### Step 1 &ndash; Check Self-signed Certificate ####

First, Check if you have a self-signed certificate in your personal certificate store.

```
PS C:\WINDOWS\system32> dir Cert:\LocalMachine\My


    Directory: Microsoft.PowerShell.Security\Certificate::LocalMachine\My


Thumbprint                                Subject                                               
----------                                -------                                               
EBD8********************************6E42  CN=localhost
```

If there's no certificate with name of `CN=localhost`, you should create the one using `makecert.exe`. The easiest way to execute `makecert.exe` is to run `Developer Command Prompt for VS2015`.

```
makecert.exe -r -pe -n "CN=localhost" -b 01/01/2015 -e 12/31/2020 -ss My -len 2048
``` 

* `-r`: Create a self signed certificate.
* `-pe`: Mark generated private key as exportable.
* `-n`: Certificate subject X509 name. eg) `-n "CN=localhost"`
* `-b`: Start of the validity period in `mm/dd/yyyy` format; default to now.
* `-e`: End of validity period in `mm/dd/yyyy` format; defaults to 2039.
* `-ss`: Subject's certificate store name that stores the output certificate. eg) `-ss My`
* `-len`: Generated Key Length (Bits). Default to `2048` for 'RSA' and `512` for 'DSS'.


#### Step 2 &ndash; Store Self-signed Certificate to Root Store ####

Second, store the self-signed certificate to the root store.

```
PS C:\WINDOWS\system32> $cert = (Get-Item Cert:\LocalMachine\My\EBD8********************************6E42)
PS C:\WINDOWS\system32> $store = (Get-Item Cert:\Localmachine\Root)
PS C:\WINDOWS\system32> $flags = [System.Security.Cryptography.X509Certificates.OpenFlags]::ReadWrite
PS C:\WINDOWS\system32> $store.Open($flags)
PS C:\WINDOWS\system32> $store.Add($cert)
PS C:\WINDOWS\system32> $store.Close()
```

Finally, you can verify the self-signed certificate has been stored into the root store.

```
PS C:\WINDOWS\system32> dir Cert:\LocalMachine\Root


    Directory: Microsoft.PowerShell.Security\Certificate::LocalMachine\Root


Thumbprint                                Subject                                               
----------                                -------                                               
...
EBD8********************************6E42  CN=localhost 
...
```


### Build and Run Sample Web API Application ###

All setup has been completed! Now, build the solution in Visual Studio 2015 and punch `F5` key to get into the `Debug` mode. Then you'll get a JSON response of your tenant organisation details.


## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work, please send us a pull request onto our `dev` branch for review.


## License ##

**Graph API App-Only Web API Sample** is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2015 [DevKimchi](http://devkimchi.com)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
