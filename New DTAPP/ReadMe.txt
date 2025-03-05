Package Manager Command to generate models from database

Scaffold-DbContext "Data Source=JFC-LTM-VMW2K16\JIIFC;Initial Catalog=New_DTAPP;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

After running, the default contsructor and onConfiguring method need to be removed from the context