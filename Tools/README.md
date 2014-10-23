# Install notes

#### NhProf
1. Download [NhProf](http://www.hibernatingrhinos.com/downloads/NHProf/latest)
2. Unzip to `Tools/NhProf/`.

#### StyleCop
1. Download and install from [StyleCop](http://stylecop.codeplex.com/releases/view/79972)
2. install to Visual Studio.

#### Visual Studio Item Templates

#### Easy way #####
1. Unzip Appva-ItemTemplates.zip into C:\Users\{YOUR WINDOWSUSER}\Documents\Visual Studio 2013\Templates\ItemTemplates 
2. Edit the author node `<author><a href="mailto:{YOUR_EMAIL_ADDRESS}">{YOUR_FULL_NAME}</a></author>` in each unziped template.
3. Done


##### Hardcore way #####
##### Class.cs
1. Replace Class.cs with custom Class.cs in
   `C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\ItemTemplates\CSharp\Code\1033\Class\Class.cs`
2. Edit the author node `<author><a href="mailto:{YOUR_EMAIL_ADDRESS}">{YOUR_FULL_NAME}</a></author>`.

##### Interface.cs
1. Replace Interface.cs with custom Interface.cs in
   `C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\ItemTemplates\CSharp\Code\1033\Interface\Interface.cs`
2. Edit the author node `<author><a href="mailto:{YOUR_EMAIL_ADDRESS}">{YOUR_FULL_NAME}</a></author>`.

##### MVC and Web API
1. Find the appropriate class to overwrite.
2. Edit the author node `<author><a href="mailto:{YOUR_EMAIL_ADDRESS}">{YOUR_FULL_NAME}</a></author>`.

