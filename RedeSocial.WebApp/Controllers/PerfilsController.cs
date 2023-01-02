using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;
using RedeSocial.Infrastructure.Context;
using System.Security.Claims;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class PerfilsController : Controller
    {
        private readonly PerfilService _service;
        private readonly RedeSocialDbContext _context;

        public PerfilsController(PerfilService perfilService,RedeSocialDbContext context) {
            _service = perfilService;
            _context = context;

        }

        // GET: Perfils
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var perfil = _service.ConsultarPerfil(userId);
            return View(_service.ConsultarPerfils());
        }
        public IActionResult Relacionamentos(Guid id)
        {

            if (_context.Perfils_ != null)
            {
                var amizades = _context.Perfils_.Include(a => a.RelacionamentosB).ToList();
                var pessoa = _context.Perfils_.Find(id);
                return View(pessoa);
            }
            return Problem("Entity set 'RedeSocialDbContext.Perfil' is null.");
        }

        // GET: Perfils/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                id = GetUserId();
            }
            var perfil = _service.ConsultarPerfil(id.Value);
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        // GET: Perfils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Foto")] Perfil perfil, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                perfil.Foto = UploadImage(foto);
                perfil.Id = GetUserId();
                _service.CriarPerfil(perfil);
                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: Perfils/Edit/5
        public async Task<IActionResult> Edit(Guid? id, IFormFile foto)
        {
            if (id == null)
            {
                return NotFound();
            }
            var perfil = _service.ConsultarPerfil(id.Value);
            if (perfil == null)
            {
                perfil.Foto = UploadImage(foto);
                return NotFound();
            }
            return View(perfil);
        }

        

        // POST: Perfils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Foto")] Perfil perfil)
        {
            if (id != perfil.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = _service.AlterarPerfil(perfil);
                if (!result)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: Perfils/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var perfil = _service.ConsultarPerfil(id.Value);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // POST: Perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _service.ExcluirPerfil(id);
            return RedirectToAction(nameof(Index));
        }

        private Guid GetUserId() {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        private static string UploadImage(IFormFile imageFile)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=storagepadrao;AccountKey=JlOf5nHA5trVWIf8GtiCYWDASt/BNSPoOiYaRQkZdHlRi2qVTEgKfX7xVPb86/xVC4SB61HEdyCr+AStNi4oBw==;EndpointSuffix=core.windows.net";
            string containerName = "doat";
            var reader = imageFile.OpenReadStream();
            var cloundStorageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = cloundStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = container.GetBlockBlobReference(imageFile.FileName);
            Thread.Sleep(5000);
            blob.UploadFromStreamAsync(reader);
            return blob.Uri.ToString();

        }
        private static void DeleteFile(string foto)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=storagepadrao;AccountKey=JlOf5nHA5trVWIf8GtiCYWDASt/BNSPoOiYaRQkZdHlRi2qVTEgKfX7xVPb86/xVC4SB61HEdyCr+AStNi4oBw==;EndpointSuffix=core.windows.net";
            string containerName = "doat";
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            string arquivo = foto.Substring(foto.LastIndexOf('/') + 1);
            var blobClient = blobContainerClient.GetBlobClient(arquivo);
            blobClient.DeleteIfExists();
        }
    }
}
