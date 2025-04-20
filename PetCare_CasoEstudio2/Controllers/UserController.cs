using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PetCare_CasoEstudio2.Models;

[Authorize(Roles = "Administrador")]
public class UserController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();
    private UserManager<ApplicationUser> userManager;
    private RoleManager<IdentityRole> roleManager;

    public UserController()
    {
        userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
    }

    // GET: User
    public ActionResult Index()
    {
        var users = userManager.Users.ToList();
        return View(users);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        return View(); // Ya no enviamos roles
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existing = await userManager.FindByEmailAsync(model.Email);
            if (existing != null)
            {
                ModelState.AddModelError("Email", "El correo ya está en uso.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                NombreCompleto = model.NombreCompleto,
                PhoneNumber = model.PhoneNumber,
                Rol = "Cliente" // 🔒 Fijar rol como Cliente
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user.Id, "Cliente");
                return RedirectToAction("Index");
            }

            AddErrors(result);
        }

        return View(model);
    }

    // GET: User/Edit/id
    public ActionResult Edit(string id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        var user = userManager.FindById(id);
        if (user == null) return HttpNotFound();

        var model = new EditUserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            NombreCompleto = user.NombreCompleto,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Rol = user.Rol
        };

        ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Name", "Name", model.Rol);
        return View(model);
    }

    // POST: User/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(EditUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null) return HttpNotFound();

            user.UserName = model.UserName;
            user.NombreCompleto = model.NombreCompleto;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var currentRoles = await userManager.GetRolesAsync(user.Id);
            await userManager.RemoveFromRolesAsync(user.Id, currentRoles.ToArray());

            await userManager.AddToRoleAsync(user.Id, model.Rol);
            user.Rol = model.Rol;

            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Name", "Name", model.Rol);
        return View(model);
    }

    // GET: User/Delete/id
    public ActionResult Delete(string id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        var user = userManager.FindById(id);
        if (user == null) return HttpNotFound();

        return View(user);
    }

    // POST: User/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return HttpNotFound();

        var result = await userManager.DeleteAsync(user);
        if (result.Succeeded)
            return RedirectToAction("Index");

        AddErrors(result);
        return View(user);
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
            ModelState.AddModelError("", error);
    }
}
