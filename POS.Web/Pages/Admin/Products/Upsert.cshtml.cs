using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Data.Repository.IRepository;
using POS.Models;

namespace POS.Web.Pages.Admin.Products
{
    [Authorize]
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public Product Product { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            Product = new();
            Product.Id = 0;
            Product.IsActive = true;
        }
        public void OnGet(int? id)
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            if (id != null && id != 0)
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            }
        }
        public IActionResult OnPost()
        {
            Product.Image = getImageURL(Product.Image);
            if (Product.Id == 0)
                _unitOfWork.Product.Add(Product);
            else
                _unitOfWork.Product.Update(Product);
            _unitOfWork.Save();
            TempData["success"] = $"El producto ha sido {(Product.Id == 0 ? "creado" : "actualizado")} correctamente";
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
                var new_file = "img\\products\\" + Guid.NewGuid().ToString() + ext;
                var path = Path.Combine(wwwrootFolder, new_file);

                using (var stream = new FileStream(path, FileMode.Create))
                    files[0].CopyTo(stream);

                return @"\" + new_file;
            }
            return image;
        }
    }
}
