using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using RedeSocial.Domain.Entities;
using RedeSocial.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using RedeSocial.Domain.Services;
using System.Security.Claims;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {        
            private readonly PostService _service;
            private readonly PerfilService _servicePerfil;

        public PostsController(PostService postService, PerfilService perfilService)
            {
                _service = postService;
                _servicePerfil = perfilService;
            }

            // GET: Posts
            public async Task<IActionResult> Index(string search) {

            //List<Post> posts = (List<Post>)_service.ConsultarPosts();
            IEnumerable<Post> posts;

            posts = _service.GetAllPostsByPerfil(search);

            return View(posts);
            }
        
        public async Task<IActionResult> Comentar([Bind("Id,Texto,Data")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {

                comentario.Id = new Guid();
                Post post = _service.ConsultarPost(GetUserId());
                comentario.Post = post;
                _service.CriarComentario(comentario);

                return RedirectToAction(nameof(Details));
            }
            return View(comentario);
        }
        // GET: Posts/Details/5
        public async Task<IActionResult> Details()
        {
            List<Comentario> comentarios = (List<Comentario>)_service.ConsultarComentarios();
            return View(comentarios);
        }


        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post,IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                post.Foto = UploadImage(foto);
                post.Id = new Guid();
                Perfil perfil = _servicePerfil.ConsultarPerfil(GetUserId());
                post.Perfil = perfil;
                _service.CriarPost(post); 
                return RedirectToAction(nameof(ListarPost));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, IFormFile foto)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _service.ConsultarPost(id.Value);
            if (post == null)
            {
                post.Foto = UploadImage(foto);
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Foto,Comentario,Data")] Post post, IFormFile foto)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = _service.AlterarPost(post);
                if (!result)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(ListarPost));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _service.ConsultarPost(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _service.ExcluirPost(id);
            return RedirectToAction(nameof(ListarPost));
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public async Task<IActionResult> ListarPost()
        {
            List<Post> posts = (List<Post>)_service.ConsultarPosts();
            return View(posts);
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
