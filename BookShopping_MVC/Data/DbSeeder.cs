
namespace BookShopping_MVC.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider serviceProvider)
        {   // IServiceProvider -> GetService --> here implement (DI) and get from program.cs and identity 
            var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>();

            // adding some roles to DB
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));   // here create role for admin
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));    // the same thing for user

            // create admin user 
            // Data seeding 
            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            if (userMgr != null)
            {
                var userInDb = await userMgr.FindByEmailAsync(admin.Email);

                if (userInDb is null)
                {
                    await userMgr.CreateAsync(admin, "Admin@123");
                    await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
                }
            }

        }
    }
}
