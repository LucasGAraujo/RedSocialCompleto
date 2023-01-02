using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly RedeSocialDbContext _context;
        private readonly WithDapper _dapper;

        public PostRepository(RedeSocialDbContext context, WithDapper dapper)
        {
            _context = context;
            _dapper = dapper;
        }
        
        public Post ConsultarPost(Guid id)
        {
            return _context.Posts.Find(id);
        }
        public int AlterarPost(Post post)
        {
            _context.Update(post);
            return _context.SaveChanges();
        }
        public ICollection<Post> ConsultarPosts()
        {
            return _context.Posts.ToList();
        }

        public IEnumerable<Post> GetAllPostsByPerfil(string perfil)
        {
            return _dapper.GetAllPostsByPerfil(perfil);
        }
        public ICollection<Comentario> ConsultarComentarios()
        {
            return _context.Comentarios.ToList();
        }

        public void CriarPost(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();
        }
        public void CriarComentario(Comentario comentario)
        {
            _context.Add(comentario);
            _context.SaveChanges();
        }
        public int ExcluirPost(Guid id)
        {
            Post post = _context.Posts.Find(id);

            if (post == null)
            {
                return 0;
            }
            _context.Posts.Remove(post);
            return _context.SaveChanges();
        }
    } 
}

