using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using POS.Data.Repository.IRepository;
using POS.Models;

namespace POS.Web.Pages.Admin.Categories;
[Authorize]
[BindProperties]
public class UpsertModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public Category Category { get; set; }
    public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
        Category = new();
    }
    public void OnGet(int? id)
    {
        if (id!=null)
            Category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
    }
    public IActionResult OnPost()
    {
        Category.Image = getImageURL(Category.Image);

        if (Category.Id == 0)
            _unitOfWork.Category.Add(Category);
        else 
            _unitOfWork.Category.Update(Category);
        _unitOfWork.Save();
        TempData["success"] = $"La categoría ha sido {(Category.Id == 0?"creada":"actualizada")} correctamente";
        return RedirectToPage("Index");
    }
    public string getImageURL(string image)
    {
        var wwwrootFolder = _hostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;

        if (files.Count > 0)
        {
            if (!string.IsNullOrEmpty(image))
                System.IO.File.Delete(Path.Combine(wwwrootFolder, image.TrimStart('\\')));

            var ext = Path.GetExtension(files[0].FileName);
            var new_file = "img\\categories\\" + Guid.NewGuid().ToString() + ext;
            var path = Path.Combine(wwwrootFolder, new_file);

            using (var stream = new FileStream(path, FileMode.Create))
                files[0].CopyTo(stream);

            return @"\"+new_file;
        }
        return image;
    }
}
