using RedeSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Interfaces
{
    public interface IPostRepository
    {
        public void CriarPost(Post post);
        public Post ConsultarPost(Guid id);
        public int AlterarPost(Post post);
        public ICollection<Post> ConsultarPosts();
        public int ExcluirPost(Guid id); 
        public ICollection<Comentario> ConsultarComentarios();
        public void CriarComentario(Comentario comentario);
        public IEnumerable<Post> GetAllPostsByPerfil(string perfil);
    }
}
